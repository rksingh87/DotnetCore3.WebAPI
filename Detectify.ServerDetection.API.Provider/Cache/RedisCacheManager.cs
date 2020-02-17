using Detectify.ServerDetection.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;

namespace Detectify.ServerDetection.API.Provider
{
    public class RedisCacheManager : ICacheManager
    {
        private readonly IDatabase cache;

        private readonly Lazy<ConnectionMultiplexer> LazyRedisConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            string cacheConnection = AppSettings.ConnectionString_Redis;
            return ConnectionMultiplexer.Connect(cacheConnection);
        });


        public RedisCacheManager()
        {
            cache = LazyRedisConnection.Value.GetDatabase();
        }

        public bool CheckIfKeyExists(string key)
        {
            return cache.KeyExists(key);
        }

        public T Get<T>(string key)
        {
            if (this.CheckIfKeyExists(key))
                return JsonConvert.DeserializeObject<T>(cache.StringGet(key));
            else
                return default;
        }

        public void Put<T>(string key, T data)
        {
            if (this.CheckIfKeyExists(key))
                this.Remove(key);
            cache.StringSet(key, JsonConvert.SerializeObject(data));
        }

        public void Remove(string key)
        {
            cache.KeyDelete(key);
        }
    }
}
