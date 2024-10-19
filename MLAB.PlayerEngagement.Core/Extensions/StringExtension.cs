using Newtonsoft.Json.Linq;

namespace MLAB.PlayerEngagement.Core.Extensions;

public static class StringExtension
{
    public static string ReplaceComma(this string str)
    {
        return str.Replace(",", "⸴");
    }
    public static string ToLocalDateTime(this string str)
    {
        DateTime dt;

        if(DateTime.TryParse(str,out dt))
            return dt.ToUniversalTime().AddHours(8).ToString("yyyy-MM-dd HH:mm:ss");

        return null;
    }

    public static string ToMlabExportDateString(this string str)
    {
        DateTime dt;

        if (DateTime.TryParse(str, out dt))
            return dt.ToString("dd/MM/yyyy HH:mm:ss");

        return null;
    }
    public static string CsvQuoteAndReplace(this string value)
    {
        return !string.IsNullOrEmpty(value) ? $"\"{value}\"" : "";
    }
}
