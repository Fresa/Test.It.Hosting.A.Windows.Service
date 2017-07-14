using System;

namespace Test.It.Hosting.A.WindowsService.Tests
{
    public abstract class XUnitWindowsServiceSpecification<TConfiguration> : WindowsServiceSpecification<TConfiguration>, IDisposable
        where TConfiguration : class, IWindowsServiceConfiguration, new()
    {
        private readonly TConfiguration _configuration;

        protected XUnitWindowsServiceSpecification()
        {
            _configuration = new TConfiguration();
            SetConfiguration(_configuration);
        }
        
        public void Dispose()
        {
            _configuration.Dispose();
            Client.Disconnect();
        }
    }
}