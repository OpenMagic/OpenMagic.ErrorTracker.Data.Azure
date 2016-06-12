using System;
using TechTalk.SpecFlow;

namespace OpenMagic.ErrorTracker.Persistence.Azure.Specifications.Features.EventsQueue.Steps
{
    [Binding]
    public class AddAsyncSteps
    {
        [Given(@"an IEvent")]
        public void GivenAnIEvent()
        {
            throw new NotImplementedException("todo");
        }

        [When(@"EventsQueue\.AddAsync\(IEvent\) is called")]
        public void WhenEventsQueue_AddAsyncIEventIsCalled()
        {
            throw new NotImplementedException("todo");
        }

        [Then(@"the event is added to the queue")]
        public void ThenTheEventIsAddedToTheQueue()
        {
            throw new NotImplementedException("todo");
        }
    }
}