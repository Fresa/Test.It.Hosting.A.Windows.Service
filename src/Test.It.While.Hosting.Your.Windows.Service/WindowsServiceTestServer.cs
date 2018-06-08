using System;
using Test.It.Starters;

namespace Test.It.While.Hosting.Your.Windows.Service
{
    internal class WindowsServiceTestServer : IDisposable
    {
        private WindowsServiceTestServer(IApplicationStarter<IWindowsServiceController> applicationStarter)
        {
            var appBuilder = new DefaultApplicationBuilder();
            var builder = new ControllerProvidingAppBuilder<IWindowsServiceController>(appBuilder);
            var environment = applicationStarter.Start(builder);
            Controller = builder.Controller;
            
            appBuilder.Build()(environment);
        }
        
        public IWindowsServiceController Controller { get; }

        public static WindowsServiceTestServer Start(IApplicationStarter<IWindowsServiceController> applicationStarter)
        {
            return new WindowsServiceTestServer(applicationStarter);
        }

        public void Dispose()
        {
        }
    }
}