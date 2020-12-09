using Credentials;
using DatabaseClient;
using Repository;
using System;
using System.Collections.Generic;

namespace DatabaseClient.Tests
{
    public class ApiResponseDatabaseEmulator : IRepository<ApiResponse>
    {
        protected Dictionary<string, ApiResponse> _database;

        public ApiResponseDatabaseEmulator()
        {
            _database = new Dictionary<string, ApiResponse>();
        }

        public void Create(ApiResponse coordinates)
        {
            JsonStringContent geolocation = new JsonStringContent(coordinates.ToString());
            var key = Convert.ToString(geolocation.Value("area"));
            _database.Add(key, coordinates);
        }

        public void Delete(string location)
        {
            _database.Remove(location);
        }

        public ApiResponse Read(string location)
        {
            if(_database.ContainsKey(location))
                return _database[location];
            return null;
        }

        public void Update(ApiResponse coordinates)
        {
            try
            {
                var oldUnit = Read(Convert.ToString(coordinates.Value("area")));
                _database.Remove(Convert.ToString(oldUnit.Value("area")));
                _database.Add(Convert.ToString(coordinates.Value("area")), coordinates);
            }
            catch (Exception)
            {
                throw new KeyNotFoundException();
            }
        }

        public bool Contains(string area)
        {
            return _database.ContainsKey(area);
        }
    }
}
