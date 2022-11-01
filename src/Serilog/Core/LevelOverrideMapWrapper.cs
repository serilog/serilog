namespace Serilog.Core;

class LevelOverrideMapWrapper
{
    volatile LevelOverrideMap? _map;
    IDictionary<string, LoggingLevelSwitch> _overrides = new Dictionary<string, LoggingLevelSwitch>();

    public LevelOverrideMap? Map => _map;

    public void Add(string source, LoggingLevelSwitch levelSwitch)
    {
        _overrides[source] = levelSwitch;
        RefreshMap();
    }

    public void Replace(IDictionary<string, LoggingLevelSwitch> overrides)
    {
        _overrides = overrides;
        RefreshMap();
    }

    public void RefreshMap()
    {
        _map = _map?.WithOverrides(_overrides);
    }

    public void EnsureMap(LogEventLevel defaultMinimumLevel, LoggingLevelSwitch? defaultLevelSwitch)
    {
        if (_overrides.Count > 0)
        {
            _map = new LevelOverrideMap(_overrides, defaultMinimumLevel, defaultLevelSwitch);
        }
    }
}
