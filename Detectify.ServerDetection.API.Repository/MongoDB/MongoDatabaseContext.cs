using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Detectify.ServerDetection.API.Repository.MongoDB
{

    public class MongoDatabaseContext : IDisposable
    {
        private MongoDatabaseContext() { }

        private IMongoDatabase Database { get; set; }

        public MongoDatabaseContext(string connectionString, string dbName)
        {
            this.Database = new MongoClient(connectionString).GetDatabase(dbName); ;
        }

        public async Task InsertDocumentAsync<T>(string collectionName, T bsonElements)
        {
            await this.Database.GetCollection<T>(collectionName).InsertOneAsync(bsonElements);
        }

        public async Task InsertDocumentsAsync<T>(string collectionName, IEnumerable<T> bsonElements)
        {
            await this.Database.GetCollection<T>(collectionName).InsertManyAsync(bsonElements);
        }

        public async Task<List<T>> GetDocumentsAsync<T>(string collectionName, FilterDefinition<T> filterDefinition)
        {
            List<T> list = new List<T>();
            using (var cursor = await this.Database.GetCollection<T>(collectionName).Find(filterDefinition).ToCursorAsync())
            {
                while (await cursor.MoveNextAsync())
                {
                    foreach (var doc in cursor.Current)
                    {
                        list.Add(doc);
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName"></param>
        /// <returns></returns>
        public IQueryable<T> GetQueryableCollection<T>(string collectionName)
        {
            return this.Database.GetCollection<T>(collectionName).AsQueryable<T>();
        }

        /// <summary>
        /// Get First Document Async
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName"></param>
        /// <param name="filterDefinition"></param>
        /// <returns></returns>
        public async Task<T> GetFirstDocumentAsync<T>(string collectionName, FilterDefinition<T> filterDefinition)
        {
            T data = default;
            using (var cursor = await this.Database.GetCollection<T>(collectionName).Find(filterDefinition).ToCursorAsync())
            {
                while (await cursor.MoveNextAsync())
                {
                    foreach (var doc in cursor.Current)
                    {
                        data = doc;
                        break;
                    }
                    break;
                }
            }
            return data;
        }

        /// <summary>
        /// Update Documents Async
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName"></param>
        /// <param name="filterDefinition"></param>
        /// <param name="newDocument"></param>
        /// <returns></returns>
        public async Task UpdateDocumentsAsync<T>(string collectionName, FilterDefinition<T> filterDefinition, T newDocument)
        {
            await this.Database.GetCollection<T>(collectionName).ReplaceOneAsync(filterDefinition, newDocument);
        }


        public async Task UpdateManyAsync<T>(string collectionName, FilterDefinition<T> filterDefinition, UpdateDefinition<T> updateDefinition)
        {
            await this.Database.GetCollection<T>(collectionName).UpdateManyAsync(filterDefinition, updateDefinition);
        }

        /// <summary>
        /// Delete Multiple Documents
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName"></param>
        /// <param name="filterDefinition"></param>
        /// <returns></returns>
        public async Task DeleteDocumentsAsync<T>(string collectionName, FilterDefinition<T> filterDefinition)
        {
            await this.Database.GetCollection<T>(collectionName).DeleteManyAsync(filterDefinition);
        }



        /// <summary>
        /// Retrurns True if Data Exists with Given Filter Definition
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filterDefinition"></param>
        /// <returns></returns>
        public async Task<bool> CheckIfExists<T>(string collectionName, FilterDefinition<T> filterDefinition)
        {
            return (await this.Database.GetCollection<T>(collectionName).Find(filterDefinition).ToListAsync()).Count() > 0;
        }

        /// <summary>
        /// Dispose Object
        /// </summary>
        public void Dispose()
        {
            this.Database = null;
        }
    }
}
