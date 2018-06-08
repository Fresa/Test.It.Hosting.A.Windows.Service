using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.It.ApplicationBuilders;

namespace Test.It.While.Hosting.Your.Windows.Service
{
    internal class DefaultApplicationBuilder : IApplicationBuilder
    {
        private readonly Queue<IMiddleware> _middlewares = new Queue<IMiddleware>();

        public Func<IDictionary<string, object>, Task> Build()
        {
            if (_middlewares.Any() == false)
            {
                throw new InvalidOperationException("There are no middlewares defined in the pipeline.");
            }
            
            return Builder;
        }

        private Task Builder(IDictionary<string, object> environment)
        {
            if (_middlewares.Any() == false)
            {
                return Task.CompletedTask;
            }

            var nextMiddleware = _middlewares.Dequeue();
            nextMiddleware.Initialize(Builder);

            return nextMiddleware.Invoke(environment);
        }

        public void Use(IMiddleware middleware)
        {
            _middlewares.Enqueue(middleware);
        }
    }
}