using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisLibrary
{
    public class ServiceResponseFilter : Swashbuckle.AspNetCore.SwaggerGen.ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            schema.Example = new OpenApiObject
            {
                ["Id"] = new OpenApiInteger(1),
                ["Description"] = new OpenApiString("An awesome product")
            };
        }
    }
}
