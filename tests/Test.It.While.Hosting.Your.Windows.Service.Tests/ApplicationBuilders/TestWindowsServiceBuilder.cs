using System;
using System.Threading.Tasks;
using Test.It.Specifications;
using Test.It.While.Hosting.Your.Windows.Service.Tests.Applications;

namespace Test.It.While.Hosting.Your.Windows.Service.Tests.ApplicationBuilders
{
    public class TestWindowsServiceBuilder : DefaultWindowsServiceBuilder
    {
        public override IWindowsService Create(ITestConfigurer configurer)
        {
            var app = new TestWindowsServiceApp(configurer.Configure);

            return new TestWindowsServiceWrapper(app);
        }

        private class TestWindowsServiceWrapper : IWindowsService
        {
            private readonly TestWindowsServiceApp _app;

            public TestWindowsServiceWrapper(TestWindowsServiceApp app)
            {
                _app = app;
            }
            
            public int Start(params string[] args)
            {
                Task.Run(() => _app.Start(args))
                    .ContinueWith(task => OnUnhandledException?.Invoke(task.Exception), 
                        TaskContinuationOptions.OnlyOnFaulted);
                return 0;
            }

            public int Stop()
            {
                return _app.Stop();
            }

            public event Action<Exception> OnUnhandledException;
        }
    }
}