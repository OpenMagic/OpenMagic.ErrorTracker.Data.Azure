using System;
using OpenMagic.ErrorTracker.Core.Events;
using OpenMagic.ErrorTracker.Core.Repositories;
using OpenMagic.ErrorTracker.Persistence.Azure.Specifications.Helpers;
using TechTalk.SpecFlow;

namespace OpenMagic.ErrorTracker.Persistence.Azure.Specifications.Features.Repositories.EventStore.Steps
{
    [Binding]
    public class AddAsyncSteps
    {
        private readonly Actual _actual;
        private readonly DummyFactory _dummy;
        private readonly IEventStore _eventStore;
        private readonly Given _given;

        public AddAsyncSteps(Given given, Actual actual, DummyFactory dummy, IEventStore eventStore)
        {
            _given = given;
            _actual = actual;
            _dummy = dummy;
            _eventStore = eventStore;
        }

        [Given(@"aggregateType is '(.*)'")]
        public void GivenAggregateTypeIs(string aggregateType)
        {
            _given.AggregateType = Helpers.Reflection.GetCoreTypeByName(aggregateType);
        }

        [Given(@"aggregateId is '(.*)'")]
        public void GivenAggregateIdIs(string aggregateId)
        {
            _given.AggregateId = Guid.Parse(aggregateId);
        }

        [Given(@"a '(.*)' event")]
        public void GivenAEvent(string eventType)
        {
            var type = Helpers.Reflection.GetCoreTypeByName(eventType);

            _given.Event = (IEvent)_dummy.Value(type);
        }

        [When(@"EventStore\.SaveEventsAsync\(aggregateType, aggregateId, events\) is called")]
        public void WhenEventStore_SaveEventsAsyncAggregateTypeAggregateIdEventSIsCalled()
        {
            try
            {
                _eventStore.SaveEventsAsync(_given.AggregateType, _given.AggregateId, new[] { _given.Event }).Wait();
            }
            catch (AggregateException exception)
            {
                throw new InformativeAggregateException(exception);
            }
        }
    }
}