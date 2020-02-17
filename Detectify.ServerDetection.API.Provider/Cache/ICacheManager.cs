using System;
using System.Collections.Generic;
using System.Text;

namespace Detectify.ServerDetection.API.Provider
{
    public interface ICacheManager
    {
        /// <summary>
        /// Get the Cache By Key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T Get<T>(string key);

        /// <summary>
        /// Adds Object into Cache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="data"></param>
        void Put<T>(string key, T data);


        /// <summary>
        /// Remove Object from Cache
        /// </summary>
        /// <param name="key"></param>
        void Remove(string key);


        /// <summary>
        /// Check If Key Exists
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool CheckIfKeyExists(string key);
    }
}
