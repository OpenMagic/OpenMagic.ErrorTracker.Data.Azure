using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace OpenMagic.ErrorTracker.Persistence.Azure.Queues
{
    public class RunOnceQueueCreator : IRunOnceQueueCreator
    {
        private static readonly ConcurrentDictionary<string, bool> CheckedQueues = new ConcurrentDictionary<string, bool>();

        public Task CreateIfNotExistsAsync(IRunOnceQueueCreatorSettings settings)
        {
            return Task.Run(() => CheckedQueues.GetOrAdd(settings.QueueName, CreateIfNotExistsImpl(settings)));
        }

        private static bool CreateIfNotExistsImpl(IRunOnceQueueCreatorSettings settings)
        {
            return CreateIfNotExistsAsyncImpl(settings).Result;
        }

        private static async Task<bool> CreateIfNotExistsAsyncImpl(IRunOnceQueueCreatorSettings settings)
        {
            // ReSharper disable ConditionIsAlwaysTrueOrFalse
            var tokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider(settings.ManageRule.RootName, settings.ManageRule.Key);
            var namespaceManager = new NamespaceManager(settings.ServiceBusUrl, tokenProvider);
            var queueExists = await namespaceManager.QueueExistsAsync(settings.QueueName);

            if (queueExists)
            {
                return !queueExists;
            }

            var queueDescription = QueueDescription(settings.QueueName);
            await namespaceManager.CreateQueueAsync(queueDescription);

            return queueExists;
            // ReSharper restore ConditionIsAlwaysTrueOrFalse
        }

        private static QueueDescription QueueDescription(string queueName)
        {
            var description = new QueueDescription(queueName)
            {
                // 21 Jun 2016 - https://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k(Microsoft.ServiceBus.Messaging.QueueDescription.MaxSizeInMegabytes);k(TargetFrameworkMoniker-.NETFramework,Version%3Dv4.6.1);k(DevLang-csharp)&rd=true
                // The size must be specified in increments of 1024 MB, up to the maximum defined by the quota in Service Bus quotas.The maximum is currently 5120 MB(5 GB).
                MaxSizeInMegabytes = 1024 * 1
            };
            return description;
        }

    }
}