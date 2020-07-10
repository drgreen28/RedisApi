using StackExchange.Redis;

namespace RedisLibrary
{
    public interface IRedisConnectionFactory
    {
        ConnectionMultiplexer Connection();
    }
}
