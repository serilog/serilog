namespace Serilog.Tests.Support;

public class TemporarySelfLog : IDisposable
{
    TemporarySelfLog(Action<string> output)
    {
        SelfLog.Enable(output);
    }

    public void Dispose()
    {
        SelfLog.Disable();
    }

    public static IDisposable SaveTo(List<string> target)
    {
        Guard.AgainstNull(target);
        return new TemporarySelfLog(target.Add);
    }
}
