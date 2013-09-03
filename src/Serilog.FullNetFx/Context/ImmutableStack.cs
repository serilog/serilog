// Copyright 2013 Serilog Contributors
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

namespace Serilog.Context
{
    // Needed because of the shallow-copying behaviour of
    // LogicalCallContext.
    class ImmutableStack<T> : IEnumerable<T>
    {
        static readonly ImmutableStack<T> _empty = new ImmutableStack<T>();

        readonly int _count;
        readonly ImmutableStack<T> _under;
        readonly T _top;

        ImmutableStack()
        {
        }

        ImmutableStack(ImmutableStack<T> under, T top)
        {
            if (under == null) throw new ArgumentNullException("under"); 
            _under = under;
            _count = under.Count + 1;
            _top = top;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var next = this;
            while (next != _empty)
            {
                yield return next.Top;
                next = next._under;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count { get { return _count; } }

        public static ImmutableStack<T> Empty { get { return _empty; } }

        public ImmutableStack<T> Push(T t)
        {
            return new ImmutableStack<T>(this, t);
        }

        public T Top { get { return _top; } }
    }
}