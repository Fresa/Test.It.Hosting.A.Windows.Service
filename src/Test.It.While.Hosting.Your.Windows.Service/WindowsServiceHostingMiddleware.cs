using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Test.It.While.Hosting.Your.Windows.Service
{
    internal class WindowsServiceHostingMiddleware : IMiddleware
    {
        private readonly IWindowsService _service;
        private readonly IWindowsServiceController _controller;
        private Func<IDictionary<string, object>, Task> _next;

        public WindowsServiceHostingMiddleware(IWindowsService service, IWindowsServiceController controller)
        {
            _service = service;
            _controller = controller;
        }

        public void Initialize(Func<IDictionary<string, object>, Task> next)
        {
            _next = next;
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            _controller.OnStop += async () =>
            {
                try
                {
                    await Task.Run(() =>
                    {
                        var exitCode = _service.Stop();

                        _service.OnUnhandledException -= OnUnhandledException;
                        _controller.Stopped(exitCode);
                    });
                }
                catch (Exception exception)
                {
                    OnUnhandledException(exception);
                }
            };

            try
            {
                await Task.Run(() =>
                {
                    var startParameters = environment[Owin.StartParameters] as string[] ?? new string[0];

                    _service.OnUnhandledException += OnUnhandledException;

                    var startCode = _service.Start(startParameters);
                    _controller.Started(startCode);
                });
            }
            catch (Exception exception)
            {
                OnUnhandledException(exception);
            }

            if (_next != null)
            {
                await _next.Invoke(environment);
            }
        }

        private void OnUnhandledException(Exception exception)
        {
            _controller.RaiseException(exception);
        }
    }
}