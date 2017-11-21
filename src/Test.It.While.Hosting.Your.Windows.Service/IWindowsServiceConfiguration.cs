namespace Test.It.While.Hosting.Your.Windows.Service
{
    public interface IWindowsServiceConfiguration
    {
        string[] StartParameters { get; }
    }
}