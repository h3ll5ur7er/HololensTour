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
        public Dictionary<string,CodeObject> CodeObjectIDToCodeObject = new Dictionary<string, CodeObject>();
        public Object video;
        public RecognitionManager(string id, object _video)
        {
            Id = id;
            video = _video;
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
