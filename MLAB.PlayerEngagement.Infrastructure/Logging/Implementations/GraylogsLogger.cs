using MLAB.PlayerEngagement.Core.Enum;
using Newtonsoft.Json;
using NLog;
using ILogger = MLAB.PlayerEngagement.Core.Logging.ILogger;
using System.Reflection;

namespace MLAB.PlayerEngagement.Infrastructure.Logging.Implementations;

public class GraylogsLogger : ILogger
{
    private readonly NLog.Logger _logger;
    private readonly Dictionary<object, object> _properties = new Dictionary<object, object>();
    private readonly string _loggerName;

    public GraylogsLogger()
    {
         var logger = NLog.LogManager.GetCurrentClassLogger();
         _loggerName = Assembly.GetEntryAssembly().GetName().Name;
        _logger = logger;
    }

    public void AddProperty(object key, object value)
    {
        try
        {
            _properties.Add(key, value);
        }
        catch (ArgumentException)
        {
            _properties[key] = value;
        }
    }

    public void Log(LogLevels level, string message, Exception exception, object parameter)
    {
        var nlevel = GetNlogLevel(level);
        var eventInfo = new LogEventInfo(nlevel, _loggerName, message);

        if (parameter != null)
            eventInfo.Properties.Add("Parameter", JsonConvert.SerializeObject(parameter));

        if (exception != null)
            eventInfo.Exception = exception;

        Log(eventInfo);
    }

    public void Log(LogLevels level, string message, Exception exception, object parameter, string logType, string refCode)
    {
        var nlevel = GetNlogLevel(level);
        var eventInfo = new LogEventInfo(nlevel, _loggerName, message);

        if (parameter != null)
            eventInfo.Properties.Add("Parameter", logType + JsonConvert.SerializeObject(parameter));

        if (refCode != null)
            eventInfo.Properties.Add("ReferenceCode", refCode);


        if (exception != null)
            eventInfo.Exception = exception;

        Log(eventInfo);
    }

    private void Log(LogEventInfo loginfo)
    {
        if (_properties.Count > 0)
        {
            foreach (var property in _properties)
            {
                loginfo.Properties.Add(property.Key, property.Value);
            }
        }
        _logger.Log(loginfo);
    }

    private LogLevel GetNlogLevel(LogLevels level)
    {
        switch (level)
        {
            case LogLevels.Error:
                return LogLevel.Error;
            case LogLevels.Warning:
                return LogLevel.Warn;
            case LogLevels.Info:
                return LogLevel.Info;
            default:
                return LogLevel.Info;
        }
    }
}

