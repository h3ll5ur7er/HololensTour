using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proto;

namespace TourBackend
{
    public class ControlActor : IActor
    {
        protected string Id { get; }

        public ControlActor(string id)
        {
            Id = id;
            // create a new actor recognitionManager if an ControlActor gets created
            var propsRecognitionManager = Actor.FromProducer(
                        () => new RecognitionManager("RecognitionManager"));
            var pidRecognitionManager = Actor.Spawn(propsRecognitionManager);
            // create a new actor if an ControlActor gets created


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
