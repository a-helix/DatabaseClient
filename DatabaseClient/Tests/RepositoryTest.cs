using System;
using System.Collections.Generic;
using DatabaseClient;
using NUnit.Framework;

namespace DatabaseClient.Tests
{
    class RepositoryTest
    {
        public RepositoryTest() : base()
        {

        }
        
        private ApiResponseDatabaseEmulator _db;

        [SetUp]
        public void Setup()
        {
            _db = new ApiResponseDatabaseEmulator();
        }

        [Test]
        public void ReadTest()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>()
            {
                {"area", "Shire" }
            };
            ApiResponse test = new ApiResponse(dict);
            Assert.IsNull(_db.Read("Shire"));
            _db.Create(test);
            Assert.IsTrue(_db.Read("Shire").Equals(test));
            Assert.IsNull(_db.Read("Not exhist."));
        }

        [Test]
        public void CreateTest()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>()
            {
                {"area", "Shire" }
            };
            ApiResponse test = new ApiResponse(dict);
            Assert.IsFalse(_db.Contains("Shire"));
            _db.Create(test);
            Assert.IsTrue(_db.Contains("Shire"));
        }

        [Test]
        public void DeleteTest()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>()
            {
                {"area", "Delete" }
            };
            ApiResponse test = new ApiResponse(dict);
            Assert.IsFalse(_db.Contains("Delete"));
            _db.Create(test);
            Assert.IsTrue(_db.Contains("Delete"));
            _db.Delete("Delete");
            Assert.IsFalse(_db.Contains("Delete"));
            _db.Delete("Not exhist.");
        }

        [Test]
        public void UpdateTest()
        {
            Dictionary<string, string> dictZero = new Dictionary<string, string>()
            {
                {"latitude",  "0" },
                {"longitude", "0" },
                {"geolocation", "0;1" },
                {"area", "Test" },
            };
            Dictionary<string, string> dictOne = new Dictionary<string, string>()
            {
                {"latitude",  "0" },
                {"longitude", "0" },
                {"geolocation", "0;0" },
                {"area", "Test" },
            };
            ApiResponse testValue1 = new ApiResponse(dictZero);
            ApiResponse testValue2 = new ApiResponse(dictOne);
            _db.Create(testValue1);
            Assert.IsTrue(_db.Contains("Test"));
            var feedback = _db.Read("Test");
            Assert.AreEqual(
                Convert.ToString(feedback.Value("geolocation")),
                Convert.ToString(testValue1.Value("geolocation"))
                );
            _db.Update(testValue2);
            feedback = _db.Read("Test");
            Assert.AreEqual(
                Convert.ToString(feedback.Value("geolocation")),
                Convert.ToString(testValue2.Value("geolocation"))
                );
            Dictionary<string, string> dictNotExhist = new Dictionary<string, string>()
            {
                {"latitude",  "0" },
                {"longitude", "0" },
                {"geolocation", "0;0" },
                {"area", "not exhist" },
            };
            ApiResponse testValue3 = new ApiResponse(dictNotExhist);
            Assert.Throws<KeyNotFoundException>(() => _db.Update(testValue3));
        }
    }
}
