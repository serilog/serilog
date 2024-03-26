namespace TestDummies;

public class DummyAnonymousUserFilter : ILogEventFilter
{
    public bool IsEnabled(LogEvent logEvent)
    {
        if (logEvent.Properties.ContainsKey("User"))
        {
            if (logEvent.Properties["User"] is ScalarValue sv)
            {
                if (sv.Value is "anonymous")
                {
                    return false;
                }
            }
        }

        return true;
    }
}
