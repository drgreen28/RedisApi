using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RedisLibrary
{
    public class RedisService<T> : BaseService<T>, IRedisService<T>
    {
        internal readonly IDatabase Db;
        protected readonly IRedisConnectionFactory ConnectionFactory;

        public RedisService(IRedisConnectionFactory connectionFactory)
        {
            ConnectionFactory = connectionFactory;
            Db = ConnectionFactory.Connection().GetDatabase();
        }

        public void Delete(string key)
        {
            if (string.IsNullOrWhiteSpace(key) || key.Contains(":")) throw new ArgumentException("Invalid Table");

            key = GenerateKey(key);
            Db.KeyDelete(key);
        }

        public string GetString(string key)
        {
            return Db.StringGet(key);
        }

        public void SaveString(string key, string value)
        {
            if (value != null)
                Db.StringSet(key, value);
        }

        public T Get(string key)
        {
            key = this.GenerateKey(key);
            var hash = Db.HashGetAll(key);
            return this.MapFromHash(hash);
        }

        public void Save(string key, T obj)
        {
            if(obj != null)
            {
                var hash = GenerateHash(obj);
                key = GenerateKey(key);

                if (Db.HashLength(key) == 0)
                    Db.HashSet(key, hash);
                else
                {
                    var props = Properties;
                    foreach (var item in props)
                        if (Db.HashExists(key, item.Name))
                            Db.HashIncrement(key, item.Name, Convert.ToInt32(item.GetValue(obj)));
                }
            }
        }

        public async Task<ServiceResponse> ExpireKey(string key, TimeSpan timeSpan)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                await Db.KeyExpireAsync(key, timeSpan);
                response.Message = "Success";
                response.Success = true;
            }
            catch(Exception e)
            {
                response.Success = false;
                response.Message = "Failed: " + e.Message;
            }

            return response;
        }

        public async Task<ServiceResponse> SaveTable(string table, string obj, string key)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                if(obj != null)
                {
                    var hash = new HashEntry[1];
                    hash[0] = new HashEntry(key, obj);

                    await Db.HashSetAsync(table, hash);
                    response.Message = "Success";
                    response.Success = true;
                }
            }
            catch(Exception e)
            {
                response.Success = false;
                response.Message = "Failed: " + e.Message;
            }

            return response;
        }

        public async Task<ServiceResponse> GetTable(string table)
        {
            ServiceResponse response = new ServiceResponse();
            var list = new List<string>();

            try
            {
                var values = await Db.HashValuesAsync(table);
                foreach (var value in values)
                    list.Add(value);

                response.ReturnValue = list;
                response.Success = true;
            }
            catch(Exception e)
            {
                response.Success = false;
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<ServiceResponse> GetTable(string table, string key)
        {
            ServiceResponse response = new ServiceResponse();
            var list = new List<string>();

            try
            {
                var values = Db.HashScan(table, key);
                foreach (var value in values)
                    list.Add(value.Value.ToString());

                response.ReturnValue = list;
                response.Success = true;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<ServiceResponse> DeleteTableRow(string table, string key)
        {
            ServiceResponse response = new ServiceResponse();
            var list = new List<string>();

            try
            {
                await Db.HashDeleteAsync(table, key);
                response.Success = true;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<ServiceResponse> GeoAdd(string table, double longitude, double latitude, string key)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                if (true)
                {
                    RedisValue value = new RedisValue(key);
                    await Db.GeoAddAsync(table, longitude, latitude, value);
                    response.Message = "Success";
                    response.Success = true;
                }
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = "Failed: " + e.Message;
            }

            return response;
        }
    }
}
