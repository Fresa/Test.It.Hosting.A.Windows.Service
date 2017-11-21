using System;
using System.Collections.Generic;
using Test.It.While.Hosting.Your.Windows.Service.Delegates;

namespace Test.It.While.Hosting.Your.Windows.Service
{
    internal class DefaultWindowsServiceController : IWindowsServiceController
    {
        public DefaultWindowsServiceController()
        {
            Client = new DefaultWindowsServiceClient(this);
        }

        private event StopHandler StopPrivate;
        public event StopHandler OnStop
        {
            add
            {
                lock (_stopLock)
                {
                    value.Invoke();
                    StopPrivate += value;
                }
            }
            remove
            {
                lock (_stopLock)
                {
                    StopPrivate -= value;
                }
            }
        }

        private readonly object _stopLock = new object();

        public void Stop()
        {
            lock (_stoppedLock)
            {
                _stopped = true;
            }
            StopPrivate?.Invoke();
        }

        private event StoppedHandler StoppedPrivate;
        public event StoppedHandler OnStopped
        {
            add
            {
                lock (_stoppedLock)
                {
                    if (_stopped)
                    {
                        value.Invoke(0);
                    }
                    StoppedPrivate += value;
                }
            }
            remove
            {
                lock (_stoppedLock)
                {
                    StoppedPrivate -= value;
                }
            }
        }

        private readonly object _stoppedLock = new object();
        private bool _stopped;

        public void Stopped(int exitCode)
        {
            lock (_stoppedLock)
            {
                if (_stopped)
                {
                    return;
                }
                _stopped = true;
            }
            StoppedPrivate?.Invoke(exitCode);
        }

        private event StartedHandler StartedPrivate;
        public event StartedHandler OnStarted
        {
            add
            {
                lock (_startedLock)
                {
                    if (_started)
                    {
                        value.Invoke(0);
                    }
                    StartedPrivate += value;
                }
            }
            remove
            {
                lock (_stoppedLock)
                {
                    StartedPrivate -= value;
                }
            }
        }

        private readonly object _startedLock = new object();
        private bool _started;

        public void Started(int exitCode)
        {
            lock (_startedLock)
            {
                if (_started)
                {
                    return;
                }
                _started = true;
            }
            StartedPrivate?.Invoke(exitCode);
        }

        private readonly List<Exception> _exceptionsRaised = new List<Exception>();
        private readonly object _exceptionLock = new object();
        private event ExceptionHandler OnExceptionPrivate;
        public event ExceptionHandler OnException
        {
            add
            {
                lock (_exceptionLock)
                {
                    foreach (var exception in _exceptionsRaised)
                    {
                        value.Invoke(exception);
                    }
                    OnExceptionPrivate += value;
                }
            }
            remove
            {
                lock (_exceptionLock)
                {
                    OnExceptionPrivate -= value;
                }
            }
        }

        public void RaiseException(Exception exception)
        {
            lock (_exceptionLock)
            {
                _exceptionsRaised.Add(exception);
            }
            OnExceptionPrivate?.Invoke(exception);
        }

        public IWindowsServiceClient Client { get; }
    }
}