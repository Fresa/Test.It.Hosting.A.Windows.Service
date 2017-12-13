using System;

namespace Test.It.While.Hosting.Your.Windows.Service.Tests.Specifications
{
    public abstract class XUnitWindowsServiceSpecification<TConfiguration> : WindowsServiceSpecification<TConfiguration>, IDisposable
        where TConfiguration : class, IWindowsServiceHostStarter, new()
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
        }
    }
}