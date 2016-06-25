using OpenMagic.ErrorTracker.Persistence.Azure.Settings;

namespace OpenMagic.ErrorTracker.Persistence.Azure.Infrastructure
{
    public class TableManagerSettings
    {
        private readonly AzureStorageSettings _settings;

        public TableManagerSettings(AzureStorageSettings settings)
        {
            _settings = settings;
        }

        public string ConnectionString => _settings.ConnectionString;
    }
}