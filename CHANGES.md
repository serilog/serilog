2.7.1
 * #1075 - overload of `LoggerSinkConfiguration.Wrap()` accepting `LoggingLevelSwitch`
 * #1083 - update package metadata, including `https://` project and icon URLs
 * #1091 - make `{Properties:j}` work consistently with the console sink
 * #1110 - expose an efficient `Logger.None`
 * #1113 - (tooling) improve tests for `LogContext`
 * #1136 - (tooling) fix `build.sh` exit code
 * #1141 - fix logging of `byte[]` smaller than 1 KB
 * #1157 - (tooling) update _.gitignore_
 * #1158 - (tooling) re-enable macOS builds
 * #1163 - SourceLink v2 support
 * #1165 - fix a number of issues with `LoggerSinkConfiguration.Wrap()` affecting `WriteTo.Async()`
 * #1134 - (tooling) add README badges for downloads/stats
 * #1166 - allow `LoggerConfiguration` to be garbage collected after logger is created
 * #1160 - (tooling) update NuGet.org API key
 * (tooling) Update to use the latest .NET SDK

2.6.0
 * #992 - fix `LogContext` remoting issue on .NET Framework 4.5
 * #1007 - `LogContext.Suspend()` and `LogContext.Reset()` to pass or drop context from child tasks
 * #1018 - include source in NUPKGs
 * #1020 - support for `LoggingLevelSwitch` in key-value/`<appSettings>` settings
 * #1033 - warn when `MinimumLevel.Override()` is used in a sub-logger
 * #1034 - more efficient levelled `ILogger.ForContext()`/`Log.ForContext()`
 * #1051 - handle duplicate keys in key-value/`<appSettings>` settings (last-in wins)
 * #1064 - support static member references as key-value/`<appSettings>` values
 * #1059 - support for abstract class parameters in settings
 * #1068 - handle parsing of token width more robustly
 * Plus build/documentation items #988, #994, #996, #1019, #842, #1042, #1050, #1055, #1063.

2.5.0
 * #939, #946, #972 - RTM .NET Standard/Core tooling
 * #955 - `LoggerSinkConfiguration.Wrap()`
 * #944 - `{Properties}` in output templates
 * #961 - fix parser exeception when property tokens with zero-length names are encountered
 * #773 - `LogContext.Clone()` and `LogContext.Push(ILogEventEnricher)`
 * #976 - support for logging C# 7 `ValueTuple<>` values
 * #977 - output formatting performance improvements, `{Message:l}` (unquoted string) and `{Message:j}` (JSON structure) support in output templates
 * #981 - reduced allocations required for structured data capturing

Plus multiple build/test/configuration improvements.2.4.0
* #866 and #877- additional event payload limiting controls
* #833 - improve performance of message template cache lookup
* #885 - fix JSON formatting of `NaN` and infinity values
* #888 - allow minimum level overrides to be specified by configuration providers like _Serilog.Settings.AppSettings_
* #903 - add further `Log` static methods to match `ILogger` methods
* #907 - properly dispose audit sinks
* #913 - include commit hash in `AssemblyInformationalVersion`
* #925 - allow configuration providers to specify `filter` directives
* Build and test coverage work in #821, #824, #896.

2.3.0
 * #870 - fix dispose for level-restricted sinks
 * #852 - fix dictionary capturing when key/value are anonymous types
 * #835 - avoid `RemotingException` via `LogContext`
 * #855 -  allow custom enum rendering (better `ICustomFormatter` support
 * #841 - `audit-to` in key-value settings

2.2.1
 * #835 (fix for .NET 4.6+ only)

2.2.0
 * #826 - audit-style logging
 * #819 - deprecate virtual extension points on JsonFormatter
 
2.1.0
 * #782 - provide `Destructure.ByTransformingWhere()`
 * #779 - capture additional parameters even when template is malformed
 * #798 - fix overload selection in `KeyValueSettings`
 * #815 - allow level to be lowered by overrides, in addition to being raised

2.0.0
 * #535 - .NET Core support; split sinks, enrichers and settings providers into standalone packages; refactor to eliminate _Serilog.FullNetFx.dll_; remove .NET 4.0 support
 * #566 - `Log.CloseAndFlush()`
 * #561 - detect and throw exception if `LoggerConfiguration.CreateLogger()` is called more than once
 * #617 - update project to use C# 6 where appropriate
 * Use `R` round-trip formatting when converting floating point values to JSON
 * #623 - [PeriodicBatchingSink](https://github.com/serilog/serilog-sinks-periodicbatching) - reduce contention when enqueueing events to `PeriodicBatchingSink`
 * #618 - correctly handle VB.NET anonymous types when destructuring
 * #636 - support more enrichment scenarios in `<appSettings>`
 * #588 - enable `LogContext` on all target platforms
 * #654 - improved `WriteTo.Logger()` level and switch handling
 * #656 - [FileSink](https://github.com/serilog/serilog-sinks-file) - support buffered writes in file-based sinks
 * #649 - [ColoredConsoleSink](https://github.com/serilog/serilog-sinks-coloredconsole) - do not print empty properties in colored console output
 * #719 - concrete, `IDisposable` return value from `LoggerConfiguration.CreateLogger()`
 * #727 - remove the unimplementable `IScalarConversionPolicy` interface
 * #728 - allow user-specified destructuring of reflection types
 * #711 and #765 - formatting for short, all-upper/all-lowercase and fixed-width level names in output templates
 * #747 - `ForContext()` performance improvements
 * #752 - zero-allocation generic methods on `ILogger` for up to three args
 * #746 - add `BindMessageTemplate()` and `BindProperty()` to `ILogger` 
 * #731 - `SelfLog` API improvements
 * #754 - `MinimumLevel.Override()` per-source level overrides
 * #770 - message template parser optimization
 * #769 - _BenchmarkDotNet_ performance tests for solution
 * #760 - TravisCI build on Mac OS and Linux
 * #772 - included `LogEventPropertyValueVisitor` and `JsonValueFormatter`
 * #776 - message template construction optimization
 * #733 - use `ObjectHandle` to wrap `LogContext` stack on remoting-capable targets; allows cross-`AppDomain` calls without `Suspend()` or `PermitCrossAppDomainCalls`, both removed
 * #781 - allow interface-typed values to be set in `<appSettings>` configuration by specifying an implementation type name

1.5.14
 * #567 - allow literal formatting to be overridden in `JsonFormatter`

1.5.13
 * #579 - fixed `NullReferenceException` in some `RollingFileSink` failure modes
 * #550 - support custom prefixes in `ReadFrom.AppSettings()` configuration
 * #570 - added `Enrich.WithEnvironmentUserName()`
 * #562 - turn logging off when no sinks are configured

1.5.12
 * #538 - mark assemblies as CLS compliant
 * #532 - `WriteTo.Logger()` improvements for sub-pipelines

1.5.11
 * #523 - prevent excessive RAM use when large strings are cached as message templates

1.5.10
 * #514 - provide `OnEmptyBatch()` to allow batching sinks to perform background work

1.5.9
 * #491 - enable `ReadFrom.KeyValuePairs()` in .NET 4.0 build

1.5.8
 * #484 - generic overloads on `Log` to avoid boxing/allocation

1.5.7
 * #456 - support `ReadFrom.AppSettings()` and friends on .NET 4.0

1.5.6
 * Builds on `master` now derive their version from `CHANGES.md` (rather than vice-versa)
 * #441 - Fix conversion of `Nullable<>` settings

1.5.5
 * #433 - Revert the default destructuring depth back to 10

1.5.1
 * #402 - `<appSettings>` configuration support now in the Serilog package

1.4.214
 * #344 - Moved "Extras" including Web, Owin, F#, destructuring to new organisations

1.4.204
 * #344 (partial) - Moved remaining sinks to individual repositories

1.4.196
 * #393 - Removed the Splunk sink for move to https://github.com/serilog/serilog-sinks-splunk
 * #389 - Fixed the default URI in the ES sink
 * #385 - Provided `IndexDecider` option to ES sink configuration

1.4.182
 * #382 - Fixed `CounterMeasure` counting in _Serilog.Extras.Timing_
 * #386 - Expand environment variables on _Serilog.Extras.AppSettings_ values
 * #387 - Elmah.io sink dependency version updated

1.4.168
 * #376 - Fixed flushing of async events to Loggly sink (moved to serilog/serilog-sinks-loggly)
 * #374 - Run destructuring policies before converting `IEnumerable` types

1.4.152
 * #196 - `LogContext.PermitCrossAppDomainCalls` property to prevent serialization exceptions in test frameworks and when .NET remoting is used
 * #365 - _Serilog.Sinks.ApplicationInsights_ now targets the new Azure Portal-based version (preview)
 * #367 - _Serilog.Sinks.MongoDB_ now targets the new preview driver (preview)
 * #369 - Fixed log message property in .NET 4.0 build of Elasticsearch sink
 * #373 - Update the _Raygun_ sink from 2.0.4 to 4.2.0
 * #344 - Moved the _MongoDB_ and _Application Insights_ sinks out to independent repositories

1.4.139
 * #125 - Merged _MonoTouch_ and _MonoAndroid_ sinks (**not currently published  to NuGet**)
 * #362 - Update Loggly sink package dependencies
 * #203 - _Azure Document DB_ sink

1.4.126
 * #354 - Added _Serilog.Extras.FSharp_

1.4.118
 * #351 - Azure Event Hubs sink

1.4.113
 * #329 - Write event properties as columns using the new `WriteTo.AzureTableStorageWithProperties()`
 * #346 - Pass all properties through to _Serilog.Sinks.NLog_ as context properties
 * #347 - Update _Serilog.Sinks.SignalR_ to SignalR version 2.1.0 (breaking)

1.4.99
 * #342 - Accept `ILogEventEnricher` rather than just `PropertyEnricher` to `ForContext`
 * #341 - Selectable logging level for request details in _Serilog.Extras.Web_

1.4.92
 * #340 - Accept an `Encoding` parameter on the file sinks
 * #336 - Use stream when writing to Splunk via HTTP
 * #335 - Remove use of `dynamic` when destructuring `Nullable<T>` (iOS)

1.4.65
 * #321 - Revert ES sink message property name to original value
 * #324 - Calculate ES index name from UTC timestamp
 * #315 - Remove Logentries SSL cert pinning (the cert changed)
 * #327 - Hide obsolete ES configuration methods
 * #311 - Accept multiple recipient addresses in the email sink

1.4.39
 * #289 - Elasticsearch sink improvements and new virtual methods on `JsonFormatter`

1.4.33
 * #302 - If a the last batch of log events sent by `PeriodicBatchingSink` was not full, wait the `period`
 * #304 - Make sure IO exceptions are suppressed by the `WriteTo.File()` configuration method

1.4.28
 * #303 - `AssemblyInformationalVersion` needs to track the package, not assembly, version, in order to play nicely with MSI

1.4.27
 * #283 - New solution targeting ASP.NET v.NEXT (when final, we'll refactor to give this first-class support)
 * #301 - _Serilog.Extras.Web_ improvements

1.4.23
 * Fixed some more NuGet packaging issues (_Serilog.Sinks.Splunk_ this time)

1.4.22
 * #297 - Update _loggly-csharp_ package dependency

1.4.21
 * Fixed some NuGet packaging issues (including a return to dependency groups for _Serilog.Sinks.Seq_)

1.4.18
1.4.17
1.4.16
 * New CI server used
 * #227 - Reduced default maximum destructuring depth and added configuration option
 * #245 - XML content escaping fixed in MSSQL sink
 * #272 - Allow underscores in property names

1.4.15
 * #259 - Update _loggly-sharp_ dependency (breaking change for _Serilog.Sinks.Loggly_, see PR for instructions)
 * #257 - Added _Serilog.Sinks.XSockets_
 * #246 - Changed target from _Profile78_ to (compatible but broader) _Profile259_
 * #250 - Allow an existing `MongoDatabase` instance to be used when configuring Mongo sink

1.4.14
 * #253 - Added `MessageTemplateFormatMethodAttribute`

1.4.13
 * #244 - Improvements to ElasticSearch sink, switch to just ElasticSearch.NET
 * #254 - Fix ElasticSearch dependency version
 * #249 - Track message template token indexes in parser to support tooling (binary-breaking)

1.4.12
 * #240 - Update TopShelf dependency
 * #243 - Caching in `AttributedDestructuringPolicy` closes over first seen object of a type rather than using the passed parameter
 * #242 - Update NLogSink to map `Verbose` level to `Trace` level in NLog
 * #229 - Created net40 version of Extras Topshelf
 * #236 - Ensure dictionary keys are quoted in JSON even when they're numeric
 * #237 - When `JsonFormatter` formats a dictionary of `<int, object>`, the key should be double quoted
 * #235 - Fixed serialization of dicationary keys in ES and similar sinks

1.4.11
 * #238 - Dynamic level switching

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
