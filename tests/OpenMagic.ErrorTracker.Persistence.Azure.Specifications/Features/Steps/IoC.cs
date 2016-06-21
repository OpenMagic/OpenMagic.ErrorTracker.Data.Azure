using BoDi;
using OpenMagic.ErrorTracker.Core.Queues;
using OpenMagic.ErrorTracker.Core.Serialization;
using OpenMagic.ErrorTracker.Persistence.Azure.Queues;
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
            _objectContainer.RegisterTypeAs<Queues.EventsQueue, IEventsQueue>();
            _objectContainer.RegisterTypeAs<RunOnceQueueCreator, IRunOnceQueueCreator>();
            _objectContainer.RegisterTypeAs<Serializer, ISerializer>();
        }
    }
}