using System;
using System.Diagnostics;
using OpenMagic.ErrorTracker.Persistence.Azure.Logging;

namespace OpenMagic.ErrorTracker.Persistence.Azure.Infrastructure
{
    internal class LoggingProfiler
    {
        private static readonly ILog Logger = LogProvider.GetLogger(nameof(LoggingProfiler));
        private readonly Stopwatch _stopwatch;

        internal LoggingProfiler()
        {
            _stopwatch = Stopwatch.StartNew();
        }

        internal void Log(Func<Stopwatch, string> message)
        {
            Logger.Info(() => message(_stopwatch));
        }
    }
}