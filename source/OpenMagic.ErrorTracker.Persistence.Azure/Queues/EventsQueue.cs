using System;
using System.Threading.Tasks;
using OpenMagic.ErrorTracker.Core.Events;
using OpenMagic.ErrorTracker.Core.Queues;

namespace OpenMagic.ErrorTracker.Persistence.Azure.Queues
{
    public class EventsQueue : IEventsQueue
    {
        public EventsQueue()
        {
            throw new NotImplementedException("todo");
        }

        public Task AddAsync(IEvent @event)
        {
            throw new NotImplementedException("todo");
        }
    }
}