using System;

namespace Test.It.Hosting.A.Windows.Service
{
    /// <summary>
    /// Defines the communication channels between the hosted Windows Service and the Test Framework.
    /// </summary>
    public interface IWindowsServiceController
    {
        /// <summary>
        /// Triggered when the Windows Service disconnects
        /// </summary>
        event EventHandler Disconnected;

        /// <summary>
        /// Triggered when an exception is raised.
        /// </summary>
        event EventHandler<Exception> OnException;

        /// <summary>
        /// Raises an exception. 
        /// </summary>
        /// <param name="exception"></param>
        void RaiseException(Exception exception);

        /// <summary>
        /// Channel to communicate with the Windows Service. Usually exposed to the test instance.
        /// </summary>
        IWindowsServiceClient Client { get; }
    }
}