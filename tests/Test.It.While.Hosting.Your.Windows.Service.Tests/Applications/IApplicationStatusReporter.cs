namespace Test.It.While.Hosting.Your.Windows.Service.Tests.Applications
{
    public interface IApplicationStatusReporter
    {
        bool HaveStarted { get; set; }
    }
}