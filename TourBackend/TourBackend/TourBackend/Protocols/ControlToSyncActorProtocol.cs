using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proto;
using System.Diagnostics;

namespace TourBackend
{

    public class SyncObject
    {
        public Int64 timestamp;

        public object thisLock = new Object();

        public string objectid;
        public Dictionary<string, CodeObject> dict;

        //Basic Konstruktor 
        public SyncObject(string _objectid, Dictionary<string, CodeObject> _dict)
        {
            objectid = _objectid;
            dict = _dict;
        }

        public void SetTimeStamp(Int64 _timestamp) {
            this.timestamp = _timestamp;
        }

    }

    // Encapsulates the current state of an Object 
    // This definition is subject to fluent change

    public class CodeObject {

        public bool isActive = true;
        public string objectid;
        public int mediaid;
        public float[] position;
        public float[] rotation;

        public CodeObject(CodeObject codeobj) {

            position = new float[3];
            rotation = new float[9];

            objectid = codeobj.objectid;
            mediaid = codeobj.mediaid;
            codeobj.position.CopyTo(position,0);
            codeobj.rotation.CopyTo(rotation, 0);
            isActive = codeobj.isActive;
        }

        public CodeObject(string _objectid, int _mediaid, float[] _position, float[] _rotation, bool _isActive) {
            objectid = _objectid;
            mediaid = _mediaid;
            position = _position;
            rotation = _rotation;
            isActive = _isActive;
        }

        public CodeObject(string _objectid, int _mediaid, float[] _position, float[] _rotation)
        {
            objectid = _objectid;
            mediaid = _mediaid;
            position = _position;
            rotation = _rotation;
        }
    }

    // Request to update the SyncObject with the current TourState

    public class WriteCurrentTourState
    {

        public string id;
        public Dictionary<string, CodeObject> dict;

        public WriteCurrentTourState(string _id, Dictionary<string, CodeObject> _dict)
        {
            id = _id;
            dict = _dict;
        }

    }

    // Respond that the updating of the SyncObject has been successful

    public class RespondWriteCurrentTourState {

        public string id;

        public RespondWriteCurrentTourState(string _id) {
            id = _id;
        }

    }

}
