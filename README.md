# CitizenFX.Extensions.Client.Serilog
A fork of the [diagnostic logging library known as Serilog](https://serilog.net/), based off [v2.12.0 of Serilog](https://github.com/serilog/serilog/tree/v2.12.0) for compatability with [.net client-side FiveM](https://fivem.net/)

## Example
```csharp
var logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.FiveM()
    .CreateLogger();

logger.Information("Hello from the FiveM client!");
```

## Changes from [v2.12.0 of Serilog](https://github.com/serilog/serilog/tree/v2.12.0)
### 2.12.0-cfx

## Notes
- This fork is in **NO WAY** affiliated with the [Serilog Organization](https://github.com/serilog) or the [Serilog project](https://serilog.net/), it's purely a fork to provide compatability with FiveM's client-resource shipped mono
