using Test.It.While.Hosting.Your.Windows.Service.Delegates;

namespace Test.It.While.Hosting.Your.Windows.Service
{
    public interface IWindowsServiceClient
    {
        /// <summary>
        /// Sends a stop command to the Windows Service.
        /// </summary>
        void Stop();

        /// <summary>
        /// Triggered when the Windows Service has stopped.
        /// </summary>
        event StoppedHandler OnStopped;
    }
}