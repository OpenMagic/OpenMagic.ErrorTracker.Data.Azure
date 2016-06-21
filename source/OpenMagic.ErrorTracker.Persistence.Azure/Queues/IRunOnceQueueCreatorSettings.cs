using OpenMagic.ErrorTracker.Persistence.Azure.Settings.AzureServiceBusRules;

namespace OpenMagic.ErrorTracker.Persistence.Azure.Queues
{
    public interface IRunOnceQueueCreatorSettings
    {
        string ServiceBusUrl { get; }
        string QueueName { get; }
        IAzureServiceBusRule ManageRule { get; }
    }
}