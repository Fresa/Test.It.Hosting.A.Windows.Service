using Test.It.Specifications;
using Test.It.Starters;

namespace Test.It.Hosting.A.WindowsService
{
    /// <summary>
    /// Builds the Windows Service host process.
    /// </summary>
    public interface IWindowsServiceBuilder
    {
        /// <summary>
        /// Creates the Windows Service hosting process with a test configuration.
        /// </summary>
        /// <param name="configurer">A test configuration used to override the Windows Service configuration.</param>
        /// <returns>An application starter</returns>
        IApplicationStarter<IWindowsServiceController> CreateWith(ITestConfigurer configurer);
    }
}