﻿using System;
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
        public Dictionary<PID,string> pidToID = new Dictionary<PID, string>();
        public Dictionary<string, PID> idToPID = new Dictionary<string, PID>();

        public RecognitionManager(string id)
        {
            Id = id;
        }

        public Task ReceiveAsync(IContext context)
        {
            var msg = context.Message;
            switch (msg)
            {
                case RequestAllVirtualObjects r :
                    r.senderPID.Tell(new RespondRequestAllVirtualObjects(r.senderPID,))
                    break;

            }
            return Actor.Done;
        }

    }
}
