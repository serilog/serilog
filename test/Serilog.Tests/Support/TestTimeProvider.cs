namespace Serilog.Tests.Support;

class TestTimeProvider: TimeProvider
{
    DateTimeOffset _utcNow = DateTimeOffset.UtcNow;

    public override long GetTimestamp()
    {
        return _utcNow.Ticks;
    }

    public override DateTimeOffset GetUtcNow()
    {
        return _utcNow;
    }

    public void Advance(TimeSpan distance)
    {
        _utcNow = _utcNow.Add(distance);
    }

    public override long TimestampFrequency => TimeSpan.TicksPerSecond;
}
