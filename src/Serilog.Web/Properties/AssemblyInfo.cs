using System.Reflection;
using System.Web;
using Serilog.Web;

[assembly: AssemblyTitle("Serilog.Web")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyCopyright("Copyright © Nicholas Blumhardt 2013")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

[assembly: PreApplicationStartMethod(typeof(ApplicationLifecycleModule), "Register")]
