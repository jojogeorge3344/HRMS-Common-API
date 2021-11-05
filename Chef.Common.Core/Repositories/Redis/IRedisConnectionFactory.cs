using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Common.Core.Repositories
{
    public interface IRedisConnectionFactory
    {
        List<T> GetData<T>(RedisKey key, CommandFlags flags = CommandFlags.None);

        bool SetData(RedisKey key, object value, TimeSpan? expiry = null, When when = When.Always, CommandFlags flags = CommandFlags.None);

        void ConnectionErrorMessage(object sender, RedisErrorEventArgs e);
        bool DeleteKey(RedisKey key);
    }
}
