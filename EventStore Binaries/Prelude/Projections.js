// Copyright (c) 2012, Event Store LLP
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are
// met:
// 
// Redistributions of source code must retain the above copyright notice,
// this list of conditions and the following disclaimer.
// Redistributions in binary form must reproduce the above copyright
// notice, this list of conditions and the following disclaimer in the
// documentation and/or other materials provided with the distribution.
// Neither the name of the Event Store LLP nor the names of its
// contributors may be used to endorse or promote products derived from
// this software without specific prior written permission
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
// "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
// LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
// A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
// HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
// SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
// LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
// DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
// THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
// OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
// 
"use strict";

var $projections = {
    createEventProcessor: function (_log, _notify) {
        var debugging = false;
        var runDefaultHandler = true;
        var eventHandlers = {};
        var anyEventHandlers = [];
        var deletedNotificationHandlers = [];
        var createdNotificationHandlers = [];
        var rawEventHandlers = [];
        var transformers = [];
        var getStatePartitionHandler = function () {
            throw "GetStatePartition is not defined";
        };
        var catalogEventTransformer = null;

        var sources = {
            /* TODO: comment out default falses to reduce message size */
            allStreams: false,
            allEvents: true,
            byStreams: false,
            byCustomPartitions: false,
            categories: [],
            streams: [],
            catalogStream: null,
            events: [],

            options: {
                definesStateTransform: false,
                definesCatalogTransform: false,
                handlesDeletedNotifications: false,
                producesResults: false,
                definesFold: false,
                resultStreamName: null,
                partitionResultStreamNamePattern: null,
                $forceProjectionName: null,
                $includeLinks: false,
                disableParallelism: false,
                reorderEvents: false,
                processingLag: 0,
                biState: false,
            },
            version: 4
        };

        var initStateHandler = function () {
            return {};
        };

        var initSharedStateHandler = function () {
            return {};
        };

        var projectionState = null;
        var projectionSharedState = null;

        var commandHandlers = {
            set_debugging: function () {
                debugging = true;
            },

            initialize: function () {
                var initialState = initStateHandler();
                projectionState = initialState;
                return "OK";
            },

            initialize_shared: function () {
                var initialState = initSharedStateHandler();
                projectionSharedState = initialState;
                return "OK";
            },

            get_state_partition: function (event, isJson, streamId, eventType, category, sequenceNumber, metadata, linkMetadata) {
                return getStatePartition(event, streamId, eventType, category, sequenceNumber, metadata, linkMetadata);
            },

            transform_catalog_event: function (event, isJson, streamId, eventType, category, sequenceNumber, metadata, linkMetadata, partition, streamMetadataRaw) {
                if (!catalogEventTransformer)
                    throw "catalogEventTransformer is not set";
                var eventEnvelope = new envelope(null, event, eventType, streamId, sequenceNumber, metadata, linkMetadata, partition, streamMetadataRaw);

                if (isJson) {
                    tryDeserializeBody(eventEnvelope);
                }

                var result = catalogEventTransformer(partition, eventEnvelope);

                return result;
            },

            process_event: function (event, isJson, streamId, eventType, category, sequenceNumber, metadata, linkMetadata, partition) {
                processEvent(event, isJson, streamId, eventType, category, sequenceNumber, metadata, linkMetadata, partition);
                var stateJson;
                var finalResult;
                if (!sources.options.biState) {
                    stateJson = JSON.stringify(projectionState);
                    return stateJson;
                } else {
                    stateJson = JSON.stringify(projectionState);
                    var sharedStateJson = JSON.stringify(projectionSharedState);
                    finalResult = [stateJson, sharedStateJson];
                    return finalResult;
                }
            },

            process_deleted_notification: function (partition, isSoftDeleted) {
                processDeletedNotification(partition, isSoftDeleted);
                var stateJson;
                if (!sources.options.biState) {
                    stateJson = JSON.stringify(projectionState);
                    return stateJson;
                } else {
                    throw "Bi-State projections do not support delete notifications";
                }
            },

            process_created_notification: function (event, isJson, streamId, eventType, category, sequenceNumber, metadata, linkMetadata, partition) {
                processCreatedNotification(event, isJson, streamId, eventType, category, sequenceNumber, metadata, linkMetadata, partition);
                var stateJson;
                stateJson = JSON.stringify(projectionState);
                return stateJson;
            },

            transform_state_to_result: function () {
                var result = projectionState;
                for (var i = 0; i < transformers.length; i++) {
                    var by = transformers[i];
                    result = by(result);
                    if (result === null)
                        break;
                }
                return result !== null ? JSON.stringify(result) : null;
            },

            set_state: function (jsonState) {
                var parsedState = JSON.parse(jsonState);
                projectionState = parsedState;
                return "OK";
            },

            set_shared_state: function(jsonState) {
                var parsedState = JSON.parse(jsonState);
                projectionSharedState = parsedState;
                return "OK";
            },

            debugging_get_state: function () {
                return JSON.stringify(projectionState);
            },

            get_sources: function () {
                return JSON.stringify(sources);
            }
        };

        function registerCommandHandlers($on) {
            // this is the only way to pass parameters to the system module

            for (var name in commandHandlers) {
                $on(name, commandHandlers[name]);
            }
        }

        function on_event(eventName, eventHandler) {
            runDefaultHandler = false;
            eventHandlers[eventName] = eventHandler;
            sources.allEvents = false;
            sources.events.push(eventName);
            sources.options.definesFold = true;
        }

        function on_init_state(initHandler) {
            initStateHandler = initHandler;
            sources.options.definesFold = true;
        }

        function on_init_shared_state(initHandler) {
            initSharedStateHandler = initHandler;
            sources.options.definesFold = true;
        }

        function on_any(eventHandler) {
            runDefaultHandler = false;
            sources.allEvents = true;
            anyEventHandlers.push(eventHandler);
            sources.options.definesFold = true;
        }

        function on_deleted_notification(eventHandler) {
            deletedNotificationHandlers.push(eventHandler);
            sources.options.handlesDeletedNotifications = true;
            sources.options.definesFold = true;
        }

        function on_created_notification(eventHandler) {
            createdNotificationHandlers.push(eventHandler);
            sources.options.handlesCreatedNotifications = true;
            sources.options.definesFold = true;
        }

        function on_raw(eventHandler) {
            runDefaultHandler = false;
            sources.allEvents = true;
            rawEventHandlers.push(eventHandler);
            sources.options.definesFold = true;
        }

        function callHandler(handler, state, eventEnvelope) {
            if (debugging)
                debugger;
            var newState = handler(state, eventEnvelope);
            if (newState === undefined)
                newState = state;
            return newState;
        };

        function tryDeserializeBody(eventEnvelope) {
            var eventRaw = eventEnvelope.bodyRaw;
            try {
                if (eventRaw == '') {
                    eventEnvelope.body = {};
                } else if (typeof eventRaw === "object") { //TODO: why do we need this?
                    eventEnvelope.body = eventRaw;
                    eventEnvelope.isJson = true;
                } else {
                    eventEnvelope.body = JSON.parse(eventRaw);
                    eventEnvelope.isJson = true;
                }
                eventEnvelope.data = eventEnvelope.body;

            } catch (ex) {
                _log("JSON Parsing error: " + ex);
                eventEnvelope.jsonError = ex;
                eventEnvelope.body = undefined;
                eventEnvelope.data = undefined;
            }
        }

        function envelope(body, bodyRaw, eventType, streamId, sequenceNumber, metadataRaw, linkMetadataRaw,
            partition, streamMetadataRaw) {
            this.isJson = false;
            this.data = body;
            this.body = body;
            this.bodyRaw = bodyRaw;;
            this.eventType = eventType;
            this.streamId = streamId;
            this.sequenceNumber = sequenceNumber;
            this.metadataRaw = metadataRaw;
            this.streamMetadataRaw = streamMetadataRaw;
            this.linkMetadataRaw = linkMetadataRaw;
            this.partition = partition;
            this.metadata_ = null;
        }

        Object.defineProperty(envelope.prototype, "metadata", {
            get: function () {
                if (!this.metadata_ && this.metadataRaw) {
                    this.metadata_ = JSON.parse(this.metadataRaw);
                }
                return this.metadata_;
            }
        });

        Object.defineProperty(envelope.prototype, "streamMetadata", {
            get: function () {
                if (!this.streamMetadata_) {
                    if (this.streamMetadataRaw) {
                        this.streamMetadata_ = JSON.parse(this.streamMetadataRaw);
                    } else {
                        this.streamMetadata_ = {};
                    }
                }
                return this.streamMetadata_;
            }
        });

        Object.defineProperty(envelope.prototype, "linkMetadata", {
            get: function () {
                if (!this.linkMetadata_) {
                    if (this.linkMetadataRaw) {
                        this.linkMetadata_ = JSON.parse(this.linkMetadataRaw);
                    } else {
                        this.linkMetadata_ = {};
                    }
                }
                return this.linkMetadata_;
            }
        });

        function getStatePartition(eventRaw, isJson, streamId, eventType, category, sequenceNumber, metadataRaw, linkMetadataRaw) {

            var eventHandler = getStatePartitionHandler;

            var eventEnvelope = new envelope(null, eventRaw, eventType, streamId, sequenceNumber, metadataRaw, linkMetadataRaw, null, null);

            if (isJson)
                tryDeserializeBody(eventEnvelope);

            var partition = eventHandler(eventEnvelope);

            var result;
            //TODO: warn/disable empty string
            if (partition === undefined || partition === null || partition === "")
                result = "";
            else
                result = partition.toString();
            return result;

        }

        function defaultEventHandler(state, eventEnvelope) {
            return eventEnvelope.isJson ? eventEnvelope.body : { $e: eventEnvelope.bodyRaw };
        }

        function processEvent(eventRaw, isJson, streamId, eventType, category, sequenceNumber, metadataRaw, linkMetadataRaw, partition, streamMetadataRaw) {

            var eventName = eventType;

            var eventHandler;
            var state = !sources.options.biState ? projectionState : [projectionState, projectionSharedState];

            var index;

            var eventEnvelope = new envelope(null, eventRaw, eventType, streamId, sequenceNumber, metadataRaw, linkMetadataRaw, partition, streamMetadataRaw);
            // debug only
            for (index = 0; index < rawEventHandlers.length; index++) {
                eventHandler = rawEventHandlers[index];
                state = callHandler(eventHandler, state, eventEnvelope);
            }

            eventHandler = eventHandlers[eventName];
            if (isJson && (runDefaultHandler || eventHandler !== undefined || anyEventHandlers.length > 0)) {
                tryDeserializeBody(eventEnvelope);
            }

            if (runDefaultHandler) {
                state = callHandler(defaultEventHandler, state, eventEnvelope);
            }

            for (index = 0; index < anyEventHandlers.length; index++) {
                eventHandler = anyEventHandlers[index];
                state = callHandler(eventHandler, state, eventEnvelope);
            }

            eventHandler = eventHandlers[eventName];
            if (eventHandler !== undefined) {
                state = callHandler(eventHandler, state, eventEnvelope);
            }
            if (!sources.options.biState) {
                projectionState = state;
            } else {
                projectionState = state[0];
                projectionSharedState = state[1];
            }
        }

        function processDeletedNotification(partition, isSoftDeleted) {

            var eventEnvelope = { partition: partition, isSoftDeleted: isSoftDeleted };
            var state = !sources.options.biState ? projectionState : [projectionState, projectionSharedState];
            var index;
            var eventHandler;

            for (index = 0; index < deletedNotificationHandlers.length; index++) {
                eventHandler = deletedNotificationHandlers[index];
                state = callHandler(eventHandler, state, eventEnvelope);
            }

            if (!sources.options.biState) {
                projectionState = state;
            } else {
                throw "Bi-State projections do not support delete notifications";
            }
        }

        function processCreatedNotification(eventRaw, isJson, streamId, eventType, category, sequenceNumber, metadataRaw, linkMetadataRaw, partition, streamMetadataRaw) {

            var eventHandler;
            var state = !sources.options.biState ? projectionState : [projectionState, projectionSharedState];

            var index;

            var eventEnvelope = new envelope(null, eventRaw, eventType, streamId, sequenceNumber, metadataRaw, linkMetadataRaw, partition, streamMetadataRaw);

            if (isJson) {
                tryDeserializeBody(eventEnvelope);
            }

            for (index = 0; index < createdNotificationHandlers.length; index++) {
                eventHandler = createdNotificationHandlers[index];
                state = callHandler(eventHandler, state, eventEnvelope);
            }

            if (!sources.options.biState) {
                projectionState = state;
            } else {
                projectionState = state[0];
                projectionSharedState = state[1];
            }
        }

        function fromStream(sourceStream) {
            sources.streams.push(sourceStream);
        }

        function fromCategory(sourceCategory) {
            sources.categories.push(sourceCategory);
        }

        function fromStreamCatalog(streamCatalog, transformer) {
            sources.catalogStream = streamCatalog;
            sources.options.definesCatalogTransform = transformer != null;
            catalogEventTransformer = function(streamId, ev) {
                return transformer(ev);
            };
        }

        function fromStreamsMatching(filter) {
            sources.catalogStream = "$all";
            sources.options.definesCatalogTransform = true;
            catalogEventTransformer = function(streamId, ev) {
                return filter(streamId, ev) ? streamId : null;
            };
            byStream();
        }

        function byStream() {
            sources.byStreams = true;
        }

        function partitionBy(eventHandler) {
            getStatePartitionHandler = eventHandler;
            sources.byCustomPartitions = true;
        }

        function $defines_state_transform() {
            sources.options.definesStateTransform = true;
            sources.options.producesResults = true;
        }

        function $outputState() {
            sources.options.producesResults = true;
        }

        function chainTransformBy(by) {
            transformers.push(by);
            sources.options.definesStateTransform = true;
            sources.options.producesResults = true;
        }

        function fromAll() {
            sources.allStreams = true;
        }

        function emit(ev) {
            _notify("emit", JSON.stringify(ev));
        }

        function options(opts) {
            for (var name in opts) {
                if (sources.options[name] === undefined)
                    throw "Unrecognized option: " + name;
                sources.options[name] = opts[name];
            }
        }

        return {
            on_event: on_event,
            on_init_state: on_init_state,
            on_init_shared_state: on_init_shared_state,
            on_any: on_any,
            on_raw: on_raw,
            on_deleted_notification: on_deleted_notification,
            on_created_notification: on_created_notification,

            fromAll: fromAll,
            fromCategory: fromCategory,
            fromStream: fromStream,
            fromStreamCatalog: fromStreamCatalog,
            fromStreamsMatching: fromStreamsMatching,

            byStream: byStream,
            partitionBy: partitionBy,
            $defines_state_transform: $defines_state_transform,
            $outputState: $outputState,
            chainTransformBy: chainTransformBy,

            emit: emit,
            options: options,

            register_comand_handlers: registerCommandHandlers,
        };
    }
};
$projections;