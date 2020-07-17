using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedisLibrary
{
    //public partial class ServiceResponse
    //{
    //    public bool Success { get; set; }
    //    public string Message { get; set; }
    //    public Exception Exception { get; set; }
    //    public ServiceResponse() { }
    //    public ServiceResponse(bool success)
    //    {
    //        Success = success;
    //    }
    //    public ServiceResponse(bool success, string message)
    //    {
    //        Success = success;
    //        Message = message;
    //    }
    //}

    public partial class ServiceResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> ReturnValue { get; set; }
        public Exception Exception { get; set; }
        public ServiceResponse()
        {

        }
    }
}
