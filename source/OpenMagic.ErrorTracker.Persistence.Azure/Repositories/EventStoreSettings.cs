using OpenMagic.ErrorTracker.Persistence.Azure.Settings;

namespace OpenMagic.ErrorTracker.Persistence.Azure.Repositories
{
    public class EventStoreSettings
    {
        private readonly AzureStorageSettings _azureStorageSettings;

        public EventStoreSettings(AzureStorageSettings azureStorageSettings)
        {
            _azureStorageSettings = azureStorageSettings;
        }

        public string TableName => $"{_azureStorageSettings.TableNamePrefix}Events";
    }
}