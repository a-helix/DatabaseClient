using Newtonsoft.Json;
using NUnit.Framework;


namespace DatabaseClient.Tests
{
    public class SubscriptionUnitOfWorkTest
    {
        SubscriptionDatabaseEmulator db;
        SubscriptionUnitOfWorkMog testUnit;
        Subscription test;
        SubscriptionMog testMog;

        [SetUp]
        public void Setup()
        {
            db = new SubscriptionDatabaseEmulator();
            testUnit = new SubscriptionUnitOfWorkMog(db);
            test = new Subscription();
            test.ID = "ID 1";
            test.UserID = "User 1";
            test.Location = "Location 1";
            test.RequestsPerHour = 1;
            test.Active = true;
            test.Status = "Running";
            test.CreatedAt = 10;
            test.ExpiredAt = 20;
            test.LastSent = 30;
            testMog = new SubscriptionMog(test);
        }

        [Test]
        public void CommitTest()
        {
            Assert.AreEqual(testUnit.CreateList().Count, 0);
            Assert.AreEqual(testUnit.UpdateList().Count, 0);
            Assert.AreEqual(testUnit.DeleteList().Count, 0);
            Assert.AreEqual(testUnit.ReadDict().Count, 0);
            testUnit.Add(test);
            testUnit.Modify(test);
            Assert.AreEqual(testUnit.CreateList().Count, 1);
            Assert.AreEqual(testUnit.UpdateList().Count, 1);
            Assert.AreEqual(testUnit.DeleteList().Count, 0);
            Assert.AreEqual(testUnit.ReadDict().Count, 0);
            testUnit.Commit();
            Assert.AreEqual(testUnit.CreateList().Count, 0);
            Assert.AreEqual(testUnit.UpdateList().Count, 0);
            Assert.AreEqual(testUnit.DeleteList().Count, 0);
            Assert.AreEqual(testUnit.ReadDict().Count, 0);
            testUnit.Remove(test.ID);
            testUnit.GetByID(test.ID);
            Assert.AreEqual(testUnit.CreateList().Count, 0);
            Assert.AreEqual(testUnit.UpdateList().Count, 0);
            Assert.AreEqual(testUnit.DeleteList().Count, 1);
            Assert.AreEqual(testUnit.ReadDict().Count, 1);
            testUnit.Commit();
            Assert.AreEqual(testUnit.CreateList().Count, 0);
            Assert.AreEqual(testUnit.UpdateList().Count, 0);
            Assert.AreEqual(testUnit.DeleteList().Count, 0);
            Assert.AreEqual(testUnit.ReadDict().Count, 0);
        }

        [Test]
        public void AddTest()
        {
            Assert.AreEqual(testUnit.CreateList().Count, 0);
            testUnit.Add(test);
            Assert.AreEqual(testUnit.CreateList().Count, 1);
            testUnit.Commit();
            Assert.AreEqual(testUnit.CreateList().Count, 0);
        }

        [Test]
        public void GetbyIdTest()
        {
            Assert.AreEqual(testUnit.ReadDict().Count, 0);
            testUnit.Add(test);
            testUnit.Commit();
            var feedback = testUnit.GetByID(test.ID);
            Assert.AreEqual(testUnit.ReadDict().Count, 1);
            var feedbackMog = new SubscriptionMog(feedback);
            Assert.AreEqual(test.ID, feedback.ID);
            Assert.IsTrue(testMog.Equals(feedbackMog));
            Assert.IsNull(testUnit.GetByID("not exhist"));
        }

        [Test]
        public void RemoveTest()
        {
            Assert.IsFalse(testUnit.DeleteList().Contains(test.ID));
            testUnit.Remove(test.ID);
            Assert.IsTrue(testUnit.DeleteList().Contains(test.ID));
            testUnit.Commit();
            Assert.IsFalse(testUnit.DeleteList().Contains(test.ID));
        }

        [Test]
        public void MOdifyTest()
        {
            SubscriptionMog testMog = new SubscriptionMog(test);
            testUnit.Add(test);
            testUnit.Commit();
            string testJson = JsonConvert.SerializeObject(test);
            Subscription modified = JsonConvert.DeserializeObject<Subscription>(testJson);
            modified.Location = "Updated location";
            var modifiedMog = new SubscriptionMog(modified);
            var feedback = testUnit.GetByID(test.ID);
            var feedbackMog = new SubscriptionMog(feedback);
            Assert.IsTrue(feedbackMog.Equals(testMog));
            Assert.AreEqual(testUnit.UpdateList().Count, 0);
            testUnit.Modify(modified);
            Assert.AreEqual(testUnit.UpdateList().Count, 1);
            testUnit.Commit();
            Assert.AreEqual(testUnit.UpdateList().Count, 0);
            feedback = testUnit.GetByID(test.ID);
            feedbackMog = new SubscriptionMog(feedback);
            Assert.IsTrue(feedbackMog.Equals(modifiedMog));
            Assert.IsFalse(feedbackMog.Equals(testMog));
        }
    }
}
