namespace OpenMagic.ErrorTracker.Persistence.Azure.Settings.AzureServiceBusRules.Infrastructure
{
    public abstract class AzureServiceBusRule : AppSettings, IAzureServiceBusRule
    {
        protected AzureServiceBusRule(string ruleName) : base($"AzureServiceBus_{ruleName}Rule")
        {
        }

        public string Name => GetString(nameof(Name));
        public string Key => GetString(nameof(Key));
    }
}