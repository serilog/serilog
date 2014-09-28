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

namespace Serilog.Extras.Timing
{

	/// <summary>
	/// The result data of a health check.
	/// </summary>
	public class HealthCheckResult{

		/// <summary>
		/// Initializes a new instance of the <see cref="Serilog.Extras.Timing.HealthCheckResult"/> class.
		/// </summary>
		public HealthCheckResult ()
		{
			IsHealthty = true;
			Message = "successfull";
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Serilog.Extras.Timing.HealthCheckResult"/> class.
		/// </summary>
		/// <param name="isHealthy">If set to <c>true</c> is healthy.</param>
		/// <param name="message">Message.</param>
		/// <param name="exception">Exception.</param>
		public HealthCheckResult (bool isHealthy, string message = null, Exception exception = null)
		{
			IsHealthty = isHealthy;
			Message = message;
			Exception = exception;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Serilog.Extras.Timing.HealthCheckResult"/> class.
		/// </summary>
		/// <param name="message">Message.</param>
		public HealthCheckResult (string message)
		{
			IsHealthty = false;
			Message = message;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Serilog.Extras.Timing.HealthCheckResult"/> class.
		/// </summary>
		/// <param name="message">Message.</param>
		/// <param name="exception">Exception.</param>
		public HealthCheckResult (string message, Exception exception)
		{
			IsHealthty = false;
			Message = message;
			Exception = exception;
		}

		/// <summary>
		/// Message of the result.
		/// </summary>
		/// <value>The message.</value>
		public string Message {
			get;
			internal set;
		}

		/// <summary>
		/// A possible exception.
		/// </summary>
		/// <value>The exception.</value>
		public Exception Exception {
			get;
			internal set;
		}

		/// <summary>
		/// Indicates if the result of the check is indeed healthy or not.
		/// </summary>
		/// <value><c>true</c> if this instance is healthty; otherwise, <c>false</c>.</value>
		public bool IsHealthty {
			get;
			internal set;
		}
	}
}