using System;
using System.Collections.Generic;
using System.Text;

namespace RedisLibrary.Models
{
    public class RedisGeoLocation
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Key { get; set; }
    }
}
