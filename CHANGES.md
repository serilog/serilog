1.4.10
 * #225 - Ensure Azure Tablestorage rowkey is unique
 * #224 - Use UTC to generate partitionkey
 * #219 - .NET 4.0 support for Elastic Search
 * #221 - Exponential back-off for PeriodicBatchingSink

1.4.9
 * #213 - Check for null User.Identity when enriching events with usernames in ASP.NET
 * #216 - Updated to use Splunk TCP Writer

1.4.8
 * #204 - Basic HTTP authentication support for the CouchDB sink
 * #207 - Fixed typo in `DepthLimiter` error message
 * #209 - Update _Serilog.Sinks.Splunk_ to Splunk SDK 2.0 GA
 * #210 - Fixed string formatting in `PropertyBinder` error message

1.4.7
 * #192 - Additional enrichers for _Serilog.Extras.Web_
 * #202 - Batching mode for _Serilog.Sinks.AzureTableStorage_

1.4.6
 * Reintroduced fix from https://github.com/continuousit/seq-client/pull/19 dropped in project move

1.4.5
 * #179 - _Serilog.Sinks.Splunk_ updated for the Splunk SDK 2.0

1.4.4
  * Includes the _Serilog.Sinks.Seq_ sink, to write to the http://getseq.net event server

1.4.3
  * #169 - Added the _Serilog.Extras.DestructureByIgnoring_ package

1.4.2
  * #197 - Allow `EventSource` creation to be skipped when the source does not exist

1.4.1
  * #196 - Introduce `LogContext.Suspend()` to clear `LogicalCallContext` for cross-domain calls
