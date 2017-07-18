using Test.It.Specifications;

namespace Test.It.While.Hosting.Your.Windows.Service
{
    public class DefaultWindowsServiceHostStarter<TApplicationBuilder> : IWindowsServiceHostStarter 
        where TApplicationBuilder : IWindowsServiceBuilder, new()
    {
        private WindowsServiceTestServer _server;

        public IWindowsServiceController Start(ITestConfigurer testConfigurer)
        {
            var applicationBuilder = new TApplicationBuilder();
            _server = WindowsServiceTestServer.Create(applicationBuilder.CreateWith(testConfigurer).Start);
            return _server.Controller;
        }

        public void Dispose()
        {
            _server?.Dispose();
        }
    }
}