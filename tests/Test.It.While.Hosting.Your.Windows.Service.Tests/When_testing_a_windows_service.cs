﻿using System;
using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using Should.Fluent;
using Test.It.While.Hosting.Your.Windows.Service.Tests.ApplicationBuilders;
using Test.It.While.Hosting.Your.Windows.Service.Tests.Applications;
using Test.It.While.Hosting.Your.Windows.Service.Tests.Specifications;
using Xunit;

namespace Test.It.While.Hosting.Your.Windows.Service.Tests
{
    namespace Given_a_windows_service
    {
        public class When_testing : XUnitWindowsServiceSpecification<DefaultWindowsServiceHostStarter<TestWindowsServiceBuilder>>
        {
            private bool _started;
            private int _exitCode;

            protected override TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(4);

            protected override string[] StartParameters { get; } = { "start" };

            protected override void Given(IServiceContainer configurer)
            {
                var applicationStatusReporter = A.Fake<IApplicationStatusReporter>();
                A.CallToSet(() => applicationStatusReporter.HaveStarted).To(true).Invokes(() =>
                {
                    _started = true;
                    ServiceController.Stop();
                });
                configurer.Register(() => applicationStatusReporter);

                ServiceController.OnStopped += code =>
                {
                    _exitCode = code;
                };
            }

            [Fact]
            public void It_should_have_started_the_app()
            {
                _started.Should().Be.True();
            }

            [Fact]
            public void It_should_have_reported_the_exit_code()
            {
                _exitCode.Should().Equal(5);
            }
        }
    }

    namespace Given_a_windows_service_that_fails_to_start
    {
        public class When_testing : ExceptionSupressingServiceSpecification<DefaultWindowsServiceHostStarter<FailingOnStartWindowsServiceBuilder>>
        {
            private readonly List<Exception> _exceptionsCaught = new List<Exception>();

            [Fact]
            public void It_should_have_gotten_an_exception()
            {
                _exceptionsCaught.Should().Count.One();
                _exceptionsCaught.Should().Contain.One(exception => exception.Message.Equals("Failing to start"));
            }

            protected override void OnException(Exception exception)
            {
                _exceptionsCaught.Add(exception);
            }
        }
    }

    namespace Given_a_windows_service_that_fails_to_stop
    {
        public class When_testing : ExceptionSupressingServiceSpecification<DefaultWindowsServiceHostStarter<FailingOnStopWindowsServiceBuilder>>
        {
            private readonly List<Exception> _exceptionsCaught = new List<Exception>();

            protected override void When()
            {
                ServiceController.Stop();
            }

            [Fact]
            public void It_should_have_gotten_an_exception()
            {
                _exceptionsCaught.Should().Count.One();
                _exceptionsCaught.Should().Contain.One(exception => exception.Message.Equals("Failing to stop"));
            }

            protected override void OnException(Exception exception)
            {
                _exceptionsCaught.Add(exception);
            }
        }
    }

    namespace Given_a_test_that_times_out
    {
        public class When_testing : ExceptionSupressingServiceSpecification<DefaultWindowsServiceHostStarter<EmptyWindowsServiceBuilder>>
        {
            private readonly List<Exception> _exceptionsCaught = new List<Exception>();

            protected override TimeSpan Timeout { get; set; } = TimeSpan.FromMilliseconds(1);

            [Fact]
            public void It_should_have_gotten_an_exception()
            {
                _exceptionsCaught.Should().Count.One();
                _exceptionsCaught.OfType<TimeoutException>().Should().Count.One();
                _exceptionsCaught.OfType<TimeoutException>().First().Message.Should().Contain("stop ");
            }

            protected override void OnException(Exception exception)
            {
                _exceptionsCaught.Add(exception);
            }
        }

        public class When_stopping : ExceptionSupressingServiceSpecification<DefaultWindowsServiceHostStarter<SlowStoppingWindowsServiceBuilder>>
        {
            private readonly List<Exception> _exceptionsCaught = new List<Exception>();

            protected override TimeSpan StopTimeout { get; set; } = TimeSpan.FromMilliseconds(1);

            protected override void When()
            {
                ServiceController.Stop();
            }

            [Fact]
            public void It_should_have_gotten_an_exception()
            {
                _exceptionsCaught.Should().Count.One();
                _exceptionsCaught.OfType<TimeoutException>().Should().Count.One();
                _exceptionsCaught.OfType<TimeoutException>().First().Message.Should().Contain("stopped ");
            }

            protected override void OnException(Exception exception)
            {
                _exceptionsCaught.Add(exception);
            }
        }
    }
}