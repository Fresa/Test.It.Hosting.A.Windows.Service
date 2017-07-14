namespace Test.It.Hosting.A.WindowsService
{
    public interface IWindowsService
    {
        /// <summary>
        /// Starts the Windows Service
        /// </summary>
        /// <param name="args">Any argument that the Windows Service might use</param>
        /// <returns>Start process status defined by the Windows Service.</returns>
        int Start(params string[] args);

        /// <summary>
        /// Stops the Windows Service.
        /// </summary>
        void Stop();
    }
}