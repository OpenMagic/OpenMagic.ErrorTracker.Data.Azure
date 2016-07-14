using BoDi;
using LazyCache;
using LazyCache.Mocks;
using TechTalk.SpecFlow;

namespace OpenMagic.ErrorTracker.Persistence.Azure.Specifications
{
    [Binding]
    public class IoC
    {
        private readonly IObjectContainer _objectContainer;

        public IoC(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        [BeforeScenario]
        public void InitializeWebDriver()
        {
            _objectContainer.RegisterTypeAs<MockCachingService, IAppCache>();
        }
    }
}