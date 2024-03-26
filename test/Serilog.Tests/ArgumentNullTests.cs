using System.Runtime.CompilerServices;
using File = System.IO.File;

namespace Serilog.Tests;

public class ArgumentNullTests
{
    [Fact]
    public void Run()
    {
        var solutionDirectory = GetSolutionDirectory();
        var serilogDirectory = Path.Combine(solutionDirectory, "src/Serilog");

        foreach (var csFile in Directory.EnumerateFiles(serilogDirectory, "*.cs"))
        {
            if (Path.GetFileName(csFile) == "Guard.cs")
            {
                continue;
            }

            if (!File.ReadAllText(csFile).Contains("throw new ArgumentNullException"))
            {
                continue;
            }

            throw new($"Don't throw ArgumentNullException directly. Instead use Guard.AgainstNull method. Path: {csFile}");
        }
    }

    static string GetSolutionDirectory([CallerFilePath] string file = "")
    {
        var currentDirectory = new FileInfo(file).Directory!.FullName;
        do
        {
            if (Directory.GetFiles(currentDirectory, "*.sln").Any())
            {
                return currentDirectory;
            }

            var parent = Directory.GetParent(currentDirectory);
            if (parent is null)
            {
                break;
            }

            currentDirectory = parent.FullName;
        } while (true);

        throw new();
    }
}
