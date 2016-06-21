namespace OpenMagic.ErrorTracker.Persistence.Azure.Settings.AzureServiceBusRules
{
    public abstract class AzureServiceBusRule : AppSettings, IAzureServiceBusRule
    {
        protected AzureServiceBusRule(string ruleName) : base($"AzureServiceBus_{ruleName}Rule_")
        {
        }

        public string RootName => $"Root{Name}";
        public string Name => GetString(nameof(Name));
        public string Key => GetString(nameof(Key));
    }
}