global using System.Collections;
global using System.ComponentModel;
global using System.Diagnostics.CodeAnalysis;
global using System.Globalization;
global using System.Reflection;
global using System.Runtime.CompilerServices;
global using System.Text;
global using System.Text.RegularExpressions;
global using System.Threading;
global using Serilog.Capturing;
global using Serilog.Configuration;
global using Serilog.Context;
global using Serilog.Core;
global using Serilog.Core.Enrichers;
global using Serilog.Core.Filters;
global using Serilog.Core.Pipeline;
global using Serilog.Core.Sinks;
global using Serilog.Data;
global using Serilog.Debugging;
global using Serilog.Events;
global using Serilog.Formatting.Display.Obsolete;
global using Serilog.Formatting.Json;
global using Serilog.Parsing;
global using Serilog.Policies;
global using Serilog.Rendering;
global using Serilog.Settings.KeyValuePairs;

#if FEATURE_REMOTING
global using System.Runtime.Remoting;
global using System.Runtime.Remoting.Lifetime;
global using System.Runtime.Remoting.Messaging;
#endif
