using OpenMagic.ErrorTracker.Persistence.Azure.Settings.AzureServiceBusRules.Infrastructure;

namespace OpenMagic.ErrorTracker.Persistence.Azure.Settings.AzureServiceBusRules
{
    public class AzureServiceBusListenRule : AzureServiceBusRule
    {
        public AzureServiceBusListenRule() : base("Listen")
        {
        }
    }
}
