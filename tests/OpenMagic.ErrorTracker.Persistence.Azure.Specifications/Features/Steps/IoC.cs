using BoDi;
using OpenMagic.ErrorTracker.Core.Infrastructure.Serialization;
using OpenMagic.ErrorTracker.Core.Repositories;
using OpenMagic.ErrorTracker.Persistence.Azure.Infrastructure;
using OpenMagic.ErrorTracker.Persistence.Azure.Repositories;
using TechTalk.SpecFlow;

namespace OpenMagic.ErrorTracker.Persistence.Azure.Specifications.Features.Steps
{
    [Binding]
    public class WebDriverSupport
    {
        private readonly IObjectContainer _objectContainer;

        public WebDriverSupport(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        [BeforeScenario]
        public void InitializeContainer()
        {
            _objectContainer.RegisterTypeAs<EventStore, IEventStore>();
            _objectContainer.RegisterTypeAs<QueueCreator, IQueueCreator>();
            _objectContainer.RegisterTypeAs<RunOnceQueueCreator, IRunOnceQueueCreator>();
            _objectContainer.RegisterTypeAs<Serializer, ISerializer>();
        }
    }
}