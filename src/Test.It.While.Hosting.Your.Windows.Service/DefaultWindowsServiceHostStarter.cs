using System.Threading.Tasks;
using Test.It.Specifications;

namespace Test.It.While.Hosting.Your.Windows.Service
{
    public class DefaultWindowsServiceHostStarter<TApplicationBuilder> : IWindowsServiceHostStarter 
        where TApplicationBuilder : IWindowsServiceBuilder, new()
    {
        private WindowsServiceTestServer _server;
        private ITestConfigurer _testConfigurer;
        private TApplicationBuilder _applicationBuilder;

        public IWindowsServiceController Create(ITestConfigurer testConfigurer, params string[] startParameters)
        {
            _applicationBuilder = new TApplicationBuilder();

            _testConfigurer = testConfigurer;
            _server = WindowsServiceTestServer
                .Create(_applicationBuilder
                    .WithConfiguration(new DefaultWindowsServiceConfiguration(startParameters))
                    .CreateWith(_testConfigurer));
            return _server.Controller;
        }

        public void Dispose()
        {
            _server?.Dispose();
        }

        public async Task StartAsync()
        {
            await _server.StartAsync();
        }
    }
}