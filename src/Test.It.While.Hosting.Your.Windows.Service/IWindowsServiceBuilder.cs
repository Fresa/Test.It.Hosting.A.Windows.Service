using Test.It.Specifications;
using Test.It.Starters;

namespace Test.It.While.Hosting.Your.Windows.Service
{
    /// <summary>
    /// Builds the Windows Service host process.
    /// </summary>
    public interface IWindowsServiceBuilder
    {
        /// <summary>
        /// Use an optional windows service configuration
        /// </summary>
        /// <param name="configuration">Windows service configuration</param>
        /// <returns></returns>
        IWindowsServiceBuilder WithConfiguration(IWindowsServiceConfiguration configuration);
        
        /// <summary>
        /// Creates the Windows Service hosting process with a test configuration.
        /// </summary>
        /// <param name="configurer">A test configuration used to override the Windows Service configuration.</param>
        /// <returns>An application starter</returns>
        IApplicationStarter<IWindowsServiceController> CreateWith(ITestConfigurer configurer);
    }
}