using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RedisApi.Controllers;
using RedisLibrary;
using RedisLibrary.Models;
using System;
using System.Threading.Tasks;
using Xunit;

namespace RedisTest
{
    public class GeospatialTest
    {
        IConfiguration _config;
        IRedisConnectionFactory _factory;
        IOptions<RedisConfiguration> _options;

        public GeospatialTest()
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile("C:\\repos\\RedisApi\\RedisTest\\appsettings.Test.json")
                .Build();

            var redisConfig = new RedisConfiguration();

            redisConfig.Host = _config.GetValue<string>("redis:host");
            redisConfig.Port = _config.GetValue<int>("redis:port");


            _options = Options.Create(redisConfig);
            _factory = new RedisConnectionFactory(_options);
        }

        [Fact]
        public async Task TestGeoAddAsync()
        {
            LocationController controller = new LocationController(_config, _factory);

            RedisGeoLocation redisGeoLocation = new RedisGeoLocation { Key = "myTruck", Latitude = 36.12196, Longitude = -115.17172 };

            var test = await controller.GeoAdd("temp", redisGeoLocation);

            Assert.True(test.Success);
        }
    }
}
