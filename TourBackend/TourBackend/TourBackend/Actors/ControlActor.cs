using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proto;
using System.Threading;

namespace TourBackend
{

    // Attention: This Actor is subject to change!
    // Neither the constructors nor any other function is guaranteed to stay unchanged!

    public class ControlActor : IActor
    {
        public string id { get; }
        public SyncObject sync;
        public CameraFeedSyncObject video; // This field will *certainly* change
        public PID recognitionManager;
        public PID syncActor;
        // More managed PIDs are to follow

        public ControlActor(string _id, SyncObject _sync, CameraFeedSyncObject _video)
        {
            id = _id;
            sync = _sync;
            video = _video;

            var syncprops = Actor.FromProducer(() => new SyncActor("syncactor", _sync));
            this.syncActor = Actor.Spawn(syncprops);
        }

        // This constructor is only meant for debugging 
        // It is functionally *identical* to the usual constructor except that it 
        // links the reference _debugPID to either of the newly created Actors, chosen according to the int argument,
        // as this allows for more convenient testing.
        // Key for the int argument:
        // 1: RecognitionManager
        // 2: SyncActor

        public ControlActor(string _id, SyncObject _sync, CameraFeedSyncObject _video, ref PID _debugPID, int debug)
        {
            id = _id;
            sync = _sync;
            video = _video;

            var syncprops = Actor.FromProducer(() => new SyncActor("syncactor", _sync));
            this.syncActor = Actor.Spawn(syncprops);

            switch (debug)
            {
                case 1:
                    _debugPID = recognitionManager;
                    break;
                case 2:
                    _debugPID = syncActor;
                    break;
                default:
                    break;
            }
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
