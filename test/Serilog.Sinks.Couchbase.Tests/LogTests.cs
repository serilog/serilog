using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Serilog.Sinks.Couchbase.Tests
{
    [TestFixture]
    public class LogTests
    {
        [Test]
        public void TheUninitializedLoggerIsSilent()
        {
            string[] localUriList = { "http://TA030324:8091/pools", "http://TA030119:8091/pools" };
            string bucketName = "serilogtest";

            Log.Logger = new LoggerConfiguration()
                                .WriteTo.Couchbase(localUriList, bucketName)
                                .CreateLogger();

            for (int i = 0; i < 10; i++)
            {
                Log.Information("Test Log Entry {Number}", i);
                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}
