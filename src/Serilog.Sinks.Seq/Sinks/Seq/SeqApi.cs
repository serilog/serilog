using System;
using Serilog.Events;

namespace Serilog.Sinks.Seq
{
    class SeqApi
    {
        // Why not use a JSON parser here? For a very small case, it's not
        // worth taking on the extra payload/dependency management issues that
        // a full-fledged parser will entail. If things get more sophisticated
        // we'll reevaluate.
        const string LevelMarker = "\"MinimumLevelAccepted\":\"";

        public static LogEventLevel? ReadEventInputResult(string eventInputResult)
        {
            if (eventInputResult == null) return null;

            // Seq 1.5 servers will return JSON including "MinimumLevelAccepted":x, where
            // x may be null or a JSON string representation of the equivalent LogEventLevel
            var startProp = eventInputResult.IndexOf(LevelMarker, StringComparison.Ordinal);
            if (startProp == -1)
                return null;

            var startValue = startProp + LevelMarker.Length;
            if (startValue >= eventInputResult.Length)
                return null;

            var endValue = eventInputResult.IndexOf('"', startValue);
            if (endValue == -1)
                return null;

            var value = eventInputResult.Substring(startValue, endValue - startValue);
            LogEventLevel minimumLevel;
            if (!Enum.TryParse(value, out minimumLevel))
                return null;

            return minimumLevel;
        }
    }
}