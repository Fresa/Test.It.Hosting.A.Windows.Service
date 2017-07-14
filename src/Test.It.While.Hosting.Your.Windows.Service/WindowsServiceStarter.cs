using System;
using Test.It.AppBuilders;
using Test.It.Starters;

namespace Test.It.While.Hosting.Your.Windows.Service
{
    public abstract class BaseWindowsServiceStarter<TClient> : IApplicationStarter<TClient>
        where TClient : IWindowsServiceController
    {
        protected abstract TClient Client { get; }
        protected abstract Action Starter { get; }
        
        public virtual void Start(IAppBuilder<TClient> applicationBuilder)
        {            
            applicationBuilder.WithController(Client).Use(new TaskStartingMiddleware(Starter));
        }
    }
}