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

        public string id { get; }
        public bool isActive = true;

        public string objectid;
        public int mediaid;
        public int[] position;
        public int[] rotation;

        public CodeObjectActor(string _id, bool _isActive)
        {
            id = _id;
            isActive = _isActive;
        }

        public CodeObjectActor(string _id)
        {
            id = _id;
        }

        public Task ReceiveAsync(IContext context)
        {
            var msg = context.Message;
            switch (msg)
            {
                case UpdateCodeObjectActor u:
                    mediaid = u.mediaid;
                    position = u.position;
                    rotation = u.rotation;
                    break;
                case RequestCodeObject r:
                    var _msg = new RespondCodeObject(r.messageid, new CodeObject(objectid, mediaid, position, rotation, isActive));
                    context.Sender.Tell(_msg);
                    break;
                default:
                    break;
            }
            return Actor.Done;
        }

    }
}
