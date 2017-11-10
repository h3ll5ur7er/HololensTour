using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Proto;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace TourBackend
{
    [TestClass]
    public class SyncActor_UnitTest
    {
        [TestMethod]
        public async Task SyncActor_needs_to_be_able_to_write_on_SyncObject()
        {

            var dict = new Dictionary<string, CodeObject>();
            var cd1 = new CodeObject("cd1", 1, new[] { 1f, 2f, 3f }, new[] { 4f, 5f, 6f });
            var cd2 = new CodeObject("cd2", 1, new[] { 4f, 7f, 8f }, new[] { 1f, 19f, 3f }); // Just build to "random" CodeObjects

            dict.Add("cd1", cd1);
            dict.Add("cd2", cd2);

            Assert.AreEqual(dict["cd1"], cd1); // Check that the dict contains the right CodeObjects
            Assert.AreEqual(dict["cd2"], cd2);

            var obj = new SyncObject("sync1", new Dictionary<string, CodeObject>());

            CollectionAssert.AreEqual((System.Collections.ICollection)obj.dict, (System.Collections.ICollection)new Dictionary<string, CodeObject>()); // Check that SyncObject has no dict

            var propsSyncActor1 = Actor.FromProducer(() => new SyncActor("SyncActor1", obj));
            var pidSyncActor1 = Actor.Spawn(propsSyncActor1);

            var reply = await pidSyncActor1.RequestAsync<RespondWriteCurrentTourState>(new WriteCurrentTourState("dab", dict), TimeSpan.FromSeconds(1));

            pidSyncActor1.Stop();

            CollectionAssert.AreEqual((System.Collections.ICollection)obj.dict, (System.Collections.ICollection)dict);

        }

        [TestMethod]
        public async Task SyncActor_needs_to_write_different_timestamps_for_different_writes()
        {
            var dict = new Dictionary<string, CodeObject>();
            var cd1 = new CodeObject("cd1", 1, new[] { 1f, 2f, 3f }, new[] { 4f, 5f, 6f });
            var cd2 = new CodeObject("cd2", 1, new[] { 4f, 7f, 8f }, new[] { 1f, 19f, 3f }); // Just build two "random" CodeObjects
            var cd3 = new CodeObject("cd3", 2, new[] { 57f, 34f, 124f }, new[] { 1235f,35f, 757f }); // Just build to "random" CodeObjects

            dict.Add("cd1", cd1);
            dict.Add("cd2", cd2);

            Assert.AreEqual(dict["cd1"], cd1); // Check that the dict contains the right CodeObjects
            Assert.AreEqual(dict["cd2"], cd2);

            var obj = new SyncObject("sync1", new Dictionary<string, CodeObject>());

            CollectionAssert.AreEqual((System.Collections.ICollection)obj.dict, (System.Collections.ICollection)new Dictionary<string, CodeObject>()); // Check that SyncObject has no dict

            var propsSyncActor1 = Actor.FromProducer(() => new SyncActor("SyncActor1", obj));
            var pidSyncActor1 = Actor.Spawn(propsSyncActor1);

            var reply = await pidSyncActor1.RequestAsync<RespondWriteCurrentTourState>(new WriteCurrentTourState("dab", dict), TimeSpan.FromSeconds(1));

            var timestamp1 = obj.timestamp;

            Thread.Sleep(1); // Actors might be faster than 1 ms, but 1 ms is maximal precision of stopwatch

            dict.Add("cd3", cd3);
            var reply2 = await pidSyncActor1.RequestAsync<RespondWriteCurrentTourState>(new WriteCurrentTourState("dab2", dict), TimeSpan.FromSeconds(1));

            var timestamp2 = obj.timestamp;

            pidSyncActor1.Stop();

            Assert.AreNotEqual(timestamp1,timestamp2);

        }
        [TestMethod]
        public async Task SyncActor_needs_to_respond_after_writing_on_SyncObject()
        {

            var dict = new Dictionary<string, CodeObject>();
            var cd1 = new CodeObject("cd1", 1, new[] { 1f, 2f, 3f }, new[] { 4f, 5f, 6f });
            var cd2 = new CodeObject("cd2", 1, new[] { 4f, 7f, 8f }, new[] { 1f, 19f, 3f }); // Just build to "random" CodeObjects

            dict.Add("cd1", cd1);
            dict.Add("cd2", cd2);

            Assert.AreEqual(dict["cd1"], cd1); // Check that the dict contains the right CodeObjects
            Assert.AreEqual(dict["cd2"], cd2);

            var obj = new SyncObject("sync1", null);

            CollectionAssert.AreEqual(obj.dict, null); // Check that SyncObject has no dict

            var propsSyncActor1 = Actor.FromProducer(() => new SyncActor("SyncActor1", obj));
            var pidSyncActor1 = Actor.Spawn(propsSyncActor1);

            var reply = await pidSyncActor1.RequestAsync<RespondWriteCurrentTourState>(new WriteCurrentTourState("req1", dict), TimeSpan.FromSeconds(1)); // Expect Answer

            pidSyncActor1.Stop();

            Assert.AreEqual(reply.id, "req1");

        }

    }
}
