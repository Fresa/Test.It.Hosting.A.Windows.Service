using System;
using Test.It.While.Hosting.Your.Windows.Service.Delegates;

namespace Test.It.While.Hosting.Your.Windows.Service
{
    /// <summary>
    /// Defines the communication channels between the hosted Windows Service and the Test Framework.
    /// </summary>
    public interface IWindowsServiceController
    {
        /// <summary>
        /// Triggered when the Windows Service is requested to stop
        /// </summary>
        event StopHandler OnStop;

        /// <summary>
        /// Stop the Windows Service
        /// </summary>
        void Stop();
        
        /// <summary>
        /// Triggered when the Windows Service has stopped
        /// </summary>
        event StoppedHandler OnStopped;

        /// <summary>
        /// Signal Windows Service started
        /// </summary>
        /// <param name="exitCode"></param>
        void Started(int exitCode);

        /// <summary>
        /// Triggered when the Windows Service has started
        /// </summary>
        event StartedHandler OnStarted;
        
        /// <summary>
        /// Signal Windows Service stopped
        /// </summary>
        /// <param name="exitCode"></param>
        void Stopped(int exitCode);

        /// <summary>
        /// Triggered when an exception is raised.
        /// </summary>
        event ExceptionHandler OnException;

        /// <summary>
        /// Raises an exception. 
        /// </summary>
        /// <param name="exception"></param>
        void RaiseException(Exception exception);

        /// <summary>
        /// Service controller to communicate with the Windows Service. Usually exposed to the test instance.
        /// </summary>
        IServiceController ServiceController { get; }
    }
}