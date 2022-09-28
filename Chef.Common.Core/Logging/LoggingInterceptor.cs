using System;
using System.Linq;
using Castle.DynamicProxy;

namespace Chef.Common.Core.Logging
{
    public class LoggingInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine($">> {invocation.Method.Name}");

            // Invoke the method
            invocation.Proceed();

            Console.WriteLine($"<< {invocation.Method.Name}");
        }
    }
}

