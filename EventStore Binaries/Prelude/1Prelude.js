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

// these $ globals are defined by external environment
// they are redefined here to make R# like tools understand them
var _log = $log;
var _load_module = $load_module;

function log(message) {
    _log("P: " + message);
}

function initializeModules() {
    // load module load new instance of the given module every time
    // this is a responsibility of prelude to manage instances of modules
    var modules = _load_module('Modules');

    // TODO: replace with createRequire($load_module)
    modules.$load_module = _load_module;

    return modules;
} 

function initializeProjections() {
    var projections = _load_module('Projections');
    return projections;
}

var modules = initializeModules();
var projections = initializeProjections();

function scope($on, $notify) {
    var eventProcessor = projections.createEventProcessor(log, $notify);
    eventProcessor.register_comand_handlers($on);
    
    function queryLog(message) {
        if (typeof message === "string")
            _log(message);
        else
            _log(JSON.stringify(message));
    }

    function translateOn(handlers) {

        for (var name in handlers) {
            if (name == 0 || name === "$init") {
                eventProcessor.on_init_state(handlers[name]);
            } else if (name === "$initShared") {
                eventProcessor.on_init_shared_state(handlers[name]);
            } else if (name === "$any") {
                eventProcessor.on_any(handlers[name]);
            } else if (name === "$deleted") {
                eventProcessor.on_deleted_notification(handlers[name]);
            } else if (name === "$created") {
                eventProcessor.on_created_notification(handlers[name]);
            } else {
                eventProcessor.on_event(name, handlers[name]);
            }
        }
    }


    function $defines_state_transform() {
        eventProcessor.$defines_state_transform();
    }

    function transformBy(by) {
        eventProcessor.chainTransformBy(by);
        return {
            transformBy: transformBy,
            filterBy: filterBy,
            outputState: outputState,
            outputTo: outputTo,
        };
    }

    function filterBy(by) {
        eventProcessor.chainTransformBy(function (s) {
            var result = by(s);
            return result ? s : null;
        });
        return {
            transformBy: transformBy,
            filterBy: filterBy,
            outputState: outputState,
            outputTo: outputTo,
        };
    }

    function outputTo(resultStream, partitionResultStreamPattern) {
        eventProcessor.$defines_state_transform();
        eventProcessor.options({
            resultStreamName: resultStream,
            partitionResultStreamNamePattern: partitionResultStreamPattern,
        });
    }

    function outputState() {
        eventProcessor.$outputState();
        return {
            transformBy: transformBy,
            filterBy: filterBy,
            outputTo: outputTo,
        };
    }

    function when(handlers) {
        translateOn(handlers);
        return {
            $defines_state_transform: $defines_state_transform,
            transformBy: transformBy,
            filterBy: filterBy,
            outputTo: outputTo,
            outputState: outputState,
        };
    }

    function whenAny(handler) {
        eventProcessor.on_any(handler);
        return {
            $defines_state_transform: $defines_state_transform,
            transformBy: transformBy,
            filterBy: filterBy,
            outputState: outputState,
            outputTo: outputTo,
        };
    }

    function foreachStream() {
        eventProcessor.byStream();
        return {
            when: when,
            whenAny: whenAny,
        };
    }

    function partitionBy(byHandler) {
        eventProcessor.partitionBy(byHandler);
        return {
            when: when,
            whenAny: whenAny,
        };
    }

    function fromCategory(category) {
        eventProcessor.fromCategory(category);
        return {
            partitionBy: partitionBy,
            foreachStream: foreachStream,
            when: when,
            whenAny: whenAny,
            outputState: outputState,
        };
    }

    function fromAll() {
        eventProcessor.fromAll();
        return {
            partitionBy: partitionBy,
            when: when,
            whenAny: whenAny,
            foreachStream: foreachStream,
            outputState: outputState,
        };
    }

    function fromStream(stream) {
        eventProcessor.fromStream(stream);
        return {
            partitionBy: partitionBy,
            when: when,
            whenAny: whenAny,
            outputState: outputState,
        };
    }

    function fromStreamCatalog(streamCatalog, transformer) {
        eventProcessor.fromStreamCatalog(streamCatalog, transformer ? transformer : null);
        return {
            foreachStream: foreachStream,
        };
    }

    function fromStreamsMatching(filter) {
        eventProcessor.fromStreamsMatching(filter);
        return {
            when: when,
            whenAny: whenAny,
        };
    }

    function fromStreams(streams) {
        var arr = Array.isArray(streams) ? streams : arguments;
        for (var i = 0; i < arr.length; i++) 
            eventProcessor.fromStream(arr[i]);
 
        return {
            partitionBy: partitionBy,
            when: when,
            whenAny: whenAny,
            outputState: outputState,
        };
    }

    function emit(streamId, eventName, eventBody, metadata) {
        var message = { streamId: streamId, eventName: eventName , body: JSON.stringify(eventBody), metadata: metadata, isJson: true };
        eventProcessor.emit(message);
    }

    function linkTo(streamId, event, metadata) {
        var message = { streamId: streamId, eventName: "$>", body: event.sequenceNumber + "@" + event.streamId, metadata: metadata, isJson: false };
        eventProcessor.emit(message);
    }

    function copyTo(streamId, event, metadata) {
        var m = {};

        var em = event.metadata;
        if (em)
            for (var p1 in em)
                if (p1.indexOf("$") !== 0 || p1 === "$correlationId")
                    m[p1] = em[p1];

        if (metadata) 
            for (var p2 in metadata)
                if (p2.indexOf("$") !== 0)
                    m[p2] = metadata[p2];

        var message = { streamId: streamId, eventName: event.eventType, body: event.bodyRaw, metadata: m };
        eventProcessor.emit(message);
    }

    function linkStreamTo(streamId, linkedStreamId, metadata) {
        var message = { streamId: streamId, eventName: "$@", body: linkedStreamId, metadata: metadata, isJson: false };
        eventProcessor.emit(message);
    }

    function options(options_obejct) {
        eventProcessor.options(options_obejct);
    }

    return {
        log: queryLog,

        on_any: eventProcessor.on_any,
        on_raw: eventProcessor.on_raw,

        fromAll: fromAll,
        fromCategory: fromCategory,
        fromStream: fromStream,
        fromStreams: fromStreams,
        fromStreamCatalog: fromStreamCatalog,
        fromStreamsMatching: fromStreamsMatching,

        options: options,
        emit: emit, 
        linkTo: linkTo,
        copyTo: copyTo,
        linkStreamTo: linkStreamTo,
        require: modules.require,
    };
};

scope;
