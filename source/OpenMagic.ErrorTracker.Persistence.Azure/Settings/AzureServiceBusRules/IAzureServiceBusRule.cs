namespace OpenMagic.ErrorTracker.Persistence.Azure.Settings.AzureServiceBusRules
{
    public interface IAzureServiceBusRule
    {
        string RootName { get; }
        string Name { get; }
        string Key { get; }
    }
}