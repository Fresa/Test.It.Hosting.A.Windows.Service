namespace Test.It.While.Hosting.Your.Windows.Service
{
    public interface IWindowsServiceClient
    {
        /// <summary>
        /// Sends a disconnect command to the Windows Service.
        /// </summary>
        void Disconnect();
    }
}