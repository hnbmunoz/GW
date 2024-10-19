using MLAB.PlayerEngagement.Core.Enum;

namespace MLAB.PlayerEngagement.Core.Logging;

public interface ILogger
{
    void AddProperty(object key, object value);
    void Log(LogLevels level, string message, Exception exception, object parameter);
    void Log(LogLevels level, string message, Exception exception, object parameter, string logType, string refCode);
}
