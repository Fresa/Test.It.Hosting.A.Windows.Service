using System.Collections.Generic;

namespace Test.It.While.Hosting.Your.Windows.Service
{
    internal class DefaultWindowsServiceStarter<TClient> : BaseWindowsServiceStarter<TClient>
        where TClient : IWindowsServiceController
    {
        public DefaultWindowsServiceStarter(IWindowsService windowsWindowsService, IWindowsServiceConfiguration windowsServiceConfiguration, TClient client)
        {
            Client = client;
            WindowsService = windowsWindowsService;
            Environment = new Dictionary<string, object> { { Owin.StartParameters, windowsServiceConfiguration.StartParameters } };
        }

        protected override IDictionary<string, object> Environment { get; }

        protected override TClient Client { get; }

        protected override IWindowsService WindowsService { get; }
    }
}