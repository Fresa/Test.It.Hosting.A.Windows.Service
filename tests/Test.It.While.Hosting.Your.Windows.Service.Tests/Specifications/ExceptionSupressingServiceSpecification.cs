using System;

namespace Test.It.While.Hosting.Your.Windows.Service.Tests.Specifications
{
    public abstract class ExceptionSupressingServiceSpecification<TConfiguration> : WindowsServiceSpecification<TConfiguration>, IDisposable
        where TConfiguration : class, IWindowsServiceHostStarter, new()
    {
        private readonly TConfiguration _configuration;
        
        protected ExceptionSupressingServiceSpecification()
        {
            _configuration = new TConfiguration();
            try
            {
                SetConfiguration(_configuration);
            }
            catch (Exception exception)
            {
                OnException(exception);
            }
        }

        protected abstract void OnException(Exception exception);

        public void Dispose()
        {
            _configuration.Dispose();
        }
    }
}