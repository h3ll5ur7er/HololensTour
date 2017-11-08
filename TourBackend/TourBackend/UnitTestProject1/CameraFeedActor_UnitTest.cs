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

            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            path = Path.Combine(path, "Resources");
            path = Path.Combine(path, "TestVideo_000.bmp");

            Stream testfile = File.OpenRead(path);

            testframe = SoftwareBitmap.Copy(await Utils.CreateTestFrame(testfile));

            Assert.AreEqual(null, test.bitmap);

            test.bitmap = SoftwareBitmap.Copy(testframe);

            Assert.AreNotEqual(null, test.bitmap);
            Assert.AreEqual(testframe.PixelHeight, test.bitmap.PixelHeight);
            Assert.AreEqual(testframe.PixelWidth, test.bitmap.PixelWidth);
        }

        [TestMethod]
        public async Task CameraFeedActor_needs_to_get_update_from_CameraFeedSyncObject_when_using_local_frames()
        {
            SoftwareBitmap testframe;
            CameraFeedSyncObject test = new CameraFeedSyncObject("new");

            PID pid = new PID();

            var syncobj = new SyncObject("sync1", new Dictionary<string, CodeObject>());
            var syncobj2 = new CameraFeedSyncObject("sync2");

            var propsctrl = Actor.FromProducer(() => new ControlActor("ctrl", syncobj, null));
            var pidctrl = Actor.Spawn(propsctrl);

            var propsSyncActor1 = Actor.FromProducer(() => new CameraFeedActor("CameraFeedActor", syncobj2, pid));
            var pidSyncActor1 = Actor.Spawn(propsSyncActor1);

            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            path = Path.Combine(path, "Resources");
            path = Path.Combine(path, "TestVideo_007.bmp");

            Stream testfile = File.OpenRead(path);

            testframe = SoftwareBitmap.Copy(await Utils.CreateTestFrame(testfile));
            test.bitmap = SoftwareBitmap.Copy(testframe);

            test.UpdateFrame();
        }

    }
}
