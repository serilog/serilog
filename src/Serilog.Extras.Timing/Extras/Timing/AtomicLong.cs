////////////////////////////////////////////////////////////////////////////////
//
//  MATTBOLT.BLOGSPOT.COM
//  Copyright(C) 2013 Matt Bolt
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at:
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.
//
//////////////////////////////////////////////////////////////////////////////// 

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

namespace Serilog.Extras.Timing
{
    using System.Threading;

    /// <summary>
    /// Provides lock-free atomic read/write utility for a <c>long</c> value. The atomic classes found in this package
    /// were are meant to replicate the <c>java.util.concurrent.atomic</c> package in Java by Doug Lea. The two main differences
    /// are implicit casting back to the <c>long</c> data type, and the use of a non-volatile inner variable.
    /// 
    /// <para>The internals of these classes contain wrapped usage of the <c>System.Threading.Interlocked</c> class, which is how
    /// we are able to provide atomic operation without the use of locks. </para>
    /// </summary>
    /// <remarks>
    /// It's also important to note that <c>++</c> and <c>--</c> are never atomic, and one of the main reasons this class is 
    /// needed. I don't believe its possible to overload these operators in a way that is autonomous.
    /// </remarks>
    /// author Matt Bolt
    public class AtomicLong
    {

        private long _value;

        /// <summary>
        /// Creates a new <c>AtomicLong</c> instance with an initial value of <c>0</c>.
        /// </summary>
        public AtomicLong()
            : this(0)
        {

        }

        /// <summary>
        /// Creates a new <c>AtomicLong</c> instance with the initial value provided.
        /// </summary>
        public AtomicLong(long value)
        {
            _value = value;
        }

        /// <summary>
        /// This method returns the current value.
        /// </summary>
        /// <returns>
        /// The <c>long</c> value accessed atomically.
        /// </returns>
        public long Get()
        {
            return Interlocked.Read(ref _value);
        }

        /// <summary>
        /// This method sets the current value atomically.
        /// </summary>
        /// <param name="value">
        /// The new value to set.
        /// </param>
        public void Set(long value)
        {
            Interlocked.Exchange(ref _value, value);
        }

        /// <summary>
        /// This method atomically sets the value and returns the original value.
        /// </summary>
        /// <param name="value">
        /// The new value.
        /// </param>
        /// <returns>
        /// The value before setting to the new value.
        /// </returns>
        public long GetAndSet(long value)
        {
            return Interlocked.Exchange(ref _value, value);
        }

        /// <summary>
        /// Atomically sets the value to the given updated value if the current value <c>==</c> the expected value.
        /// </summary>
        /// <param name="expected">
        /// The value to compare against.
        /// </param>
        /// <param name="result">
        /// The value to set if the value is equal to the <c>expected</c> value.
        /// </param>
        /// <returns>
        /// <c>true</c> if the comparison and set was successful. A <c>false</c> indicates the comparison failed.
        /// </returns>
        public bool CompareAndSet(long expected, long result)
        {
            return Interlocked.CompareExchange(ref _value, result, expected) == expected;
        }

        /// <summary>
        /// Atomically adds the given value to the current value.
        /// </summary>
        /// <param name="delta">
        /// The value to add.
        /// </param>
        /// <returns>
        /// The updated value.
        /// </returns>
        public long AddAndGet(long delta)
        {
            return Interlocked.Add(ref _value, delta);
        }

        /// <summary>
        /// This method atomically adds a <c>delta</c> the value and returns the original value.
        /// </summary>
        /// <param name="delta">
        /// The value to add to the existing value.
        /// </param>
        /// <returns>
        /// The value before adding the delta.
        /// </returns>
        public long GetAndAdd(long delta)
        {
            for (; ; )
            {
                long current = Get();
                long next = current + delta;
                if (CompareAndSet(current, next))
                {
                    return current;
                }
            }
        }

        /// <summary>
        /// This method increments the value by 1 and returns the previous value. This is the atomic 
        /// version of post-increment.
        /// </summary>
        /// <returns>
        /// The value before incrementing.
        /// </returns>
        public long Increment()
        {
            return GetAndAdd(1);
        }

        /// <summary>
        /// This method decrements the value by 1 and returns the previous value. This is the atomic 
        /// version of post-decrement.
        /// </summary>
        /// <returns>
        /// The value before decrementing.
        /// </returns>
        public long Decrement()
        {
            return GetAndAdd(-1);
        }

        /// <summary>
        /// This method increments the value by 1 and returns the new value. This is the atomic version 
        /// of pre-increment.
        /// </summary>
        /// <returns>
        /// The value after incrementing.
        /// </returns>
        public long PreIncrement()
        {
            return Interlocked.Increment(ref _value);
        }

        /// <summary>
        /// This method decrements the value by 1 and returns the new value. This is the atomic version 
        /// of pre-decrement.
        /// </summary>
        /// <returns>
        /// The value after decrementing.
        /// </returns>
        public long PreDecrement()
        {
            return Interlocked.Decrement(ref _value);
        }

        /// <summary>
        /// This operator allows an implicit cast from <c>AtomicLong</c> to <c>long</c>.
        /// </summary>
        public static implicit operator long(AtomicLong value)
        {
            return value.Get();
        }

    }

}