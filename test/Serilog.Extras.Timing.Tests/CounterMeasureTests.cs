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

using NUnit.Framework;
using System;
using Serilog.Events;
using System.Reactive.Linq;

namespace Serilog.Extras.Timing.Tests
{



	[TestFixture ()]
	public class CounterMeasureTests
	{
		LogEvent _eventSeen;

		public CounterMeasureTests ()
		{
			var configuration = new LoggerConfiguration();
			var logger = configuration
				.MinimumLevel.Verbose()               // Make sure we see also the lowest level
				.WriteTo.Observers(events => events   // So we can check the result
					.Do(evt => { _eventSeen = evt; })
					.Subscribe())
				.WriteTo.Console()                    // Still visible in the unit test console
				.CreateLogger();

			Log.Logger = logger;
		}

		[Test ()]
		public void CounterStoresValue ()
		{
			var check = Log.Logger.CountOperation("invocations", "times", false);

			Assert.AreEqual (check.Value (), 0);

			check.Increment ();
			Assert.AreEqual (check.Value (), 1);

			check.Increment ();
			Assert.AreEqual (check.Value (), 2);

			check.Decrement ();
			Assert.AreEqual (check.Value (), 1);

			check.Reset ();
			Assert.AreEqual (check.Value (), 0);

			}

		[Test ()]
		public void CounterWritesResult ()
		{
			var check = Log.Logger.CountOperation("invocations", "times", false);

			Assert.AreEqual (check.Value (), 0);

			check.Increment ();
			Assert.AreEqual (check.Value (), 1);
		
			check.Write ();

			Assert.AreEqual (LogEventLevel.Information, _eventSeen.Level);
			Assert.AreEqual ("\"invocations\" count = 1 times", _eventSeen.RenderMessage ());

		}

		[Test ()]
		public void CounterWithCustomLevelWritesWithThatLevel ()
		{
			var check = Log.Logger.CountOperation("invocations", "times", false, LogEventLevel.Debug);
		

			check.Write ();

			Assert.AreEqual (LogEventLevel.Debug, _eventSeen.Level);

		}

		[Test ()]
		public void CounterWritesDirectResultsToLogger ()
		{
			var check = Log.Logger.CountOperation("invocations", "times", true);

			check.Increment ();
			Assert.AreEqual ("\"invocations\" count = 1 times", _eventSeen.RenderMessage ());

			check.Increment ();
			Assert.AreEqual ("\"invocations\" count = 2 times", _eventSeen.RenderMessage ());

			check.Decrement ();
			Assert.AreEqual ("\"invocations\" count = 1 times", _eventSeen.RenderMessage ());

			check.Reset ();
			Assert.AreEqual ("\"invocations\" count = 0 times", _eventSeen.RenderMessage ());
		}
	}
	
}
