using OpenMagic.ErrorTracker.Persistence.Azure.Settings.AzureServiceBusRules.Infrastructure;

namespace OpenMagic.ErrorTracker.Persistence.Azure.Infrastructure
{
    public interface IQueueCreatorSettings
    {
        string ServiceBusEndpoint { get; }
        string Name { get; }
        IAzureServiceBusRule ManageRule { get; }
    }
}