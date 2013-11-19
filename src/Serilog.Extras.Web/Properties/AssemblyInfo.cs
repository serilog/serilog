using System.Reflection;
using System.Web;
using Serilog.Extras.Web;

[assembly: AssemblyTitle("Serilog.Web")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyCopyright("Copyright © Serilog Contributors 2013")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

[assembly: PreApplicationStartMethod(typeof(ApplicationLifecycleModule), "Register")]
