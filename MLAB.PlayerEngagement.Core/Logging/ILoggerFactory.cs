namespace MLAB.PlayerEngagement.Core.Logging;

public interface ILoggerFactory
{
    ILogger CreateLogger(string name);
}
