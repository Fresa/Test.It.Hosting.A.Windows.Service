using System;
using System.Threading.Tasks;
using Test.It.Specifications;

namespace Test.It.While.Hosting.Your.Windows.Service
{
    public interface IWindowsServiceHostStarter : IDisposable
    {
        /// <summary>
        /// Creates the hosting process.
        /// </summary>
        /// <param name="testConfigurer">Test Configurer for the application</param>
        /// <param name="startParameters">Start parameters for the application</param>
        /// <returns>Windows Service Controller</returns>
        IWindowsServiceController Create(ITestConfigurer testConfigurer, params string[] startParameters);

        /// <summary>
        /// Starts the hosting process.
        /// </summary>
        Task StartAsync();
    }
}