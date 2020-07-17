using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using RedisLibrary;
using Swashbuckle.AspNetCore.Annotations;

namespace RedisApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        private readonly IRedisConnectionFactory Factory;
        public RedisController(IConfiguration configuration, IRedisConnectionFactory factory)
        {
            Configuration = configuration;
            Factory = factory;
        }



        [HttpGet("HealthCheck")]
        public IActionResult HealthCheck()
        {
            List<string> values = new List<string>();
            values.Add("Current configuration is for " + Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
            values.Add("Feature A is " + Configuration["Customer:FeatureA"]);
            values.Add("Feature B is " + Configuration["Customer:FeatureB"]);

            return new JsonResult(values);
        }

        [HttpPost("Table/{tableName}/{key}")]
        public async Task<ServiceResponse> CreateTableAsync(string tableName, string key, [FromBody] string value)
        {
            var redis = new RedisService<string>(Factory);
            var response = await redis.SaveTable(tableName, value, key);

            return response;
        }

        [HttpGet("Table/{tableName}")]
        public async Task<ServiceResponse> GetTableAsync(string tableName)
        {
            var redis = new RedisService<string>(Factory);
            var values = await redis.GetTable(tableName);

            return values;
        }

        [HttpGet("Table/{tableName}/{key}")]
        public async Task<ServiceResponse> GetTableAsync(string tableName, string key)
        {
            var redis = new RedisService<string>(Factory);
            var values = await redis.GetTable(tableName, key);

            return values;
        }

        [HttpDelete("Table/{tableName}/{key}")]
        public async Task<ServiceResponse> DeleteTableRowAsync(string tableName, string key)
        {
            var redis = new RedisService<string>(Factory);
            var response = await redis.DeleteTableRow(tableName, key);

            return response;
        }

        [HttpPost("Table/ExpireKey/{key}/{seconds}")]
        public async Task<ServiceResponse> ExpireKey(string key, int seconds)
        {
            var redis = new RedisService<string>(Factory);
            var values = await redis.ExpireKey(key, new TimeSpan(0, 0, seconds));

            return values;
        }


    }
}
