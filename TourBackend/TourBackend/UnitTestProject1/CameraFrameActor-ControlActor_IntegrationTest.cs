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
            CameraFeedSyncObject camerafeedsyncobject = new CameraFeedSyncObject("new");
            SyncObject syncobject = new SyncObject("sync1", new Dictionary<string, CodeObject>());

            //This dict will have to be updated to be true to the frame
            var dict = new Dictionary<string, CodeObject>();
            var cd1 = new CodeObject("cd1", 1, new[] { 1f, 2f, 3f }, new[] { 4f, 5f, 6f });
            var cd2 = new CodeObject("cd2", 1, new[] { 4f, 7f, 8f }, new[] { 1f, 19f, 3f }); // Just build two "random" CodeObjects

            dict.Add("cd1", cd1);
            dict.Add("cd2", cd2);

            FrameWork fw = new FrameWork(syncobject, camerafeedsyncobject);
            fw.Initialize();

            // Creates a testframe from local bitmaps
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            path = Path.Combine(path, "Resources");
            path = Path.Combine(path, "TestVideo_000.bmp");
            Stream testfile = File.OpenRead(path);
            testframe = await Utils.CreateTestFrame(testfile);

            lock (camerafeedsyncobject.thisLock)
            {
                camerafeedsyncobject.bitmap = testframe;
                camerafeedsyncobject.timestamp = 110100010;
            }
            // The timestamp is also the message id

            camerafeedsyncobject.UpdateFrame();

            // See if the output has been updated within 1 second
            Stopwatch stop = new Stopwatch();
            stop.Start();
            while (stop.ElapsedMilliseconds < 1000 && syncobject.dict != dict) {
                Thread.Sleep(5); // Arbitrary sleep length
            }
            stop.Stop();

            // Fail test if syncobj hasn't been updated
            CollectionAssert.AreEqual(syncobject.dict, dict);
        }

        [TestMethod]
        public async Task Multiple_frames_need_to_travel_from_CameraFeedSyncObject_To_SyncActor_with_mock_Frames()
        {
            SoftwareBitmap testframe;
            CameraFeedSyncObject camerafeedsyncobject = new CameraFeedSyncObject("new");
            SyncObject syncobject = new SyncObject("sync1", new Dictionary<string, CodeObject>());

            //This dict will have to be updated to be true to the frame
            var dict = new Dictionary<string, CodeObject>();
            var cd1 = new CodeObject("cd1", 1, new[] { 1f, 2f, 3f }, new[] { 4f, 5f, 6f });
            var cd2 = new CodeObject("cd2", 1, new[] { 4f, 7f, 8f }, new[] { 1f, 19f, 3f }); // Just build two "random" CodeObjects

            dict.Add("cd1", cd1);
            dict.Add("cd2", cd2);

            FrameWork fw = new FrameWork(syncobject, camerafeedsyncobject);
            fw.Initialize();

            for (int i=0;i<5;i++) {

                // Creates a testframe from local bitmaps
                var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                path = Path.Combine(path, "Resources");
                path = Path.Combine(path, "TestVideo_00"+i+".bmp");
                Stream testfile = File.OpenRead(path);
                testframe = await Utils.CreateTestFrame(testfile);

                lock (camerafeedsyncobject.thisLock)
                {
                    camerafeedsyncobject.bitmap = testframe;
                    camerafeedsyncobject.timestamp = 110100010;
                }
                // The timestamp is also the message id

                camerafeedsyncobject.UpdateFrame();

                // See if the output has been updated within 1 second
                Stopwatch stop = new Stopwatch();
                stop.Start();
                while (stop.ElapsedMilliseconds < 1000 && syncobject.dict != dict)
                {
                    Thread.Sleep(5); // Arbitrary sleep length
                }
                stop.Stop();

                // Fail test if syncobj hasn't been updated
                CollectionAssert.AreEqual(syncobject.dict, dict);
            }
        }

    }
}
