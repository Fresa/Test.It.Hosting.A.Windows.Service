using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Test.It.While.Hosting.Your.Windows.Service.Tests.Specifications
{
    public abstract class XUnitWindowsServiceSpecification<TConfiguration> : WindowsServiceSpecification<TConfiguration>, IAsyncLifetime
        where TConfiguration : class, IWindowsServiceHostStarter, new()
    {
        private TConfiguration _configuration;

        public async Task InitializeAsync()
        {
            _configuration = new TConfiguration();
            await SetConfigurationAsync(_configuration);
        }

        public Task DisposeAsync()
        {
            _configuration.Dispose();
            return Task.CompletedTask;
        }
    }
}