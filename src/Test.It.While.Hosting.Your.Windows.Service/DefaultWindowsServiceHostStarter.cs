using Test.It.Specifications;

namespace Test.It.While.Hosting.Your.Windows.Service
{
    public class DefaultWindowsServiceHostStarter<TApplicationBuilder> : IWindowsServiceHostStarter 
        where TApplicationBuilder : IWindowsServiceBuilder, new()
    {
        private WindowsServiceTestServer _server;

        public IWindowsServiceController Start(ITestConfigurer testConfigurer, params string[] startParameters)
        {
            var applicationBuilder = new TApplicationBuilder();
            _server = WindowsServiceTestServer
                .Start(applicationBuilder
                    .WithConfiguration(new DefaultWindowsServiceConfiguration(startParameters))
                    .CreateWith(testConfigurer));

            return _server.Controller;
        }

        public void Dispose()
        {
            _server?.Dispose();
        }
    }
}