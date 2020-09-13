using System;
using System.Threading.Tasks;

namespace Chef.Common.Repositories
{
    public class Retry
    {
        public static T Do<T>(Func<T> func)
        {
            if (func == null)
                throw new ArgumentNullException(nameof(func));

            return func();
        }

        public static async Task<T> DoAsync<T>(Func<Task<T>> func)
        {
            if (func == null)
                throw new ArgumentNullException(nameof(func));

            return await func();
        }

        public static void Do(Action action)
            => Do(() =>
            {
                action();
                return true;
            });

        public static async Task DoAsync(Func<Task> action)
            => await DoAsync(async () =>
            {
                await action();
                return true;
            });
    }
}
