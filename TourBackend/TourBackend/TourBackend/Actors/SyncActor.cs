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
        public object syncobject { get; }

        public SyncActor(string _id, object _syncobject)
        {
            id = _id;
            syncobject = _syncobject;
        }

        public Task ReceiveAsync(IContext context)
        {
            var msg = context.Message;
            switch (msg)
            {
                case msg is WriteCurrentTourState w:



                    break;
            }
            return Actor.Done;
        }

    }
}
