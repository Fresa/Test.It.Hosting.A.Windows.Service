using System;
using Test.It.Specifications;

namespace Test.It.While.Hosting.Your.Windows.Service.Tests.ApplicationBuilders
{
    public class FailingOnStopWindowsServiceBuilder : DefaultWindowsServiceBuilder
    {
        public override IWindowsService Create(ITestConfigurer configurer)
        {
            return new FailingTestWindowsService();
        }

        private class FailingTestWindowsService : IWindowsService
        {
            public int Start(params string[] args)
            {
                return 0;
            }

            public int Stop()
            {
                throw new Exception("Failing to stop");
            }

            public event Action<Exception> OnUnhandledException;
        }
    }
}