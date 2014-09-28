using NUnit.Framework;
using System;

namespace Serilog.Extras.Timing.Tests
{
	[TestFixture ()]
	public class HealthMeasureTests
	{
		[Test ()]
		public void HealthyCheckResult ()
		{
			var configuration = new LoggerConfiguration();
			var logger = configuration.MinimumLevel.Verbose().WriteTo.Trace().CreateLogger ();

			var check = logger.HealthCheck ("test-healthy", () => new HealthCheckResult ());

			check.Write ();

		}

		[Test ()]
		public void UnHealthyCheckResult ()
		{
			var configuration = new LoggerConfiguration();
			var logger = configuration.MinimumLevel.Verbose().WriteTo.Trace().CreateLogger ();

			var check = logger.HealthCheck ("test-unhealthy", () => new HealthCheckResult ("something was wrong", new ArgumentException()));

			check.Write ();

		}
	}
}

