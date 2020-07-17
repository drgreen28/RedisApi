//using Microsoft.OpenApi.Any;
//using Microsoft.OpenApi.Exceptions;
//using Microsoft.OpenApi.Models;
//using Newtonsoft.Json;
//using Swashbuckle.AspNetCore.SwaggerGen;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace RedisApi
//{
//    public class ServiceResponseFilter : ISchemaFilter
//    {
//        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
//        {
//            Exception e = new Exception("Message");
//            schema.Example = new OpenApiObject
//            {
//                ["Success"] = new OpenApiBoolean(true),
//                ["Message"] = new OpenApiString("An awesome product"),
//                ["ReturnValue"] = new OpenApiArray(),
//                ["Exception"] = new OpenApiReference()
//            };
//        }
//    }
//}
