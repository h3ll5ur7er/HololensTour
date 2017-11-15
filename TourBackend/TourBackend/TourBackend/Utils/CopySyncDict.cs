using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourBackend
{
    public static class CopySyncDict
    {
        public static Dictionary<string, CodeObject> Copy(Dictionary<string, CodeObject> _dict) {
            Dictionary<string, CodeObject> copy = new Dictionary<string, CodeObject>();

            foreach(KeyValuePair<string, CodeObject> p in _dict){
                copy.Add(p.Key, new CodeObject(p.Value));
            }

            return copy;
        }
    }
}
