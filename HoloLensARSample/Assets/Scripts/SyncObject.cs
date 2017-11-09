using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TourBackend
{

    public class SyncObject
    {

        private Object thisLock = new Object();

        public string objectid;
        public Dictionary<string, CodeObject> dict;
        public System.Int64 timestamp;

        //Basic Konstruktor 
        public SyncObject(string _objectid, Dictionary<string, CodeObject> _dict, System.Int64 _timestamp)
        {
            objectid = _objectid;
            dict = _dict;
            timestamp = _timestamp;
        }

    }

    public class CodeObject
    {

        public string objectid;
        public int mediaid;
        public int[] position;
        public int[] rotation;

        public CodeObject(string _objectid, int _mediaid, int[] _position, int[] _rotation)
        {
            objectid = _objectid;
            mediaid = _mediaid;
            position = _position;
            rotation = _rotation;
        }
    }
}