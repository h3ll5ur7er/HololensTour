using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proto;

namespace TourBackend
{
    public class SyncActor : IActor
    {

        protected string id { get; }
        public SyncObject syncobject { get; } // Object which syncs Unity and the Actor framework

        public SyncActor(string _id, SyncObject _syncobject)
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
                    break;
            }
            return Actor.Done;
        }

    }
}
