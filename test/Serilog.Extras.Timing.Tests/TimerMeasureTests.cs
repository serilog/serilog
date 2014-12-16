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

namespace Serilog.Extras.Timing.Tests
{

	[TestFixture ()]
	public class TimerMeasureTests
	{
		LogEvent _eventSeen;

		public TimerMeasureTests ()
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
		public void TimedOperationShouldWriteMessages ()
		{
			var check = Log.Logger.BeginTimedOperation("test", "test-id");

			Assert.AreEqual ("Beginning operation \"test-id\": \"test\"", _eventSeen.RenderMessage());

			check.Dispose ();
			Assert.IsTrue (_eventSeen.RenderMessage ().StartsWith ("Completed operation \"test-id\"", StringComparison.Ordinal));

		}

		[Test ()]
		public void OperationThatExceedsTimeShouldRenderMessages ()
		{
			var check = Log.Logger.BeginTimedOperation("test", "test-id", LogEventLevel.Information, TimeSpan.FromMilliseconds(2));

			Assert.AreEqual ("Beginning operation \"test-id\": \"test\"", _eventSeen.RenderMessage());

			// Wait at least 3 milliseconds

			System.Threading.Thread.Sleep (3);

			check.Dispose ();

			Assert.IsTrue (_eventSeen.RenderMessage ().Contains ("exceeded"));
			Assert.AreEqual (LogEventLevel.Warning, _eventSeen.Level);
			Assert.IsTrue (Convert.ToInt32(_eventSeen.Properties ["TimedOperationElapsedInMs"].ToString()) > 3);
			Assert.IsTrue (_eventSeen.Properties.ContainsKey ("WarningLimit"));

		}

		[Test ()]
		public void CanAddAdditionalProperties ()
		{
			var check = Log.Logger.BeginTimedOperation("test", "test-id");

			using (LogContext.PushProperty ("numberOfOperations", 10)) {
			
				Assert.AreEqual ("Beginning operation \"test-id\": \"test\"", _eventSeen.RenderMessage ());
			
				check.Dispose ();
				Assert.IsTrue (_eventSeen.RenderMessage ().StartsWith ("Completed operation \"test-id\"", StringComparison.Ordinal));
			}

			Assert.IsTrue (_eventSeen.Properties.ContainsKey ("numberOfOperations"));
			Assert.IsTrue (_eventSeen.Properties ["numberOfOperations"].ToString() == "10");

		}
	}
	
}
