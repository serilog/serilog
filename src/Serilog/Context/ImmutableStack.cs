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

        public IEnumerator<T> GetEnumerator()
        {
            var next = this;
            while (!next.IsEmpty)
            {
                yield return next.Top;
                next = next._under;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count { get; }

        public static ImmutableStack<T> Empty { get; } = new ImmutableStack<T>();

        public bool IsEmpty => _under == null;

        public ImmutableStack<T> Push(T t) => new ImmutableStack<T>(this, t);

        public T Top => _top;

    }
}