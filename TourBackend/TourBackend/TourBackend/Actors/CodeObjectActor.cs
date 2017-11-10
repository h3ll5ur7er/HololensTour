using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proto;

namespace TourBackend
{
    public class CodeObjectActor : IActor
    {

        public bool isActive = true;

        public string objectid;
        public int mediaid;
        public float[] position;
        public float[] rotation;

        public CodeObjectActor(string _objectid, bool _isActive)
        {
            objectid = _objectid;
            isActive = _isActive;
        }

        public CodeObjectActor(string _objectid)
        {
            objectid = _objectid;
        }

        public Task ReceiveAsync(IContext context)
        {
            var msg = context.Message;
            switch (msg)
            {
                case UpdateCodeObjectActor u:
                    if (u.objectid == objectid)
                    {
                        mediaid = u.mediaid;
                        position = u.position;
                        rotation = u.rotation;
                    }
                    break;
                case RequestCodeObject r:
                    var _msg = new RespondCodeObject(r.messageid, new CodeObject(objectid, mediaid, position, rotation, isActive));
                    context.Sender.Tell(_msg);
                    break;
                case SetActiveVirtualObject s:
                    if (s.toBeActiveVirtualObjectID == objectid) {
                        isActive = true;
                        context.Sender.Tell(new RespondSetActiveVirtualObject(s.messageID, objectid));
                    }
                    break;
                case SetInActiveVirtualObject s:
                    if (s.toBeInActiveVirtualObjectID == objectid)
                    {
                        isActive = false;
                        context.Sender.Tell(new RespondSetInActiveVirtualObject(s.messageID, objectid));
                    }
                    break;
                default:
                    break;
            }
            return Actor.Done;
        }

    }
}
