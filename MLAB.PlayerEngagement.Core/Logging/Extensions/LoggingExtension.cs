using MLAB.PlayerEngagement.Core.Enum;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;

namespace MLAB.PlayerEngagement.Core.Logging.Extensions;

public static class LoggingExtension
{
    public static void LogInfo(this ILogger logger, string message)
    {
        logger.Log(LogLevels.Info, message, null, null);
    }

    public static void LogInfoRequest(this ILogger logger, string module, object parameter, string refCode = "",
        [CallerMemberName] string method = "")
    {
        string message = module + "-" + method + "-Request: " + JsonConvert.SerializeObject(parameter);
        logger.Log(LogLevels.Info, message, null, parameter, "Request:", refCode);
    }

    public static void LogInfoResponse(this ILogger logger, string module, object parameter, string refCode = "",
        [CallerMemberName] string method = "")
    {
        string message = module + "-" + method + "-Response: " + JsonConvert.SerializeObject(parameter);
        logger.Log(LogLevels.Info, message, null, parameter, "Response:", refCode);
    }

    public static void LogWarn(this ILogger logger, string message)
    {
        logger.Log(LogLevels.Warning, message, null, null);
    }

    public static void LogWarn(this ILogger logger, string message, object parameter)
    {
        logger.Log(LogLevels.Warning, message, null, parameter);
    }

    public static void LogError(this ILogger logger, string message)
    {
        logger.Log(LogLevels.Error, message, null, null);
    }

    public static void LogError(this ILogger logger, string message, object parameter)
    {
        logger.Log(LogLevels.Error, message, null, parameter);
    }

    public static void LogError(this ILogger logger, string controller, Exception exception, object parameter,
        string refCode = "", [CallerMemberName] string method = "")
    {
        string message = controller + "-" + method + "-Error: " + JsonConvert.SerializeObject(parameter);
        logger.Log(LogLevels.Error, message, exception, parameter, "Error:", refCode);
    }

    public static void LogError(this ILogger logger, string message, Exception exception)
    {
        logger.Log(LogLevels.Error, message, exception, null);
    }
}
