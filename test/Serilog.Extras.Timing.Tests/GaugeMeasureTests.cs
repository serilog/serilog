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
	public class GaugeMeasureTests
	{
		LogEvent _eventSeen;

		public GaugeMeasureTests ()
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
		public void GaugeShouldReturnMeasure ()
		{
			var queue = new Queue<int>();
			var gauge = Log.Logger.GaugeOperation("queue", "item(s)", () => queue.Count);

			gauge.Write ();
			Assert.AreEqual ("\"queue\" value = 0 item(s)", _eventSeen.RenderMessage());

			queue.Enqueue (1);
			queue.Enqueue (1);

			gauge.Write ();
			Assert.AreEqual ("\"queue\" value = 2 item(s)", _eventSeen.RenderMessage());

			queue.Dequeue ();

			gauge.Write ();
			Assert.AreEqual ("\"queue\" value = 1 item(s)", _eventSeen.RenderMessage());

			queue.Clear ();

			gauge.Write ();
			Assert.AreEqual ("\"queue\" value = 0 item(s)", _eventSeen.RenderMessage());

		}
	}
	
}
