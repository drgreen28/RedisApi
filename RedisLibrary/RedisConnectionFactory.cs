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
            connection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(redis.Value.Host));
        }

        public ConnectionMultiplexer Connection()
        {
            return connection.Value;
        }
    }
}
