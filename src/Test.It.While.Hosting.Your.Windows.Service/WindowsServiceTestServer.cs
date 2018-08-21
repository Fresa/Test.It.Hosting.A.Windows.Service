using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.It.Starters;

namespace Test.It.While.Hosting.Your.Windows.Service
{
    internal class WindowsServiceTestServer : IDisposable
    {
        private readonly DefaultApplicationBuilder _appBuilder;
        private readonly IDictionary<string, object> _environment;

        private WindowsServiceTestServer(IApplicationStarter<IWindowsServiceController> applicationStarter)
        {
            _appBuilder = new DefaultApplicationBuilder();
            var builder = new ControllerProvidingAppBuilder<IWindowsServiceController>(_appBuilder);
            _environment = applicationStarter.Start(builder);
            Controller = builder.Controller;
        }

        public IWindowsServiceController Controller { get; }

        public static WindowsServiceTestServer Create(IApplicationStarter<IWindowsServiceController> applicationStarter)
        {
            return new WindowsServiceTestServer(applicationStarter);
        }

        public async Task StartAsync()
        {
            await _appBuilder.Build()(_environment);
        }

        public void Dispose()
        {
        }
    }
}