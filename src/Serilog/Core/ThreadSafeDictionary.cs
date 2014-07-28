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

namespace Serilog.Core
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    class ThreadSafeDictionary<TKey,TValue>
    {
        ReaderWriterLockSlim _locker = new ReaderWriterLockSlim();
        Dictionary<TKey,TValue> _dictionary = new Dictionary<TKey, TValue>();

        public TValue GetOrAdd(TKey key, Func<TValue> createValue)
        {
            try
            {
                _locker.EnterUpgradeableReadLock();
                TValue value;
                if (_dictionary.TryGetValue(key, out value))
                {
                    return value;
                }

                value = createValue();
                _locker.EnterWriteLock();
                try
                {
                    _dictionary.Add(key, value);
                }
                finally
                {
                    _locker.ExitWriteLock();
                }
                return value;
            }
            finally
            {
                _locker.ExitUpgradeableReadLock();
            }
        }
    }
}
