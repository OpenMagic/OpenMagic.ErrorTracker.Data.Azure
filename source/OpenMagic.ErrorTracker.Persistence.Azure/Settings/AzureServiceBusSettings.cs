namespace OpenMagic.ErrorTracker.Persistence.Azure.Settings
{
    public class AzureServiceBusSettings : AppSettings
    {
        public AzureServiceBusSettings() : base("AzureServiceBus")
        {
        }

        public string Endpoint => $"sb://{Name}.servicebus.windows.net/";
        public string Name => GetString("Name");
    }
}