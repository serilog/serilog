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
using Serilog.Events;
using System.Threading;
using System.Threading.Tasks;

namespace Serilog.Extras.Timing
{
	sealed class MeterMeasure : IMeterMeasure, IDisposable
	{
		private readonly ILogger _logger;
		private readonly string _name;
		private readonly TimeUnit _rateUnit;
		private readonly string _measuring;
		private readonly LogEventLevel _level;
		private readonly string _template;
		private static AtomicLong _counter = new AtomicLong();

		private readonly CancellationTokenSource _cancellationToken = new CancellationTokenSource();

		private static readonly TimeSpan Interval = TimeSpan.FromSeconds(5);
		private EWMA _m1Rate = EWMA.OneMinuteEWMA();
		private EWMA _m5Rate = EWMA.FiveMinuteEWMA();
		private EWMA _m15Rate = EWMA.FifteenMinuteEWMA();

		private readonly long _startTime = DateTime.Now.Ticks;

		public MeterMeasure(ILogger logger, string name, string measuring, TimeUnit rateUnit, LogEventLevel level, string template)
		{
			_logger = logger;
			_name = name;
			_rateUnit = rateUnit;
			_measuring = measuring ?? "operations";
			_level = level;
			_template = template;

			Task.Factory.StartNew(async () =>
				{
					while (!_cancellationToken.IsCancellationRequested)
					{
						await Task.Delay(Interval, _cancellationToken.Token);
						Tick();
					}
				}, _cancellationToken.Token);
		}


		public void Mark(long n = 1)
		{
			_counter.AddAndGet(n);
			_m1Rate.Update(n);
			_m5Rate.Update(n);
			_m15Rate.Update(n);

		}

		private void Tick()
		{
			_m1Rate.Tick();
			_m5Rate.Tick();
			_m15Rate.Tick();
		}

		public void Dispose()
		{
			_cancellationToken.Cancel();
		}

		/// <summary>
		///  Returns the total number of events which have been marked.
		/// </summary>
		/// <returns></returns>
		public long Count
		{
			get { return _counter.Get(); }
		}



		public void Write()
		{
			var value = _counter.Get();

			var meanRate = new Rate
			{
				Value = value,
				TimeUnit = Abbreviate(_rateUnit),
				MeterType = _measuring
			};


			if (Count != 0)
			{
				var elapsed = (DateTime.Now.Ticks - _startTime) * 100; // 1 DateTime Tick == 100ns
				meanRate.Value = (Count / (double)elapsed) * _rateUnit.ToNanos(1); 
			}

			var oneMinuteRate = new Rate
			{
				Value = _m1Rate.Rate(_rateUnit),
				TimeUnit = Abbreviate(_rateUnit),
				MeterType = _measuring
			};

			var fiveMinuteRate = new Rate
			{
				Value = _m5Rate.Rate(_rateUnit),
				TimeUnit = Abbreviate(_rateUnit),
				MeterType = _measuring
			};

			var fifteenMinuteRate = new Rate
			{
				Value = _m15Rate.Rate(_rateUnit),
				TimeUnit = Abbreviate(_rateUnit),
				MeterType = _measuring
			};

			_logger.Write(_level, _template, _name, value, meanRate, oneMinuteRate, fiveMinuteRate, fifteenMinuteRate);

		}

		static string Abbreviate(TimeUnit unit)
		{
			switch (unit)
			{
			case TimeUnit.Nanoseconds:
				return "ns";
			case TimeUnit.Microseconds:
				return "us";
			case TimeUnit.Milliseconds:
				return "ms";
			case TimeUnit.Seconds:
				return "s";
			case TimeUnit.Minutes:
				return "m";
			case TimeUnit.Hours:
				return "h";
			case TimeUnit.Days:
				return "d";
			default:
				throw new ArgumentOutOfRangeException("unit");
			}
		}
	}

	/// <summary>
	///   Describes the rate.
	/// </summary>
	public class Rate
	{
		/// <summary>
		/// The actual value
		/// </summary>
		public Double Value { get; set; }

		/// <summary>
		/// What are we measuring
		/// </summary>
		public string MeterType { get; set; }

		/// <summary>
		/// Unit of time
		/// </summary>
		public string TimeUnit { get; set; }

		/// <summary>
		/// Returns a formatted representation.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return String.Format("{0} {1}/{2}", Value, MeterType, TimeUnit);
		}


	}

	/// <summary>
	/// Provides support for timing values
	/// <see href="http://download.oracle.com/javase/6/docs/api/java/util/concurrent/TimeUnit.html"/>
	/// </summary>
	public enum TimeUnit
	{
		/// <summary>
		/// 
		/// </summary>
		Nanoseconds = 0,
		/// <summary>
		/// 
		/// </summary>
		Microseconds = 1,
		/// <summary>
		/// 
		/// </summary>
		Milliseconds = 2,
		/// <summary>
		/// 
		/// </summary>
		Seconds = 3,
		/// <summary>
		/// 
		/// </summary>
		Minutes = 4,
		/// <summary>
		/// 
		/// </summary>
		Hours = 5,
		/// <summary>
		/// 
		/// </summary>
		Days = 6
	}

	/// <summary>
	/// Provides support for volatile operations around a <see cref="double" /> value
	/// </summary>
	internal struct VolatileDouble
	{
		private double _value;

		public static VolatileDouble operator +(VolatileDouble left, VolatileDouble right)
		{
			return Add(left, right);
		}

		private static VolatileDouble Add(VolatileDouble left, VolatileDouble right)
		{
			left.Set(left.Get() + right.Get());
			return left.Get();
		}

		public static VolatileDouble operator -(VolatileDouble left, VolatileDouble right)
		{
			left.Set(left.Get() - right.Get());
			return left.Get();
		}

		public static VolatileDouble operator *(VolatileDouble left, VolatileDouble right)
		{
			left.Set(left.Get() * right.Get());
			return left.Get();
		}

		public static VolatileDouble operator /(VolatileDouble left, VolatileDouble right)
		{
			left.Set(left.Get() / right.Get());
			return left.Get();
		}

		private VolatileDouble(double value)
			: this()
		{
			Set(value);
		}

		public void Set(double value)
		{
			Thread.VolatileWrite(ref _value, value);
		}

		public double Get()
		{
			return Thread.VolatileRead(ref _value);
		}

		public static implicit operator VolatileDouble(double value)
		{
			return new VolatileDouble(value);
		}

		public static implicit operator double(VolatileDouble value)
		{
			return value.Get();
		}

		public override string ToString()
		{
			return Get().ToString();
		}
	}

	/// <summary>
	///  An exponentially-weighted moving average
	/// </summary>
	/// <see href="http://www.teamquest.com/pdfs/whitepaper/ldavg1.pdf"/>
	/// <see href="http://www.teamquest.com/pdfs/whitepaper/ldavg2.pdf" />
	public class EWMA
	{
		private static readonly double M1Alpha = 1 - Math.Exp(-5 / 60.0);
		private static readonly double M5Alpha = 1 - Math.Exp(-5 / 60.0 / 5);
		private static readonly double M15Alpha = 1 - Math.Exp(-5 / 60.0 / 15);

		private readonly AtomicLong _uncounted = new AtomicLong(0);
		private readonly double _alpha;
		private readonly double _interval;
		private volatile bool _initialized;
		private VolatileDouble _rate;

		/// <summary>
		/// Creates a new EWMA which is equivalent to the UNIX one minute load average and which expects to be ticked every 5 seconds.
		/// </summary>
		public static EWMA OneMinuteEWMA()
		{
			return new EWMA(M1Alpha, 5, TimeUnit.Seconds);
		}

		/// <summary>
		/// Creates a new EWMA which is equivalent to the UNIX five minute load average and which expects to be ticked every 5 seconds.
		/// </summary>
		/// <returns></returns>
		public static EWMA FiveMinuteEWMA()
		{
			return new EWMA(M5Alpha, 5, TimeUnit.Seconds);
		}

		/// <summary>
		///  Creates a new EWMA which is equivalent to the UNIX fifteen minute load average and which expects to be ticked every 5 seconds.
		/// </summary>
		/// <returns></returns>
		public static EWMA FifteenMinuteEWMA()
		{
			return new EWMA(M15Alpha, 5, TimeUnit.Seconds);
		}

		/// <summary>
		/// Create a new EWMA with a specific smoothing constant.
		/// </summary>
		/// <param name="alpha">The smoothing constant</param>
		/// <param name="interval">The expected tick interval</param>
		/// <param name="intervalUnit">The time unit of the tick interval</param>
		public EWMA(double alpha, long interval, TimeUnit intervalUnit)
		{
			_interval = intervalUnit.ToNanos(interval);
			_alpha = alpha;
		}

		/// <summary>
		///  Update the moving average with a new value.
		/// </summary>
		/// <param name="n"></param>
		public void Update(long n)
		{
			_uncounted.AddAndGet(n);
		}

		/// <summary>
		/// Mark the passage of time and decay the current rate accordingly.
		/// </summary>
		public void Tick()
		{
			var count = _uncounted.GetAndSet(0);
			var instantRate = count / _interval;
			if (_initialized)
			{
				_rate += _alpha * (instantRate - _rate);
			}
			else
			{
				_rate.Set(instantRate);
				_initialized = true;
			}
		}

		/// <summary>
		/// Returns the rate in the given units of time.
		/// </summary>
		public double Rate(TimeUnit rateUnit)
		{
			var nanos = rateUnit.ToNanos(1);
			return _rate * nanos;
		}
	}

	/// <summary>
	/// Provides enum methods for timing values
	/// </summary>
	public static class TimeUnitExtensions
	{
		private static readonly long[][] _conversionMatrix = BuildConversionMatrix();

		private static long[][] BuildConversionMatrix()
		{
			var unitsCount = Enum.GetValues(typeof(TimeUnit)).Length;
			var timingFactors = new[] 
			{
				1000L,  // Nanos to micros
				1000L,  // Micros to millis
				1000L,  // Millis to seconds
				60L,    // Seconds to minutes
				60L,    // Minutes to hours
				24L     // Hours to days
			};

			// matrix[i, j] holds the timing factor we need to divide by to get from i to j;
			// we'll only populate the part of the matrix where j < i since the other half uses the same factors
			var matrix = new long[unitsCount][];
			for (var source = 0; source < unitsCount; source++)
			{
				matrix[source] = new long[source];
				var cumulativeFactor = 1L;
				for (var target = source - 1; target >= 0; target--)
				{
					cumulativeFactor *= timingFactors[target];
					matrix[source][target] = cumulativeFactor;
				}
			}

			return matrix;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="source"></param>
		/// <param name="duration"></param>
		/// <param name="target"></param>
		/// <returns></returns>
		public static long Convert(this TimeUnit source, long duration, TimeUnit target)
		{
			if (source == target) return duration;

			var sourceIndex = (int)source;
			var targetIndex = (int)target;

			var result = (sourceIndex > targetIndex) ?
				duration * _conversionMatrix[sourceIndex][targetIndex] :
				duration / _conversionMatrix[targetIndex][sourceIndex];

			return result;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="source"></param>
		/// <param name="interval"></param>
		/// <returns></returns>
		public static long ToNanos(this TimeUnit source, long interval)
		{
			return Convert(source, interval, TimeUnit.Nanoseconds);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="source"></param>
		/// <param name="interval"></param>
		/// <returns></returns>
		public static long ToMicros(this TimeUnit source, long interval)
		{
			return Convert(source, interval, TimeUnit.Microseconds);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="source"></param>
		/// <param name="interval"></param>
		/// <returns></returns>
		public static long ToMillis(this TimeUnit source, long interval)
		{
			return Convert(source, interval, TimeUnit.Milliseconds);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="source"></param>
		/// <param name="interval"></param>
		/// <returns></returns>
		public static long ToSeconds(this TimeUnit source, long interval)
		{
			return Convert(source, interval, TimeUnit.Seconds);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="source"></param>
		/// <param name="interval"></param>
		/// <returns></returns>
		public static long ToMinutes(this TimeUnit source, long interval)
		{
			return Convert(source, interval, TimeUnit.Minutes);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="source"></param>
		/// <param name="interval"></param>
		/// <returns></returns>
		public static long ToHours(this TimeUnit source, long interval)
		{
			return Convert(source, interval, TimeUnit.Hours);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="source"></param>
		/// <param name="interval"></param>
		/// <returns></returns>
		public static long ToDays(this TimeUnit source, long interval)
		{
			return Convert(source, interval, TimeUnit.Days);
		}
	}
}

