using System;
using System.Linq;
using Castle.DynamicProxy;
using Microsoft.Extensions.Logging;

namespace Chef.Common.Core.Logging
{
    public class LoggingInterceptor : IInterceptor
    {
        private readonly ILogger<LoggingInterceptor> logger;

        public LoggingInterceptor(ILogger<LoggingInterceptor> logger)
        {
            this.logger = logger;
        }

        public void Intercept(IInvocation invocation)
        {
            //TODO optimize for the production.
            var watch = System.Diagnostics.Stopwatch.StartNew();

            logger.LogInformation($">> {invocation.TargetType.FullName}.{invocation.Method.Name}");

            // Invoke the method
            invocation.Proceed();

            watch.Stop();

            logger.LogInformation($"<< {invocation.TargetType.FullName}.{invocation.Method.Name} :: {watch.ElapsedMilliseconds} ms");
        }
    }
}

