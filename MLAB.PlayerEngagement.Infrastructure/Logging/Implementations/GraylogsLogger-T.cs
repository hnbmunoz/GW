using MLAB.PlayerEngagement.Core.Enum;
using MLAB.PlayerEngagement.Core.Logging;

namespace MLAB.PlayerEngagement.Infrastructure.Logging.Implementations;

public class GraylogsLogger<T> : ILogger<T>
{
    private ILogger _logger;

    public GraylogsLogger(ILoggerFactory factory)
    {
        _logger = factory.CreateLogger(typeof(T).ToString());
    }
    public void AddProperty(object key, object value)
    {
        _logger.AddProperty(key, value);
    }

    public void Log(LogLevels level, string message, Exception exception, object parameter)
    {
        _logger.Log(level, message, exception, parameter);
    }

    public void Log(LogLevels level, string message, Exception exception, object parameter, string logType, string refCode)
    {
        _logger.Log(level, message, exception, parameter, logType, refCode);
    }


}
