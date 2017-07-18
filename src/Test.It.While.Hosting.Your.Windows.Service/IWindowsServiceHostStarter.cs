using System;
using Test.It.Specifications;

namespace Test.It.While.Hosting.Your.Windows.Service
{
    public interface IWindowsServiceHostStarter : IDisposable
    {
        /// <summary>
        /// Starts the hosting process.
        /// </summary>
        /// <param name="testConfigurer">Test Configurer for the application</param>
        /// <returns>Windows Service Controller</returns>
        IWindowsServiceController Start(ITestConfigurer testConfigurer);
    }
}