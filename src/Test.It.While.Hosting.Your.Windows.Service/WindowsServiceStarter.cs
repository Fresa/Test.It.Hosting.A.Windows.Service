using System.Collections.Generic;
using Test.It.ApplicationBuilders;
using Test.It.Starters;

namespace Test.It.While.Hosting.Your.Windows.Service
{
    public abstract class BaseWindowsServiceStarter<TClient> : IApplicationStarter<TClient>
        where TClient : IWindowsServiceController
    {
        protected abstract TClient Client { get; }

        protected abstract IWindowsService WindowsService { get; }

        protected abstract IDictionary<string, object> Environment { get; }

        public virtual IDictionary<string, object> Start(IApplicationBuilder<TClient> applicationBuilder)
        {
            applicationBuilder.WithController(Client).Use(new WindowsServiceHostingMiddleware(WindowsService, Client));
            return Environment;
        }
    }
}