using Test.It.Specifications;
using Test.It.Starters;

namespace Test.It.While.Hosting.Your.Windows.Service
{
    public abstract class DefaultWindowsServiceBuilder : IWindowsServiceBuilder
    {
        private IWindowsServiceConfiguration _configuration = new DefaultWindowsServiceConfiguration();
        public abstract IWindowsService Create(ITestConfigurer configurer);

        public IWindowsServiceBuilder WithConfiguration(IWindowsServiceConfiguration configuration)
        {
            _configuration = configuration;
            return this;
        }

        public IApplicationStarter<IWindowsServiceController> CreateWith(ITestConfigurer configurer)
        {            
            var application = Create(configurer);

            return new DefaultWindowsServiceStarter<IWindowsServiceController>(application, _configuration, new DefaultWindowsServiceController());
        }
    }
}