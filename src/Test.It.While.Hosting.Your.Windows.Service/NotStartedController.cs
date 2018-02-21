using System;
using Test.It.While.Hosting.Your.Windows.Service.Delegates;

namespace Test.It.While.Hosting.Your.Windows.Service
{
    internal class NotStartedController : IServiceController
    {
        public void Stop()
        {
            throw new InvalidOperationException("The server has not yet started and cannot be stopped.");
        }

        public void InvokeOnStopped(int exitCode)
        {
            OnStopped?.Invoke(exitCode);
        }
        
        public event StoppedHandler OnStopped;
    }
}