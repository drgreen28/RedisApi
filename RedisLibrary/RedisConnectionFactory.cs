using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;

namespace RedisLibrary
{
    public class RedisConnectionFactory : IRedisConnectionFactory
    {
        private readonly Lazy<ConnectionMultiplexer> connection;

        //private readonly IOptions<RedisConfiguration> redis;

        public RedisConnectionFactory(IOptions<RedisConfiguration> redis)
        {
            ConfigurationOptions options = new ConfigurationOptions()
            {
                Password = redis.Value.Password,
                EndPoints = { { redis.Value.Host, redis.Value.Port } }
            };
            connection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(options));
        }

        public ConnectionMultiplexer Connection()
        {
            return connection.Value;
        }
    }
}
