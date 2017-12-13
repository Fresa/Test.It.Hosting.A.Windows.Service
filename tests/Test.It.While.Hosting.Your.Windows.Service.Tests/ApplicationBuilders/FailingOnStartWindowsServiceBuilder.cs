using System;
using Test.It.Specifications;

namespace Test.It.While.Hosting.Your.Windows.Service.Tests.ApplicationBuilders
{
    public class FailingOnStartWindowsServiceBuilder : DefaultWindowsServiceBuilder
    {
        public override IWindowsService Create(ITestConfigurer configurer)
        {
            return new FailingTestWindowsService();
        }

        private class FailingTestWindowsService : IWindowsService
        {
            public int Start(params string[] args)
            {
                throw new Exception("Failing to start");
            }

            public int Stop()
            {
                return 0;
            }

            public event Action<Exception> OnUnhandledException;
        }
    }
}