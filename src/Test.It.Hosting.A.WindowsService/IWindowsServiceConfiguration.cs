using System;
using Test.It.Specifications;

namespace Test.It.Hosting.A.WindowsService
{
    public interface IWindowsServiceConfiguration : IDisposable
    {
        /// <summary>
        /// Starts the hosting process.
        /// </summary>
        /// <param name="testConfigurer"></param>
        /// <returns></returns>
        IWindowsServiceController Start(ITestConfigurer testConfigurer);
    }
}