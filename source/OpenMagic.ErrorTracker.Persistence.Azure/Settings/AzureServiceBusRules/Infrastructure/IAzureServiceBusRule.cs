namespace OpenMagic.ErrorTracker.Persistence.Azure.Settings.AzureServiceBusRules.Infrastructure
{
    public interface IAzureServiceBusRule
    {
        string Name { get; }
        string Key { get; }
    }
}