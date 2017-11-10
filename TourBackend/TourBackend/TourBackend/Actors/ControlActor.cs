using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proto;
using System.Threading;

namespace TourBackend
{

    public class FrameWork {

        private PID pidctrl;
        private SyncObject syncobj;
        private CameraFeedSyncObject video;

        public FrameWork(SyncObject _syncobj, CameraFeedSyncObject _video) {
            syncobj = _syncobj;
            video = _video;
        }

        public void Initialize()
        {
            var propsctrl = Actor.FromProducer(() => new ControlActor("ctrl", syncobj, video));
            pidctrl = Actor.Spawn(propsctrl);

            pidctrl.RequestAsync<RespondStartFramework>(new StartFramework(pidctrl), TimeSpan.FromSeconds(1));
        }

    }

    public class StartFramework {
        public PID ctrl;
        public StartFramework(PID _ctrl) {
            ctrl = _ctrl;
        }
    }

    public class RespondStartFramework {
        public RespondStartFramework(){ }
    }

    // Attention: This Actor is subject to change!
    // Neither the constructors nor any other function is guaranteed to stay unchanged!

    public class ControlActor : IActor
    {
        public string id { get; }
        public SyncObject sync;
        public CameraFeedSyncObject video; // This field will *certainly* change
        public PID recognitionManager;
        public PID syncActor;
        public PID cameraFeedSyncActor;
        public PID self;
        // More managed PIDs are to follow

        public void Start() {
            var syncprops = Actor.FromProducer(() => new SyncActor("syncactor", sync));
            this.syncActor = Actor.Spawn(syncprops);

            var recogprops = Actor.FromProducer(() => new RecognitionManager("recognitionmanager", video));
            this.recognitionManager = Actor.Spawn(recogprops);

            var cameraFeedSyncprops = Actor.FromProducer(() => new CameraFeedActor("camerafeedactor", video, self));
            this.recognitionManager = Actor.Spawn(cameraFeedSyncprops);
        }

        public ControlActor(string _id, SyncObject _sync, CameraFeedSyncObject _video)
        {
            id = _id;
            sync = _sync;
            video = _video;
        }

        // This constructor is only meant for debugging 
        // It is functionally identically to the usual constructor except that it 
        // links the reference _debugPID to either of the newly created Actors, chosen according to the int argument,
        // as this allows for more convenient testing.
        // It also doesn't spawn the CameraFeedActor
        // Key for the int argument:
        // 1: RecognitionManager
        // 2: SyncActor

        public ControlActor(string _id, SyncObject _sync, CameraFeedSyncObject _video, ref PID _debugPID, int debug)
        {
            id = _id;
            sync = _sync;
            video = _video;

            var syncprops = Actor.FromProducer(() => new SyncActor("syncactor", sync));
            this.syncActor = Actor.Spawn(syncprops);

            var recogprops = Actor.FromProducer(() => new RecognitionManager("recognitionmanager", video));
            this.recognitionManager = Actor.Spawn(recogprops);

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
                case StartFramework s:
                    self = s.ctrl;
                    this.Start();
                    context.Sender.Tell(new RespondStartFramework());
                    break;
                case NewFrameArrived n:
                    recognitionManager.Tell(n);
                    break;
                case RespondNewFrameArrived r:
                    recognitionManager.Tell(new RequestAllVirtualObjects(r.id,self,TimeSpan.FromMilliseconds(25)));
                break;
                case RespondRequestAllVirtualObjects r:
                    syncActor.Request(new WriteCurrentTourState(r.messageID, r.codeObjectIDToCodeObjectPID), self);
                    break;
            }
            return Actor.Done;
        }

    }
}
