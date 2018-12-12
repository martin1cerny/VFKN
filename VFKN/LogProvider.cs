using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;

namespace VFKN
{
    public static class LogProvider
    {
        public static ILoggerFactory Factory { get; set; }
        public static ILogger GetLogger(string category)
        {
            return Factory?.CreateLogger(category) ?? NullLogger.Instance;
        }
        public static ILogger GetLogger(Type type)
        {
            return Factory?.CreateLogger(type) ?? NullLogger.Instance;
        }
        public static ILogger GetLogger<T>()
        {
            return Factory?.CreateLogger<T>() as ILogger ?? NullLogger.Instance;
        }
    }
}
