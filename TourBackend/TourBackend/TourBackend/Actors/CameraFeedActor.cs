using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proto;

namespace TourBackend
{
    public class CameraFeedActor : IActor
    {
        public string id { get; }
        public CameraFeedSyncObject sync;   

        public CameraFeedActor(string _id, CameraFeedSyncObject _sync)
        {
            id = _id;
            sync = _sync;
        }

        public Task ReceiveAsync(IContext context)
        {
            var msg = context.Message;
            switch (msg)
            {

            }
            return Actor.Done;
        }

    }
}
