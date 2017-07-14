namespace Test.It.While.Hosting.Your.Windows.Service
{
    internal class DefaultWindowsServiceClient : IWindowsServiceClient
    {
        private readonly DefaultWindowsServiceController _controller;

        public DefaultWindowsServiceClient(DefaultWindowsServiceController controller)
        {
            _controller = controller;
        }

        public void Disconnect()
        {
            _controller.Disconnect();
        }
    }
}