namespace TestDummies.Console.Themes;

public abstract class ConsoleTheme
{
    public static ConsoleTheme None { get; } = new EmptyConsoleTheme();
}
