namespace OpenMagic.ErrorTracker.Persistence.Azure.Settings
{
    public class ErrorTrackerAppSettings : AppSettings
    {
        public ErrorTrackerAppSettings()
            : base("App")
        {
        }

        public string Environment => GetString("Environment");
    }
}
