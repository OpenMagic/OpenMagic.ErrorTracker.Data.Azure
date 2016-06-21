using OpenMagic.ErrorTracker.Core.Events;
using OpenMagic.ErrorTracker.Core.Queues;

namespace OpenMagic.ErrorTracker.Persistence.Azure.Specifications.Helpers
{
    public class Given
    {
        public RaygunMessageReceived Event { get; set; }
        public IEventsQueue EventsQueue { get; set; }
    }
}