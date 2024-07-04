using System.Text.Json;
using Api.Core.ServicesInterfaces;
using StackExchange.Redis;

namespace Api.Core.Services
{
    public class CacheService : ICachingInterface
    {   
        private IDatabase _cacheDb;

        public CacheService()
        {
            var redis = ConnectionMultiplexer.Connect("localhost:6379");
            _cacheDb = redis.GetDatabase();
        }
        public T GetData<T>(string key)
        {
            var value = _cacheDb.StringGet(key);

            if(!string.IsNullOrEmpty(value))
                return JsonSerializer.Deserialize<T>(value);

            return default;    
        }

        public object RemoveData(string key)
        {
            var exist = _cacheDb.KeyExists(key);

            if(exist)
                return _cacheDb.KeyDelete(key);

            return false;
        }

        public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            var expiryTime = expirationTime.DateTime.Subtract(DateTime.Now);

            return  _cacheDb.StringSet(key, JsonSerializer.Serialize(value), expiryTime);

            
        }
    }


}