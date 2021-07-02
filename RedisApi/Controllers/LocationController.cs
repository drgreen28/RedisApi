using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RedisLibrary;
using RedisLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        private readonly IRedisConnectionFactory Factory;
        public LocationController(IConfiguration configuration, IRedisConnectionFactory factory)
        {
            Configuration = configuration;
            Factory = factory;
        }

        [HttpPost("GeoAdd/{tableName}/{key}")]
        public async Task<ServiceResponse> GeoAdd(string tableName, [FromBody] RedisGeoLocation value)
        {
            var redis = new RedisService<string>(Factory);
            var response = await redis.GeoAdd(tableName, value.Longitude, value.Latitude, value.Key);

            return response;
        }

        [HttpPost("GeoDist/{tableName}/{car}/{waypoint}")]
        public async Task<ServiceResponse> GeoDist(string tableName, string car, string waypoint)
        {
            var redis = new RedisService<string>(Factory);
            var response = await redis.GeoDist(tableName, car, waypoint);

            return response;
        }
    }
}
