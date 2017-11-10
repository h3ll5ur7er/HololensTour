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
using System.Diagnostics;

namespace TourBackend
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task A_single_frame_needs_to_travel_from_CameraFeedSyncObject_To_SyncActor_with_mock_Frames()
        {
            SoftwareBitmap testframe;
            CameraFeedSyncObject test = new CameraFeedSyncObject("new");
            object msg = new Object();
            var pidsyncactor = new PID();

            //This dict will have to be updated to be true to the frame
            var dict = new Dictionary<string, CodeObject>();
            var cd1 = new CodeObject("cd1", 1, new[] { 1f, 2f, 3f }, new[] { 4f, 5f, 6f });
            var cd2 = new CodeObject("cd2", 1, new[] { 4f, 7f, 8f }, new[] { 1f, 19f, 3f }); // Just build two "random" CodeObjects

            dict.Add("cd1", cd1);
            dict.Add("cd2", cd2);

            var propstest = Actor.FromProducer(() => new TestActor(ref msg));
            var pidtest = Actor.Spawn(propstest);

            var syncobj = new SyncObject("sync1", new Dictionary<string, CodeObject>());
            var syncobj2 = new CameraFeedSyncObject("sync2");

            var propsctrl = Actor.FromProducer(() => new ControlActor("ctrl", syncobj, syncobj2, ref pidsyncactor, 2));
            var pidctrl = Actor.Spawn(propsctrl);

            // Creates a testframe from local bitmaps
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            path = Path.Combine(path, "Resources");
            path = Path.Combine(path, "TestVideo_000.bmp");
            Stream testfile = File.OpenRead(path);
            testframe = await Utils.CreateTestFrame(testfile);

            lock (test.thisLock)
            {
                test.bitmap = testframe;
                test.timestamp = 110100010;
            }
            // The timestamp is also the message id

            test.UpdateFrame();

            // See if the output has been updated within 1 second
            Stopwatch stop = new Stopwatch();
            stop.Start();
            while (stop.ElapsedMilliseconds < 1000 && syncobj.dict != dict) {
                Thread.Sleep(5); // Arbitrary sleep length
            }
            stop.Stop();

            // Fail test if syncobj hasn't been updated
            CollectionAssert.AreEqual(syncobj.dict, dict);
        }
    }
}
