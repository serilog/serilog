// Copyright 2014 Serilog Contributors
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Runtime.Remoting.Messaging;
using Serilog.Core;
using Serilog.Core.Enrichers;
using Serilog.Events;

namespace Serilog.Context
{
    /// <summary>
    /// Holds ambient properties that can be attached to log events. To
    /// configure, use the <see cref="LoggerConfigurationFullNetFxExtensions.FromLogContext"/>
    /// extension method.
    /// </summary>
    /// <example>
    /// Configuration:
    /// <code lang="C#">
    /// var log = new LoggerConfiguration()
    ///     .Enrich.FromLogContext()
    ///     ...
    /// </code>
    /// Usage:
    /// <code lang="C#">
    /// using (LogContext.PushProperty("MessageId", message.Id))
    /// {
    ///     Log.Information("The MessageId property will be attached to this event");
    /// }
    /// </code>
    /// </example>
    /// <remarks>The scope of the context is the current logical thread, using
    /// <see cref="CallContext.LogicalGetData"/> (and so is
    /// preserved across async/await calls).</remarks>
    public static class LogContext
    {
        static readonly string DataSlotName = typeof(LogContext).FullName;

        /// <summary>
        /// Push a property onto the context, returning an <see cref="ContextStackBookmark"/>
        /// that can later be used to remove the property, along with any others that
        /// may have been pushed on top of it and not yet popped. The property must
        /// be popped from the same thread/logical call context.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <param name="value">The value of the property.</param>
        /// <returns>A handle to later remove the property from the context.</returns>
        /// <param name="destructureObjects">If true, and the value is a non-primitive, non-array type,
        /// then the value will be converted to a structure; otherwise, unknown types will
        /// be converted to scalars, which are generally stored as strings.</param>
        /// <returns></returns>
        public static IDisposable PushProperty(string name, object value, bool destructureObjects = false)
        {
            var enrichers = Enrichers;
            if (enrichers == null)
            {
                enrichers = ImmutableStack<ILogEventEnricher>.Empty;
                Enrichers = enrichers;
            }

            var bookmark = new ContextStackBookmark(enrichers);
            Enrichers = enrichers.Push(new LazyFixedPropertyEnricher(name, value, destructureObjects));
            return bookmark;
        }

        /// <summary>
        /// Push multiple properties onto the context, returning an <see cref="IDisposable"/>
        /// that can later be used to remove the properties. The properties must
        /// be popped from the same thread/logical call context.
        /// </summary>
        /// <param name="properties">Log Properties to push onto the log context</param>
        /// <returns></returns>
        public static IDisposable PushProperties(params LogProperty[] properties)
        {
            if (Enrichers == null)
            {
                Enrichers = ImmutableStack<ILogEventEnricher>.Empty;
            }

            var bookmark = new ContextStackBookmark(Enrichers);

            foreach (var prop in properties)
            {
                Enrichers = Enrichers.Push(new LazyFixedPropertyEnricher(prop.Name, prop.Value, prop.DestructureObjects));
            }

            return bookmark;
        }

        static ImmutableStack<ILogEventEnricher> Enrichers
        {
            get { return (ImmutableStack<ILogEventEnricher>)CallContext.LogicalGetData(DataSlotName); }
            set { CallContext.LogicalSetData(DataSlotName, value); }
        }

        internal static void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var enrichers = Enrichers;
            if (enrichers == null || enrichers == ImmutableStack<ILogEventEnricher>.Empty)
                return;

            foreach (var enricher in enrichers)
            {
                enricher.Enrich(logEvent, propertyFactory);
            }
        }

        /// <summary>
        /// Bookmark the initial stack location so when the dispose is called
        /// the property stack is set back to that bookmark "unrolling" the stack.
        /// </summary>
        sealed class ContextStackBookmark : IDisposable
        {
            readonly ImmutableStack<ILogEventEnricher> _bookmark;

            public ContextStackBookmark(ImmutableStack<ILogEventEnricher> bookmark)
            {
                _bookmark = bookmark;
            }

            /// <summary>
            /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
            /// </summary>
            /// <filterpriority>2</filterpriority>
            public void Dispose()
            {
                Enrichers = _bookmark;
            }
        }
    }

    /// <summary>
    /// Structure to add properties to the logger.
    /// </summary>
    public sealed class LogProperty
    {
        /// <summary>
        /// Property Name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Property Value
        /// </summary>
        public object Value { get; private set; }

        /// <summary>
        /// Destructure the property value?
        /// </summary>
        public bool DestructureObjects { get; private set; }

        /// <summary>
        /// Create a new log property.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="destructureObjects"></param>
        public LogProperty(string name, object value, bool destructureObjects = false)
        {
            Name = name;
            Value = value;
            DestructureObjects = destructureObjects;
        }
    }
}
