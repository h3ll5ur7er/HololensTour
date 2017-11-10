using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proto;

namespace TourBackend
{
    /* 
     * This is kind of a pseude Protocoll, cause we have all the same messages from the control to the 
     * recognition manager as from the recognition manager to the CodeObjects. Therefore to know which
     * messages are allowed between the recognition manager and the code objects, you should go to the
     * ControlToRecognitionManagerProtocol.cs file and read more there...
    */

    public class UpdateCodeObjectActor {
        public string messageid;
        public string objectid;
        public int mediaid;
        public int[] position;
        public int[] rotation;

        public UpdateCodeObjectActor(string _messageid, string _objectid, int _mediaid, int[] _position, int[] _rotation) {
            messageid = _messageid;
            mediaid = _mediaid;
            position = _position;
            rotation = _rotation;
            objectid = _objectid;
        }
    }

    public class RequestCodeObject {
        public string messageid;

        public RequestCodeObject(string _messageid) {
            messageid = _messageid;
        }
    }

    public class RespondCodeObject
    {
        public string messageid;
        public CodeObject codeobject;

        public RespondCodeObject(string _messageid, CodeObject _codeobject)
        {
            messageid = _messageid;
            codeobject = _codeobject;
        }
    }

}
