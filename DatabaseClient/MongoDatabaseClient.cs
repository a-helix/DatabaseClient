using System.Collections.Generic;
using Credentials;
using Repository;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DatabaseClient
{
    public class MongoDatabaseClient : IRepository<ApiResponse>
    {
        protected MongoClient _client;
        protected IMongoDatabase _database;
        protected IMongoCollection<BsonDocument> _collection;

        public MongoDatabaseClient(string configPath, string database, string collection)
        {   
            var databaseConfig = new JsonFileContent(configPath);
            var url = databaseConfig.Value("databaseUrl").ToString();
            _client = new MongoClient(url);
            _database = _client.GetDatabase(database);
            _collection = _database.GetCollection<BsonDocument>(collection);
        }

        public void Create(ApiResponse coordinates)
        {
            BsonDocument document = BsonDocument.Parse(coordinates.ToString());
            _collection.InsertOne(document);
        }

        public ApiResponse Read(string location)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("area", location);
            var results = _collection.Find(filter).FirstOrDefault();
            Dictionary<string, object> dict = results.ToDictionary();
            var stringDict = new Dictionary<string, string>();
            foreach (KeyValuePair<string, object> entry in dict)
            {
                stringDict.Add(entry.Key, entry.Value.ToString());
            }
            ApiResponse response = new ApiResponse(stringDict);
            return response;
        }

        public void Update(ApiResponse coordinates)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("geolocation", coordinates.Value("geolocation"));
            _collection.DeleteOne(filter);
            Create(coordinates);
        }

        public void Delete(string location)
        {
            var deleteFilter = Builders<BsonDocument>.Filter.Eq("geolocation", location);
            _collection.DeleteOne(deleteFilter);
        }
    }
}
