namespace MLAB.PlayerEngagement.Core.Extensions;

public static class DateTimeExtension
{
    public static string ToLocalDateTimeString(this DateTime dt)
    {
        return dt.ToUniversalTime().AddHours(8).ToString("yyyy-MM-dd HH:mm:ss");
    }

    public static string ToMlabExportDateString(this DateTime dt)
    {
        return dt.ToString("dd/MM/yyyy HH:mm:ss");
    }
}
