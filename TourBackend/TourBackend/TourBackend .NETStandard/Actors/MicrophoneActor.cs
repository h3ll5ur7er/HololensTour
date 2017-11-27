using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proto;

namespace TourBackend
{
    public class MicrophoneActor : IActor
    {

        protected string Id { get; }

        public MicrophoneActor(string id)
        {
            Id = id;
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
