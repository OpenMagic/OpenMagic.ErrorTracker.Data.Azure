namespace OpenMagic.ErrorTracker.Persistence.Azure.Settings
{
    public class EventsQueueSettings : AppSettings
    {
        public EventsQueueSettings() : base("EventsQueue")
        {
        }

        public string Name => GetString(nameof(Name));
    }
}