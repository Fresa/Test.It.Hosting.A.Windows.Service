using System;
using FakeItEasy;
using Should.Fluent;
using Xunit;

namespace Test.It.While.Hosting.Your.Windows.Service.Tests
{
    namespace Given_a_windows_service
    {
        public class When_testing : XUnitWindowsServiceSpecification<
            DefaultWindowsServiceHostStarter<TestWindowsServiceBuilder>>
        {
            private bool _started;

            protected override TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(4);

            protected override string[] StartParameters { get; } = { "start" };

            protected override void Given(IServiceContainer configurer)
            {
                var app = A.Fake<ITestApp>();
                A.CallToSet(() => app.HaveStarted).To(true).Invokes(() =>
                {
                    _started = true;
                    ServiceController.Stop();
                });
                configurer.Register(() => app);
            }

            [Fact]
            public void It_should_have_started_the_app()
            {
                _started.Should().Be.True();
            }
        }
    }
}