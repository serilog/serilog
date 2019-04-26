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
using System.Collections;
using System.Collections.Generic;

// General-purpose type; not all features are used here.
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable MemberCanBeProtected.Global

namespace Serilog.Context
{
    class ImmutableStack<T> : IEnumerable<T>
    {
        readonly ImmutableStack<T> _under;
        readonly T _top;

        ImmutableStack()
        {
        }

        ImmutableStack(ImmutableStack<T> under, T top)
        {
            if (under == null) throw new ArgumentNullException(nameof(under));
            _under = under;
            Count = under.Count + 1;
            _top = top;
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        public int Count { get; }

        public static ImmutableStack<T> Empty { get; } = new ImmutableStack<T>();

        public bool IsEmpty => _under == null;

        public ImmutableStack<T> Push(T t) => new ImmutableStack<T>(this, t);

        public T Top => _top;

        internal struct Enumerator : IEnumerator<T>
        {
            readonly ImmutableStack<T> _stack;
            ImmutableStack<T> _top;
            T _current;

            public Enumerator(ImmutableStack<T> stack)
            {
                _stack = stack;
                _top = stack;
                _current = default(T);
            }

            public bool MoveNext()
            {
                if (_top.IsEmpty)
                    return false;
                _current = _top.Top;
                _top = _top._under;
                return true;
            }

            public void Reset()
            {
                _top = _stack;
                _current = default(T);
            }

            public T Current => _current;

            object IEnumerator.Current => _current;

            public void Dispose()
            {
            }
        }
    }
}
