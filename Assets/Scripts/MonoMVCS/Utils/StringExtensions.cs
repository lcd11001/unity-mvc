public static class StringExtensions
{
    public static string GetWords(this string input)
    {
        var first = System.Text.RegularExpressions.Regex.Replace(input, "([a-z])([A-Z])", "$1 $2");
        var second = System.Text.RegularExpressions.Regex.Replace(first, "([A-Z]+)([A-Z][a-z])", "$1 $2");
        return second;
    }

    // Add more extension methods here...
}