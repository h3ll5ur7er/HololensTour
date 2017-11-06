using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proto;
using System.Reflection;

namespace TourBackend
{
    public class SyncActor : IActor
    {

        protected string id { get; }
        public SyncObject syncobject; // Object which syncs Unity and the Actor framework

        public SyncActor(string _id, ref SyncObject _syncobject)
        {
            id = _id;
            syncobject = _syncobject;
        }

        public Task ReceiveAsync(IContext context)
        {
            var msg = context.Message;
            switch (msg)
            {
                case WriteCurrentTourState w:
                    syncobject.dict = new Dictionary<string, CodeObject>(w.dict);
                    context.Sender.Tell(new RespondWriteCurrentTourState(w.id));
                    break;
                case SetSyncObject s:
                    syncobject = s.sync;
                    break;
            }
            return Actor.Done;
        }

    }
}
