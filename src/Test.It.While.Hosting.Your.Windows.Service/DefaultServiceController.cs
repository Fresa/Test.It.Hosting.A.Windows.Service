using Test.It.While.Hosting.Your.Windows.Service.Delegates;

namespace Test.It.While.Hosting.Your.Windows.Service
{
    internal class DefaultServiceController : IServiceController
    {
        private readonly DefaultWindowsServiceController _controller;

        public DefaultServiceController(DefaultWindowsServiceController controller)
        {
            _controller = controller;
            _controller.OnStopped += code => OnStopped?.Invoke(code);
            _controller.OnStop += () => OnStop?.Invoke();
        }

        public void Stop()
        {
            _controller.Stop();
        }

        public event StoppedHandler OnStopped;
        public event StopHandler OnStop;
    }
}