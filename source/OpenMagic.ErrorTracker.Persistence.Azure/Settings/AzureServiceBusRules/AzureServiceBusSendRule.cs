using OpenMagic.ErrorTracker.Persistence.Azure.Settings.AzureServiceBusRules.Infrastructure;

namespace OpenMagic.ErrorTracker.Persistence.Azure.Settings.AzureServiceBusRules
{
    public class AzureServiceBusSendRule : AzureServiceBusRule
    {
        public AzureServiceBusSendRule() : base("Send")
        {
        }
    }
}
