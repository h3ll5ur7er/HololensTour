using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Proto;
using System.Threading;
using System.IO;
using System.Reflection;

namespace TourBackend
{
    [TestClass]
    public class CameraFeedActor_UnitTest
    {

        [TestMethod]
        public void Constructor_CameraFeedSyncObject_Test()
        {
            var test = new CameraFeedSyncObject("new");
            Assert.AreEqual(test.id, "new");

        }
        [TestMethod]
        public void CameraFeedSyncObject_must_raise_event_when_UpdateFrame_is_called()
        {
            CameraFeedSyncObject test = new CameraFeedSyncObject("new");

            bool eventreceived = false;

            test.FrameUpdated += delegate (object sender, EventArgs e)
                {
                    eventreceived = true;
                };

            test.UpdateFrame();

            Assert.AreEqual(eventreceived, true);
        }

        [TestMethod]
        public async Task CameraFeedSyncObject_must_be_writable_when_using_local_frames()
        {
            SoftwareBitmap testframe;
            CameraFeedSyncObject test = new CameraFeedSyncObject("new");

            // Creates a testframe with the right Type
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            path = Path.Combine(path, "Resources");
            path = Path.Combine(path, "TestVideo_000.bmp");
            Stream testfile = File.OpenRead(path);
            testframe = await Utils.CreateTestFrame(testfile);

            Assert.AreEqual(null, test.bitmap);

            test.bitmap = testframe;

            Assert.AreNotEqual(null, test.bitmap);
            Assert.AreEqual(testframe.PixelHeight, test.bitmap.PixelHeight);
            Assert.AreEqual(testframe.PixelWidth, test.bitmap.PixelWidth);
        }

        [TestMethod]
        public async Task NewFrameArrived_must_be_correctly_constructed() {

            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            path = Path.Combine(path, "Resources");
            path = Path.Combine(path, "TestVideo_007.bmp");

            Stream testfile = File.OpenRead(path);

            var testframe = await Utils.CreateTestFrame(testfile);

            var newframe = new NewFrameArrived("id1", testframe);

            Assert.AreEqual("id1", newframe.id);

        }


        // Test if CameraFeedSyncObject fires an event FramUpdated and CameraFeedActor listens to it and sends NewFrameArrived to ctrlpid

        [TestMethod]
        public async Task CameraFeedActor_needs_to_get_update_from_CameraFeedSyncObject_when_using_local_frames()
        {
            SoftwareBitmap testframe;
            CameraFeedSyncObject test = new CameraFeedSyncObject("new");
            object msg = new Object();

            var propstest = Actor.FromProducer(() => new TestActor(ref msg));
            var pidtest = Actor.Spawn(propstest);

            var syncobj = new SyncObject("sync1", new Dictionary<int, CodeObject>());
            var syncobj2 = new CameraFeedSyncObject("sync2");

            var propsctrl = Actor.FromProducer(() => new ControlActor("ctrl", syncobj, null, new Dictionary<int, CodeObject>()));
            var pidctrl = Actor.Spawn(propsctrl);

            // Statt der PID des ControlActor wird die des TestActors gegeben um die gesendete Nachricht abzufangen
            var propsSyncActor1 = Actor.FromProducer(() => new CameraFeedActor("CameraFeedActor", syncobj2, pidtest));
            var pidSyncActor1 = Actor.Spawn(propsSyncActor1);

            // Creates a testframe from local bitmaps
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            path = Path.Combine(path, "Resources");
            path = Path.Combine(path, "TestVideo_007.bmp");
            Stream testfile = File.OpenRead(path);
            testframe = await Utils.CreateTestFrame(testfile);

            lock (test.thisLock)
            {
                test.bitmap = testframe;
                test.timestamp = 110100010;
            }
            // The timestamp is also the message id

            test.UpdateFrame();

            if (msg.GetType() == typeof(NewFrameArrived))
            {
                Assert.AreEqual(((NewFrameArrived)msg).id, "110100010");
            }
        }

    }
}
