using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Common.Core.Repositories
{
    public class RedisConnectionFactory : IRedisConnectionFactory
    {
        private readonly ConfigurationOptions configuration = null;
        private Lazy<IConnectionMultiplexer> _Connection = null;
        public RedisConnectionFactory( bool allowAdmin = false)
        {
            configuration = new ConfigurationOptions()
            {
                //for the redis pool so you can extent later if needed
                EndPoints = { { ConfigurationManager.AppSetting["Redis:Host"], Convert.ToInt32(ConfigurationManager.AppSetting["Redis:Port"]) }, },
                AllowAdmin = allowAdmin,
                //Password = "", //to the security for the production
                ReconnectRetryPolicy = new LinearRetry(5000),
                AbortOnConnectFail = false
            };
            _Connection = new Lazy<IConnectionMultiplexer>(() =>
            {
                ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(configuration);
                //redis.ErrorMessage += _Connection_ErrorMessage;
                //redis.InternalError += _Connection_InternalError;
                //redis.ConnectionFailed += _Connection_ConnectionFailed;
                //redis.ConnectionRestored += _Connection_ConnectionRestored;
                return redis;
            });
        }

        //for the 'GetSubscriber()' and another Databases
        public IConnectionMultiplexer Connection { get { return _Connection.Value; } }

        //for the default database
        public IDatabase Database => Connection.GetDatabase();
        //Get Data From Redis
        public List<T> GetData<T>(RedisKey key, CommandFlags flags = CommandFlags.None)
        {
            RedisValue rv = Database.StringGet(key, flags);

            if (!rv.HasValue)
                return default;
            List<T> rgv = JsonConvert.DeserializeObject<List<T>>(rv);
            return rgv;
        }
        //Set Data To Redis
        public bool SetData(RedisKey key, object value, TimeSpan? expiry = null, When when = When.Always, CommandFlags flags = CommandFlags.None)
        {
            if (value == null) return false;
            return Database.StringSet(key, JsonConvert.SerializeObject(value), expiry, when, flags);
        }
        public void ConnectionErrorMessage(object sender, RedisErrorEventArgs e)
        {
            throw new NotImplementedException();
        }
        //Invalidate Data In Redis
        public bool DeleteKey(RedisKey key)
        {
            return Database.KeyDelete(key);
        }
    }
    public static class ConfigurationManager
    {
        public static IConfiguration AppSetting { get; }
        static ConfigurationManager()
        {
            AppSetting = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
        }
    }
}
