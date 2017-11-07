using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Threading;
using Proto;

namespace TourBackend
{
    class SyncActor_ControlActor_IntegrationTest
    {
        [TestMethod]
        public async Task ControlActor_must_correctly_initialize_SyncActor_which_works()
        {
            var syncobj = new SyncObject("sync1", new Dictionary<string, CodeObject>());
            var vidobj = new Object();
            var debugPID = new PID();
            // There is a constructor for debugging which allows to view the PID of the chosen Actor
            // the int argument of the constructor is interpreted as the actor which is to be linked to debugPID
            // Key:
            // 1: RecognitionManager
            // 2: SyncActor

            var propsctrl = Actor.FromProducer(() => new ControlActor("ctrl", syncobj, vidobj, ref debugPID, 2));
            var pidctrl = Actor.Spawn(propsctrl);

            var dict = new Dictionary<string, CodeObject>();
            var cd1 = new CodeObject("cd1", 1, new[] { 1, 2, 3 }, new[] { 4, 5, 6 });
            var cd2 = new CodeObject("cd2", 1, new[] { 4, 7, 8 }, new[] { 1, 19, 3 }); // Just build two "random" CodeObjects

            dict.Add("cd1", cd1);
            dict.Add("cd2", cd2);

            Assert.AreEqual(dict["cd1"], cd1); // Check that the dict contains the right CodeObjects
            Assert.AreEqual(dict["cd2"], cd2);

            var reply = await debugPID.RequestAsync<RespondWriteCurrentTourState>(new WriteCurrentTourState("dab", dict), TimeSpan.FromSeconds(1));

            debugPID.Stop();
            pidctrl.Stop();

            CollectionAssert.AreEqual((System.Collections.ICollection)syncobj.dict, (System.Collections.ICollection)dict);
        }
    }
}
