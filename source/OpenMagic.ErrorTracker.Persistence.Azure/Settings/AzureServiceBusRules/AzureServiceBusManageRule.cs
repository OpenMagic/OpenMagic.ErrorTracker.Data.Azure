using OpenMagic.ErrorTracker.Persistence.Azure.Settings.AzureServiceBusRules.Infrastructure;

namespace OpenMagic.ErrorTracker.Persistence.Azure.Settings.AzureServiceBusRules
{
    public class AzureServiceBusManageRule : AzureServiceBusRule
    {
        public AzureServiceBusManageRule() : base("Manage")
        {
        }
    }
}