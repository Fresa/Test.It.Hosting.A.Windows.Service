using System;
using Test.It.Specifications;

namespace Test.It.While.Hosting.Your.Windows.Service.Tests.ApplicationBuilders
{
    public class EmptyWindowsServiceBuilder : DefaultWindowsServiceBuilder
    {
        public override IWindowsService Create(ITestConfigurer configurer)
        {
            return new EmptyWindowsService();
        }

        private class EmptyWindowsService : IWindowsService
        {
            public int Start(params string[] args)
            {
                return 0;
            }

            public int Stop()
            {
                return 0;
            }

            public event Action<Exception> OnUnhandledException;
        }
    }
}