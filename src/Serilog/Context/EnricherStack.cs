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



// General-purpose type; not all features are used here.
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable MemberCanBeProtected.Global

namespace Serilog.Context;

class EnricherStack : IEnumerable<ILogEventEnricher>
{
    readonly EnricherStack? _under;
    readonly ILogEventEnricher? _top;

    EnricherStack()
    {
    }

    EnricherStack(EnricherStack under, ILogEventEnricher top)
    {
        _under = Guard.AgainstNull(under);
        Count = under.Count + 1;
        _top = top;
    }

    public Enumerator GetEnumerator() => new(this);

    IEnumerator<ILogEventEnricher> IEnumerable<ILogEventEnricher>.GetEnumerator() => new Enumerator(this);

    IEnumerator IEnumerable.GetEnumerator() => new Enumerator(this);

    public int Count { get; }

    public static EnricherStack Empty { get; } = new();

    public bool IsEmpty => _under == null;

    public EnricherStack Push(ILogEventEnricher t) => new(this, t);

    public ILogEventEnricher Top => _top!;

    internal struct Enumerator : IEnumerator<ILogEventEnricher>
    {
        readonly EnricherStack _stack;
        EnricherStack _top;
        ILogEventEnricher? _current;

        public Enumerator(EnricherStack stack)
        {
            _stack = stack;
            _top = stack;
            _current = null;
        }

        public bool MoveNext()
        {
            if (_top.IsEmpty)
                return false;
            _current = _top.Top;
            _top = _top._under!;
            return true;
        }

        public void Reset()
        {
            _top = _stack;
            _current = null;
        }

        public ILogEventEnricher Current => _current!;

        object IEnumerator.Current => _current!;

        public void Dispose()
        {
        }
    }
}
