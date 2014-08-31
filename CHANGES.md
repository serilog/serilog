1.4.6
 * Reintroduced fix from https://github.com/continuousit/seq-client/pull/19 dropped in project move

1.4.5
 * Merged PR #179 - _Serilog.Sinks.Splunk_ updated for the Splunk SDK 2.0

1.4.4
  * Includes the _Serilog.Sinks.Seq_ sink, to write to the http://getseq.net event server

1.4.3
  * Merged PR #169 - added the _Serilog.Extras.DestructureByIgnoring_ package

1.4.2
  * Merged PR #197 - allow `EventSource` creation to be skipped when the source does not exist

1.4.1
  * #196 - Introduce `LogContext.Suspend()` to clear `LogicalCallContext` for cross-domain calls
