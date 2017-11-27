using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proto;

namespace TourBackend
{
    public class BluetoothInterfaceActor : IActor
    {

        protected string Id { get; }

        public BluetoothInterfaceActor(string id)
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
