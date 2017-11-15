using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proto;

namespace TourBackend
{
    public class RecognitionManager : IActor
    {

        protected string Id { get; }
        public Dictionary<int,CodeObject> CodeObjectIDToCodeObject = new Dictionary<int, CodeObject>();
        public RecognitionManager(string id, Dictionary<int, CodeObject> _dict)
        {
            Id = id;
            CodeObjectIDToCodeObject = _dict;
        }

        public Task ReceiveAsync(IContext context)
        {
            var msg = context.Message;
            switch (msg)
            {
                
            }
            return Actor.Done;
        }

    }
}
