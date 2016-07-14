namespace OpenMagic.ErrorTracker.Persistence.Azure.Settings
{
    public class AzureStorageSettings : AppSettings
    {
        public AzureStorageSettings()
            : base("Azure_Storage")
        {
        }

        public string ConnectionString => GetString("ConnectionString");
    }
}