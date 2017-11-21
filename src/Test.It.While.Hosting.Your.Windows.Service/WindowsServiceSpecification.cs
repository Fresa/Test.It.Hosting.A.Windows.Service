using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;

namespace Test.It.While.Hosting.Your.Windows.Service
{
    public abstract class WindowsServiceSpecification<THostStarter> : IUseConfiguration<THostStarter>
        where THostStarter : class, IWindowsServiceHostStarter, new()
    {
        private readonly AutoResetEvent _wait = new AutoResetEvent(false);
        private readonly ConcurrentBag<Exception> _exceptions = new ConcurrentBag<Exception>();

        private void RegisterException(Exception exception)
        {
            if (!(exception is AggregateException aggregateException))
            {
                _exceptions.Add(exception);
                return;
            }

            foreach (var innerException in aggregateException.InnerExceptions)
            {
                _exceptions.Add(innerException);
            }
        }

        /// <summary>
        /// Execution timeout. Defaults to 3 seconds.
        /// </summary>
        protected virtual TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(3);

        /// <summary>
        /// Bootstraps and starts the hosted application.
        /// </summary>
        /// <param name="windowsServiceConfiguration">Windows service configuration</param>
        public void SetConfiguration(THostStarter windowsServiceConfiguration)
        {
            var controller = windowsServiceConfiguration.Start(new SimpleTestConfigurer(Given), StartParameters);
            Client = controller.Client;

            controller.OnStopped += exitCode =>
            {
                _wait.Set();
            };

            controller.OnException += RegisterException;

            When();

            Wait();
        }

        private void Wait()
        {
            if (_wait.WaitOne(Timeout) == false)
            {
                _exceptions.Add(new TimeoutException($"Waited {Timeout.Seconds} seconds."));
            }

            HandleExceptions();
        }

        private void HandleExceptions()
        {
            if (_exceptions.Any() == false)
            {
                return;
            }

            if (_exceptions.Count == 1)
            {
                throw _exceptions.First();
            }

            throw new AggregateException(_exceptions);
        }

        /// <summary>
        /// Defines the start parameters that will be used to bootstrap the application.
        /// </summary>
        /// <returns></returns>
        protected virtual string[] StartParameters { get; } = new string[0];
        
        /// <summary>
        /// Client to communicate with the hosted windows service application.
        /// </summary>
        protected IWindowsServiceClient Client { get; private set; }

        /// <summary>
        /// OBS! <see cref="Client"/> is not ready here since the application is in bootstrapping phase where you control the service configuration.
        /// </summary>
        /// <param name="configurer">Service container</param>
        protected virtual void Given(IServiceContainer configurer) { }

        /// <summary>
        /// Application has started and is reachable through <see cref="Client"/>.
        /// </summary>
        protected virtual void When() { }
    }
}