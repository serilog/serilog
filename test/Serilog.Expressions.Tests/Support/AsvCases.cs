namespace Serilog.Expressions.Tests.Support;

// "Arrow-separated values ;-) ... convenient because the Unicode `⇶` character doesn't appear in
// any of the cases themselves, and so we ignore any need for special character escaping (which is
// troublesome when the language the cases are written in uses just about all special characters somehow
// or other!).
//
// The ASV format informally supports `//` comment lines, as long as they don't contain the arrow character.
static class AsvCases
{
    static readonly string CasesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory!, "Cases");

    public static IEnumerable<object[]> ReadCases(string filename)
    {
        return from line in File.ReadLines(Path.Combine(CasesPath, filename))
            select line.Split("⇶", StringSplitOptions.RemoveEmptyEntries) into cols
            where cols.Length == 2
            select cols.Select(c => c.Trim()).ToArray<object>();
    }
}