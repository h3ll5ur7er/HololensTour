using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TourBackend
{


    public class FullObject
    {
        /*mediaid
        1 = texture
        2 = 3D mesh
        3 = video
       ...erweiterbar
       */
        public string objectid;
        public int mediaid;
        public int[] position;
        public int[] rotation;

        //Basic Konstruktor 
        public FullObject(string _objectid, int _mediaid, int[] _position, int[] _rotation, string _type)
        {
            objectid = _objectid;
            mediaid = _mediaid;
            position = _position;
            rotation = _rotation;
        }

    }


    //Dictionary with ID and Object with Mesh, position, rotation, type
    public Dictionary<int, FullObject> dict = new Dictionary<int, FullObject>();





}