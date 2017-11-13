using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TourBackend
{

    public class SyncObject
    {
        public Int64 timestamp;

        public object thisLock = new System.Object();

        public string objectid;
        public Dictionary<string, CodeObject> dict;

        //Basic Konstruktor 
        public SyncObject(string _objectid, Dictionary<string, CodeObject> _dict)
        {
            objectid = _objectid;
            dict = _dict;
        }

        public void SetTimeStamp(Int64 _timestamp)
        {
            this.timestamp = _timestamp;
        }

    }

    public class CodeObject
    {

        public string objectid;
        public int mediaid;
        public float[] position;
        public float[] rotation;

        public CodeObject(string _objectid, int _mediaid, float[] _position, float[] _rotation)
        {
            objectid = _objectid;
            mediaid = _mediaid;
            position = _position;
            rotation = _rotation;
        }
    }
}