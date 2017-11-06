using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourBackend
{

    public class CodeObject {
        public int[] position;
        public int[] rotation;

        public CodeObject(int[] _position, int[] _rotation) {
            position = _position;
            rotation = _rotation;
        }
    }

    public class WriteCurrentTourState {

        public string id;
        public Dictionary<string, CodeObject> dict;

        public WriteCurrentTourState(string _id, Dictionary<string, CodeObject> _dict)
        {
            id = _id;
            dict = _dict;
        }
    }

    public class RespondWriteCurrentTourState {

        public string id;

        public RespondWriteCurrentTourState(string _id) {
            id = _id;
        }

    }

}
