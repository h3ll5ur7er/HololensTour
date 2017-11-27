using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proto;
using Windows.Graphics.Imaging;

namespace TourBackend
{
    public class CameraFeedActor : IActor
    {

        public string id { get; }
        public CameraFeedSyncObject sync;
        public PID ctrlActor;
        public SoftwareBitmap latestBitmap;
        public Int64 latestTimestamp;

        public CameraFeedActor(string _id, CameraFeedSyncObject _sync, PID _ctrlActor)
        {
            ctrlActor = _ctrlActor;
            id = _id;
            sync = _sync;
            sync.FrameUpdated += OnFrameUpdated;
        }

        public Task ReceiveAsync(IContext context)
        {
            var msg = context.Message;
            switch (msg)
            {
            }
            return Actor.Done;
        }

        protected void OnFrameUpdated(object Sender, EventArgs e)
        {
            if (true) // Condition here is to be defined... might make sense to only process every second frame or so
            {
                lock (sync.thisLock)
                {
                    latestBitmap = sync.bitmap; // SoftwareBitmap.Copy(sync.bitmap); Need to think about safety of passing bitmap as reference
                    latestTimestamp = sync.timestamp;
                }
                ctrlActor.Tell(new NewFrameArrived(latestTimestamp.ToString(), latestBitmap));
            }
        }

    }
}
