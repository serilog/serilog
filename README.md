# Serilog [![Build status](https://ci.appveyor.com/api/projects/status/b9rm3l7kduryjgcj/branch/dev?svg=true)](https://ci.appveyor.com/project/serilog/serilog/branch/dev) [![NuGet Version](http://img.shields.io/nuget/v/Serilog.svg?style=flat)](https://www.nuget.org/packages/Serilog/) [![NuGet Downloads](https://img.shields.io/nuget/dt/serilog.svg)](https://www.nuget.org/packages/Serilog/) [![Stack Overflow](https://img.shields.io/badge/stack%20overflow-serilog-orange.svg)](http://stackoverflow.com/questions/tagged/serilog)

Serilog is a diagnostic logging library for .NET applications. It is easy to set up, has a clean API, and runs on all recent .NET platforms. While it's useful even in the simplest applications, Serilog's support for structured logging shines when instrumenting complex, distributed, and asynchronous applications and systems.

[![Serilog](https://raw.githubusercontent.com/serilog/serilog.github.io/master/images/serilog-180px.png)](https://serilog.net)

Like many other libraries for .NET, Serilog provides diagnostic logging to [files](https://github.com/serilog/serilog-sinks-file), the [console](https://github.com/serilog/serilog-sinks-console), and [many other outputs](https://github.com/serilog/serilog/wiki/Provided-Sinks).

```csharp
using var log = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("log.txt")
    .CreateLogger();

log.Information("Hello, Serilog!");
```

Unlike other logging libraries, Serilog is built from the ground up to record _structured event data_.

```csharp
var position = new { Latitude = 25, Longitude = 134 };
var elapsedMs = 34;

log.Information("Processed {@Position} in {Elapsed} ms", position, elapsedMs);
```

Serilog uses [message templates](https://messagetemplates.org), a simple DSL that extends .NET format strings with _named_ as well as positional parameters. Instead of formatting events immediately into text, Serilog captures the values associated with each named parameter.

The example above records two properties, `Position` and `Elapsed`, in the log event. The `@` operator in front of `Position` tells Serilog to _serialize_ the object passed in, rather than convert it using `ToString()`. Serilog's deep and rich support for structured event data opens up a huge range of diagnostic possibilities not available when using traditional loggers.

Rendered into [JSON format](https://github.com/serilog/serilog-formatting-compact) for example, these properties appear alongside the timestamp, level, and message like:

```json
{"Position": {"Latitude": 25, "Longitude": 134}, "Elapsed": 34}
```

Back-ends that are capable of recording structured event data make log searches and analysis possible without log parsing or regular expressions.

Supporting structured data doesn't mean giving up text: when Serilog writes events to files or the console, the template and properties are rendered into friendly human-readable text just like a traditional logging library would produce:

```
09:14:22 [INF] Processed {"Latitude": 25, "Longitude": 134} in 34 ms.
```

> **Upgrading from an earlier Serilog version?** Find [release notes here](https://github.com/serilog/serilog/releases).

### Features

 * **Community-backed and actively developed**
 * Format-based logging API with familiar [levels](https://github.com/serilog/serilog/wiki/Configuration-Basics#minimum-level) like `Debug`, `Information`, `Warning`, `Error`, and so-on
 * Discoverable C# configuration syntax and optional [XML](https://github.com/serilog/serilog-settings-appsettings) or [JSON](https://github.com/serilog/serilog-settings-configuration) configuration support
 * Efficient when enabled, extremely low overhead when a logging level is switched off
 * Best-in-class .NET Core support, including [rich integration with ASP.NET Core](https://github.com/serilog/serilog-aspnetcore)
 * Support for a [comprehensive range of sinks](https://github.com/serilog/serilog/wiki/Provided-Sinks), including files, the console, on-premises and cloud-based log servers, databases, and message queues
 * Sophisticated [enrichment](https://github.com/serilog/serilog/wiki/Enrichment) of log events with contextual information, including scoped (`LogContext`) properties, thread and process identifiers, and domain-specific correlation ids such as `HttpRequestId`
 * Zero-shared-state `Logger` objects, with an optional global static `Log` class
 * Format-agnostic logging pipeline that can emit events in plain text, JSON, in-memory `LogEvent` objects (including [Rx pipelines](https://github.com/serilog/serilog-sinks-observable)) and other formats

### Getting started

Serilog is installed [from NuGet](https://nuget.org/packages/serilog). To view log events, one or more sinks need to be installed as well, here we'll use the pretty-printing console sink, and a rolling file set:

```
dotnet add package Serilog
dotnet add package Serilog.Sinks.Console
dotnet add package Serilog.Sinks.File
```

The simplest way to set up Serilog is using the static `Log` class. A `LoggerConfiguration` is used to create and assign the default logger, normally in _Program.cs_:

```csharp
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("log.txt",
        rollingInterval: RollingInterval.Day,
        rollOnFileSizeLimit: true)
    .CreateLogger();

try
{
    // Your program here...
    const string name = "Serilog";
    Log.Information("Hello, {Name}!", name);
    throw new InvalidOperationException("Oops...");
}
catch (Exception ex)
{
    Log.Error(ex, "Unhandled exception");
}
finally
{
    await Log.CloseAndFlushAsync();
}
```

Find more, including a runnable example application, under the [Getting Started topic](https://github.com/serilog/serilog/wiki/Getting-Started) in the [documentation](https://github.com/serilog/serilog/wiki/).

### Getting help

To learn more about Serilog, check out the [documentation](https://github.com/serilog/serilog/wiki) - you'll find information there on the most common scenarios. If Serilog isn't working the way you expect, you may find the [troubleshooting guide](https://github.com/serilog/serilog/wiki/Debugging-and-Diagnostics) useful.

Serilog has an active and helpful community who are happy to help point you in the right direction or work through any issues you might encounter. You can get in touch via:

 * [Stack Overflow](http://stackoverflow.com/questions/tagged/serilog) &mdash; this is the best place to start if you have a question
 * The [#serilog tag on Twitter](https://twitter.com/search?q=%23serilog)
 * [Serilog-related courses on Pluralsight](https://www.pluralsight.com/search/?q=serilog)

We welcome bug reports and suggestions through our [issue tracker](https://github.com/serilog/serilog/issues) here on GitHub.

### Contributing

Would you like to help make Serilog even better? We keep a list of issues that are approachable for newcomers under the [up-for-grabs](https://github.com/issues?utf8=✓&q=is%3Aopen+is%3Aissue+archived%3Afalse+user%3Aserilog+label%3Aup-for-grabs) label (accessible only when logged into GitHub). Before starting work on a pull request, we suggest commenting on, or raising, an issue on the issue tracker so that we can help and coordinate efforts.  For more details check out our [contributing guide](CONTRIBUTING.md).

When contributing please keep in mind our [Code of Conduct](CODE_OF_CONDUCT.md).

### Detailed build status

| Branch | AppVeyor                                                                                                                                                              |
|--------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `dev`  | [![Build status](https://ci.appveyor.com/api/projects/status/b9rm3l7kduryjgcj/branch/dev?svg=true)](https://ci.appveyor.com/project/serilog/serilog/branch/dev)       |
| `main` | [![Build status](https://ci.appveyor.com/api/projects/status/b9rm3l7kduryjgcj/branch/master?svg=true)](https://ci.appveyor.com/project/serilog/serilog/branch/master) |

_Serilog is copyright &copy; 2013-2020 Serilog Contributors - Provided under the [Apache License, Version 2.0](http://apache.org/licenses/LICENSE-2.0.html). Needle and thread logo a derivative of work by [Kenneth Appiah](http://www.kensets.com/)._
