using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Test.It.While.Hosting.Your.Windows.Service
{
    internal class WindowsServiceHostingMiddleware
    {
        private readonly IWindowsService _service;
        private readonly IWindowsServiceController _controller;

        public WindowsServiceHostingMiddleware(IWindowsService service, IWindowsServiceController controller)
        {
            _service = service;
            _controller = controller;
        }

        public void Initialize(Func<IDictionary<string, object>, Task> next)
        {

        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            _controller.OnStop += Stop;

            await Task.Run(() =>
            {
                var startParms = environment[Owin.StartParameters] as string[] ?? new string[0];

                var startCode = _service.Start(startParms);
                _controller.Started(startCode);
            }).ContinueWith(task =>
            {
                _controller.RaiseException(task.Exception);
            }, TaskContinuationOptions.OnlyOnFaulted);
        }

        private void Stop()
        {
            var exitCode = _service.Stop();
            _controller.Stopped(exitCode);
        }
    }
}