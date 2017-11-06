using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Proto;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TourBackend
{
    [TestClass]
    public class SyncActor_UnitTest
    {
        [TestMethod]
        public void SyncActor_needs_to_be_able_to_write_on_SyncObject()
        {

            var dict = new Dictionary<string, CodeObject>();
            var cd1 = new CodeObject("cd1", 1, new[] { 1, 2, 3 }, new[] { 4, 5, 6 });
            var cd2 = new CodeObject("cd2", 1, new[] { 4, 7, 8 }, new[] { 1, 19, 3 }); // Just build to "random" CodeObjects

            dict.Add("cd1", cd1);
            dict.Add("cd2", cd2);

            Assert.AreEqual(dict["cd1"], cd1); // Check that the dict contains the right CodeObjects
            Assert.AreEqual(dict["cd2"], cd2);

            var obj = new SyncObject("sync1", new Dictionary<string, CodeObject>());

            CollectionAssert.AreEqual((System.Collections.ICollection)obj.dict, (System.Collections.ICollection)new Dictionary<string, CodeObject>()); // Check that SyncObject has no dict

            var propsSyncActor1 = Actor.FromProducer(() => new SyncActor("SyncActor1", ref obj));
            var pidSyncActor1 = Actor.Spawn(propsSyncActor1);

            pidSyncActor1.Tell(new WriteCurrentTourState("dab",dict));

            CollectionAssert.AreEqual((System.Collections.ICollection) obj.dict, (System.Collections.ICollection) dict);

    }
    public async void SyncActor_needs_to_respond_after_writing_on_SyncObject()
    {

        var dict = new Dictionary<string, CodeObject>();
        var cd1 = new CodeObject("cd1", 1, new[] { 1, 2, 3 }, new[] { 4, 5, 6 });
        var cd2 = new CodeObject("cd2", 1, new[] { 4, 7, 8 }, new[] { 1, 19, 3 }); // Just build to "random" CodeObjects

        dict.Add("cd1", cd1);
        dict.Add("cd2", cd2);

        Assert.AreEqual(dict["cd1"], cd1); // Check that the dict contains the right CodeObjects
        Assert.AreEqual(dict["cd2"], cd2);

        var obj = new SyncObject("sync1", null);

        CollectionAssert.AreEqual(obj.dict, null); // Check that SyncObject has no dict

        var propsSyncActor1 = Actor.FromProducer(() => new SyncActor("SyncActor1", ref obj));
        var pidSyncActor1 = Actor.Spawn(propsSyncActor1);

        var reply = await pidSyncActor1.RequestAsync<RespondWriteCurrentTourState>(new WriteCurrentTourState("req1", dict), TimeSpan.FromSeconds(1)); // Expect Answer

        Assert.AreEqual(reply.id, "req1");

    }

}
}
