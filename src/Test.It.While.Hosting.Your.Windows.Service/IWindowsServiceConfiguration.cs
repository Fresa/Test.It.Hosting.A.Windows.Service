using System;
using Test.It.Specifications;

namespace Test.It.While.Hosting.Your.Windows.Service
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