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
using Serilog.Context;
using System.Collections.Generic;

namespace Serilog.Extras.Timing.Tests
{
	[TestFixture ()]
	public class MeterMeasureTests
	{
		LogEvent _eventSeen;

		public MeterMeasureTests ()
		{
			var configuration = new LoggerConfiguration();
			var logger = configuration
				.MinimumLevel.Verbose()               // Make sure we see also the lowest level
				.WriteTo.Observers(events => events   // So we can check the result
					.Do(evt => { _eventSeen = evt; })
					.Subscribe())
				.WriteTo.Console()                    // Still visible in the unit test console
				.Enrich.FromLogContext()
				.CreateLogger();

			Log.Logger = logger;
		}


		[Test ()]
		public void MeterShouldWriteOutput ()
		{
			var meter = Log.Logger.MeterOperation("server load", "requests", TimeUnit.Seconds);

			meter.Mark ();

			meter.Write ();

			Assert.IsTrue (_eventSeen.RenderMessage ().Contains ("\"server load\" count = 1, "));


			System.Threading.Thread.Sleep (2000);

			meter.Mark (2);

			meter.Write ();

			Assert.IsTrue (_eventSeen.RenderMessage ().Contains ("\"server load\" count = 3, "));

			//Wait a minute
			System.Threading.Thread.Sleep (60000);

			meter.Mark (2);

			meter.Write ();

			Assert.IsTrue (_eventSeen.RenderMessage ().Contains ("\"server load\" count = 5, "));

			Assert.AreEqual (5, meter.Count);
		}
	}
	
}
