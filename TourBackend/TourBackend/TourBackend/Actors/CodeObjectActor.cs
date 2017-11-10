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

            }
            return Actor.Done;
        }

    }
}
