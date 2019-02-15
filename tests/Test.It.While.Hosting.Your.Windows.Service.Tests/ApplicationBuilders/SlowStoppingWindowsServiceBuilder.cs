using System;
using System.Threading;
using Test.It.Specifications;

namespace Test.It.While.Hosting.Your.Windows.Service.Tests.ApplicationBuilders
{
    public class SlowStoppingWindowsServiceBuilder : DefaultWindowsServiceBuilder
    {
        public override IWindowsService Create(ITestConfigurer configurer)
        {
            return new SlowStoppingTestWindowsService();
        }

        private class SlowStoppingTestWindowsService : IWindowsService
        {
            public int Start(params string[] args)
            {
                return 0;
            }

            public int Stop()
            {
                Thread.Sleep(10000);
                return 0;
            }

            public event Action<Exception> OnUnhandledException;
        }
    }
}