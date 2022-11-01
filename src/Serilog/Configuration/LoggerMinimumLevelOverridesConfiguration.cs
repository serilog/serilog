namespace Serilog.Configuration;

/// <summary/>
public class LoggerMinimumLevelOverridesConfiguration
{
    readonly LoggerConfiguration _loggerConfiguration;
    readonly LevelOverrideMapWrapper _wrapper;

    internal LoggerMinimumLevelOverridesConfiguration(LoggerConfiguration loggerConfiguration, LevelOverrideMapWrapper wrapper)
    {
        _loggerConfiguration = Guard.AgainstNull(loggerConfiguration);
        _wrapper = Guard.AgainstNull(wrapper);
    }

    /// <summary/>
    public LoggerConfiguration Add(string source, LoggingLevelSwitch levelSwitch)
    {
        Guard.AgainstNull(source);
        Guard.AgainstNull(levelSwitch);

        var trimmed = source.Trim();
        if (trimmed.Length == 0) throw new ArgumentException($"A {nameof(source)} must be provided.", nameof(source));

        _wrapper.Add(trimmed, levelSwitch);

        return _loggerConfiguration;
    }

    /// <summary/>
    public LoggerConfiguration Set(IDictionary<string, LoggingLevelSwitch> overrides)
    {
        Guard.AgainstNull(overrides);

        Dictionary<string, LoggingLevelSwitch> trimmedOverrides = new();

        foreach (var levelOverride in overrides)
        {
            var trimmed = levelOverride.Key.Trim();
            if (trimmed.Length == 0) throw new ArgumentException("A source must be provided.", nameof(overrides));

            trimmedOverrides.Add(trimmed, levelOverride.Value);
        }

        _wrapper.Replace(trimmedOverrides);

        return _loggerConfiguration;
    }
}
