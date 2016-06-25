using System.Threading.Tasks;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace OpenMagic.ErrorTracker.Persistence.Azure.Infrastructure
{
    public class QueueCreator : IQueueCreator
    {
        public async Task<bool> CreateIfNotExistsAsync(IQueueCreatorSettings settings)
        {
            var namespaceManager = CreateNamespaceManager(settings);
            var queueExists = await namespaceManager.QueueExistsAsync(settings.Name);

            if (queueExists)
            {
                return /*createdQueue*/ false;
            }

            var queueDescription = QueueDescription(settings.Name);
            await namespaceManager.CreateQueueAsync(queueDescription);

            return /*createdQueue*/ true;
        }

        private static NamespaceManager CreateNamespaceManager(IQueueCreatorSettings settings)
        {
            var tokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider(settings.ManageRule.Name, settings.ManageRule.Key);
            var namespaceManager = new NamespaceManager(settings.ServiceBusEndpoint, tokenProvider);
            return namespaceManager;
        }

        private static QueueDescription QueueDescription(string queueName)
        {
            return new QueueDescription(queueName);
        }
    }
}