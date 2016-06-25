using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace OpenMagic.ErrorTracker.Persistence.Azure.Infrastructure
{
    public class RunOnceQueueCreator : IRunOnceQueueCreator
    {
        private static readonly ConcurrentDictionary<string, bool> CheckedQueues = new ConcurrentDictionary<string, bool>();
        private readonly IQueueCreator _queueCreator;

        public RunOnceQueueCreator(IQueueCreator queueCreator)
        {
            _queueCreator = queueCreator;
        }

        public Task CreateIfNotExistsAsync(IQueueCreatorSettings settings)
        {
            return Task.Run(() => CheckedQueues.GetOrAdd(settings.Name, q => _queueCreator.CreateIfNotExistsAsync(settings).Result));
        }
    }
}