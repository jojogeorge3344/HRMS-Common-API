using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IO;

namespace Chef.Common.Core.Repositories
{
    public class RedisConnectionFactory : IRedisConnectionFactory
    {
        private readonly ConfigurationOptions configuration = null;
        private readonly Lazy<IConnectionMultiplexer> _connection = null;

        public RedisConnectionFactory(bool allowAdmin = false)
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

            _connection = new Lazy<IConnectionMultiplexer>(() =>
            {
                ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(configuration);

                return redis;
            });
        }

        //for the 'GetSubscriber()' and another Databases
        public IConnectionMultiplexer Connection { get { return _connection.Value; } }

        //for the default database
        public IDatabase Database => Connection.GetDatabase();

        //Get Data From Redis
        public List<T> GetData<T>(RedisKey key, CommandFlags flags = CommandFlags.None)
        {
            RedisValue redisValue = Database.StringGet(key, flags);

            if (!redisValue.HasValue)
            {
                return default;
            }

            List<T> deserializedValue = JsonConvert.DeserializeObject<List<T>>(redisValue);

            return deserializedValue;
        }

        //Set Data To Redis
        public bool SetData(RedisKey key, object value, TimeSpan? expiry = null, When when = When.Always, CommandFlags flags = CommandFlags.None)
        {
            if (value == null)
            {
                return false;
            }

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
