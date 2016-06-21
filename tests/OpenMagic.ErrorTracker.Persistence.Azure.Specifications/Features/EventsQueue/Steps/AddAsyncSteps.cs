using System;
using Mindscape.Raygun4Net;
using OpenMagic.ErrorTracker.Core.Events;
using OpenMagic.ErrorTracker.Core.Queues;
using OpenMagic.ErrorTracker.Persistence.Azure.Specifications.Helpers;
using TechTalk.SpecFlow;

namespace OpenMagic.ErrorTracker.Persistence.Azure.Specifications.Features.EventsQueue.Steps
{
    [Binding]
    public class AddAsyncSteps
    {
        private readonly Given _given;
        private readonly Actual _actual;
        private readonly DummyFactory _dummy;
        private readonly IEventsQueue _eventsQueue;

        public AddAsyncSteps(Given given, Actual actual, DummyFactory dummy, IEventsQueue eventsQueue)
        {
            _given = given;
            _actual = actual;
            _dummy = dummy;
            _eventsQueue = eventsQueue;
        }

        [Given(@"an IEvent")]
        public void GivenAnIEvent()
        {
            _given.Event = _dummy.Value<RaygunMessageReceived>();
        }

        [When(@"EventsQueue\.AddAsync\(IEvent\) is called")]
        public void WhenEventsQueue_AddAsyncIEventIsCalled()
        {
                _eventsQueue.AddAsync(_given.Event).Wait();
        }

        [Then(@"the event is added to the queue")]
        public void ThenTheEventIsAddedToTheQueue()
        {
            throw new NotImplementedException("todo");
        }
    }
}