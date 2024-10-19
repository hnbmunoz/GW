using MLAB.PlayerEngagement.Core.Logging;
using ILogger = MLAB.PlayerEngagement.Core.Logging.ILogger;

namespace MLAB.PlayerEngagement.Infrastructure.Logging.Implementations;

public class GraylogsLoggerFactory : ILoggerFactory
{
    public ILogger CreateLogger(string name)
    {
        return new GraylogsLogger();
    }
}
