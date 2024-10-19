
using System.Text.RegularExpressions;
namespace MLAB.PlayerEngagement.Infrastructure.Utilities
{
    public class CurrencyConverter
    {
        // Define a regex pattern to match the currency code
        private static readonly string pattern = @"^([A-Z]+)\s*\(";

        /// <summary>
        /// Extracts the currency code from a given input string.
        /// </summary>
        /// <param name="inputString">The input string containing the currency code.</param>
        /// <returns>The extracted currency code if found; otherwise, null.</returns>
        public static string ExtractCurrencyCode(string inputString)
        {
            if (string.IsNullOrWhiteSpace(inputString))
            {
                return null;
            }

            // Match the pattern in the input string
            Match match = Regex.Match(inputString, pattern);

            if (match.Success)
            {
                // Extract the currency code from the first capture group
                return match.Groups[1].Value;
            }
            else
            {
                return inputString;
            }
        }
    }
}
