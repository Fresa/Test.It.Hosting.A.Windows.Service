using Test.It.Specifications;
using Test.It.Starters;

namespace Test.It.While.Hosting.Your.Windows.Service
{
    public abstract class DefaultWindowsServiceBuilder : IWindowsServiceBuilder
    {
        private static readonly DefaultWindowsServiceController WindowsServiceController = new DefaultWindowsServiceController();
        
        public abstract IWindowsService Create(ITestConfigurer configurer);
        
        public IApplicationStarter<IWindowsServiceController> CreateWith(ITestConfigurer configurer)
        {            
            var application = Create(configurer);

            void InternalStarter()
            {
                application.Start();
            }

            return new DefaultWindowsServiceStarter<IWindowsServiceController>(InternalStarter, WindowsServiceController);
        }
    }
}