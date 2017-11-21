namespace Test.It.While.Hosting.Your.Windows.Service
{
    internal class DefaultWindowsServiceConfiguration : IWindowsServiceConfiguration
    {
        public DefaultWindowsServiceConfiguration(params string[] startParameters)
        {
            StartParameters = startParameters;
        }

        public string[] StartParameters { get; }
    }
}