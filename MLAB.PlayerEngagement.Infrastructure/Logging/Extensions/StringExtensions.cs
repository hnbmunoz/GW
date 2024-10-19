namespace MLAB.PlayerEngagement.Infrastructure.Logging.Extensions;

internal static class StringExtensions
{
    internal static bool EqualsOrdinalIgnoreCase(this string text, string text2)
    {
        return string.Equals(text, text2, StringComparison.OrdinalIgnoreCase);
    }
}
