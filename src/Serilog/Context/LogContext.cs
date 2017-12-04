// Copyright 2013-2015 Serilog Contributors
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
using System.ComponentModel;
using Serilog.Core;
using Serilog.Core.Enrichers;
using Serilog.Events;

#if ASYNCLOCAL
using System.Threading;
#elif REMOTING
using System.Runtime.Remoting;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting.Messaging;
#endif

namespace Serilog.Context
{
    /// <summary>
    /// Holds ambient properties that can be attached to log events. To
    /// configure, use the <see cref="Serilog.Configuration.LoggerEnrichmentConfiguration.FromLogContext"/> method.
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
    /// <remarks>The scope of the context is the current logical thread, using AsyncLocal
    /// (and so is preserved across async/await calls).</remarks>
    public static class LogContext
    {
#if ASYNCLOCAL
        static readonly AsyncLocal<ImmutableStack<ILogEventEnricher>> Data = new AsyncLocal<ImmutableStack<ILogEventEnricher>>();
#elif REMOTING
        static readonly string DataSlotName = typeof(LogContext).FullName + "@" + Guid.NewGuid();
#else // DOTNET_51
        [ThreadStatic]
        static ImmutableStack<ILogEventEnricher> Data;
#endif

        /// <summary>
        /// Push a property onto the context, returning an <see cref="IDisposable"/>
        /// that must later be used to remove the property, along with any others that
        /// may have been pushed on top of it and not yet popped. The property must
        /// be popped from the same thread/logical call context.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <param name="value">The value of the property.</param>
        /// <returns>A handle to later remove the property from the context.</returns>
        /// <param name="destructureObjects">If true, and the value is a non-primitive, non-array type,
        /// then the value will be converted to a structure; otherwise, unknown types will
        /// be converted to scalars, which are generally stored as strings.</param>
        /// <returns>A token that must be disposed, in order, to pop properties back off the stack.</returns>
        public static IDisposable PushProperty(string name, object value, bool destructureObjects = false)
        {
            return Push(new PropertyEnricher(name, value, destructureObjects));
        }

        /// <summary>
        /// Push an enricher onto the context, returning an <see cref="IDisposable"/>
        /// that must later be used to remove the property, along with any others that
        /// may have been pushed on top of it and not yet popped. The property must
        /// be popped from the same thread/logical call context.
        /// </summary>
        /// <param name="enricher">An enricher to push onto the log context</param>
        /// <returns>A token that must be disposed, in order, to pop properties back off the stack.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IDisposable Push(ILogEventEnricher enricher)
        {
            if (enricher == null) throw new ArgumentNullException(nameof(enricher));

            var stack = GetOrCreateEnricherStack();
            var bookmark = new ContextStackBookmark(stack);

            Enrichers = stack.Push(enricher);

            return bookmark;
        }

        /// <summary>
        /// Push multiple enrichers onto the context, returning an <see cref="IDisposable"/>
        /// that must later be used to remove the property, along with any others that
        /// may have been pushed on top of it and not yet popped. The property must
        /// be popped from the same thread/logical call context.
        /// </summary>
        /// <seealso cref="PropertyEnricher"/>.
        /// <param name="enrichers">Enrichers to push onto the log context</param>
        /// <returns>A token that must be disposed, in order, to pop properties back off the stack.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IDisposable Push(params ILogEventEnricher[] enrichers)
        {
            if (enrichers == null) throw new ArgumentNullException(nameof(enrichers));

            var stack = GetOrCreateEnricherStack();
            var bookmark = new ContextStackBookmark(stack);

            for (var i = 0; i < enrichers.Length; ++i)
                stack = stack.Push(enrichers[i]);

            Enrichers = stack;

            return bookmark;
        }

        /// <summary>
        /// Push enrichers onto the log context. This method is obsolete, please
        /// use <see cref="Push(Serilog.Core.ILogEventEnricher[])"/> instead.
        /// </summary>
        /// <param name="properties">Enrichers to push onto the log context</param>
        /// <returns>A token that must be disposed, in order, to pop properties back off the stack.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        [Obsolete("Please use `LogContext.Push(properties)` instead.")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static IDisposable PushProperties(params ILogEventEnricher[] properties)
        {
            return Push(properties);
        }

        /// <summary>
        /// Obtain an enricher that represents the current contents of the <see cref="LogContext"/>. This
        /// can be pushed back onto the context in a different location/thread when required.
        /// </summary>
        /// <returns>An enricher that represents the current contents of the <see cref="LogContext"/>.</returns>
        public static ILogEventEnricher Clone()
        {
            var stack = GetOrCreateEnricherStack();
            return new SafeAggregateEnricher(stack);
        }

        /// <summary>
        /// Remove all enrichers from the <see cref="LogContext"/>, returning an <see cref="IDisposable"/>
        /// that must later be used to restore enrichers that were on the stack before <see cref="Suspend"/> was called.
        /// </summary>
        /// <returns>A token that must be disposed, in order, to restore properties back to the stack.</returns>
        public static IDisposable Suspend()
        {
            var stack = GetOrCreateEnricherStack();
            var bookmark = new ContextStackBookmark(stack);

            Enrichers = ImmutableStack<ILogEventEnricher>.Empty;

            return bookmark;
        }

        /// <summary>
        /// Remove all enrichers from <see cref="LogContext"/> for the current async scope. 
        /// </summary>
        public static void Reset()
        {
            if (Enrichers != null && Enrichers != ImmutableStack<ILogEventEnricher>.Empty)
            {
                Enrichers = ImmutableStack<ILogEventEnricher>.Empty;
            }
        }

        static ImmutableStack<ILogEventEnricher> GetOrCreateEnricherStack()
        {
            var enrichers = Enrichers;
            if (enrichers == null)
            {
                enrichers = ImmutableStack<ILogEventEnricher>.Empty;
                Enrichers = enrichers;
            }
            return enrichers;
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

        sealed class ContextStackBookmark : IDisposable
        {
            readonly ImmutableStack<ILogEventEnricher> _bookmark;

            public ContextStackBookmark(ImmutableStack<ILogEventEnricher> bookmark)
            {
                _bookmark = bookmark;
            }

            public void Dispose()
            {
                Enrichers = _bookmark;
            }
        }

#if ASYNCLOCAL

        static ImmutableStack<ILogEventEnricher> Enrichers
        {
            get => Data.Value;
            set => Data.Value = value;
        }

#elif REMOTING

        static ImmutableStack<ILogEventEnricher> Enrichers
        {
            get
            {
                var objectHandle = CallContext.LogicalGetData(DataSlotName) as ObjectHandle;

                return objectHandle?.Unwrap() as ImmutableStack<ILogEventEnricher>;
            }
            set
            {
                if (CallContext.LogicalGetData(DataSlotName) is IDisposable oldHandle)
                {
                    oldHandle.Dispose();
                }
                
                CallContext.LogicalSetData(DataSlotName, new DisposableObjectHandle(value));
            }
        }

        sealed class DisposableObjectHandle : ObjectHandle, IDisposable
        {
            static readonly ISponsor LifeTimeSponsor = new ClientSponsor();

            public DisposableObjectHandle(object o) : base(o)
            {
            }

            public override object InitializeLifetimeService()
            {
                var lease = base.InitializeLifetimeService() as ILease;
                lease?.Register(LifeTimeSponsor);
                return lease;
            }

            public void Dispose()
            {
                if (GetLifetimeService() is ILease lease)
                {
                    lease.Unregister(LifeTimeSponsor);
                }
            }
        }

#else // DOTNET_51

        static ImmutableStack<ILogEventEnricher> Enrichers
        {
            get => Data;
            set => Data = value;
        }
#endif
    }
}
