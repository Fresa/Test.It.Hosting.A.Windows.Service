namespace Test.It.Hosting.A.Windows.Service
{
    public interface IWindowsServiceClient
    {
        /// <summary>
        /// Sends a disconnect command to the Windows Service.
        /// </summary>
        void Disconnect();
    }
}