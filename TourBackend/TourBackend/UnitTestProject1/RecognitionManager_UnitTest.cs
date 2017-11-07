using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Proto;
using System.Threading.Tasks;

namespace TourBackend
{
    [TestClass]
    public class RecognititonManager_UnitTest
    {
        /// <summary>
        /// The idea here is to test that if the conrtrolActor asks the recognition manager
        /// to get all virtual objects that are shown in the screen of the hololens
        /// that the recognition manager returns a dictionary to the control Actor with all the 
        /// virtual objects listed in there with their PID and their creating message ID
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task ControlAsksRecognitionManagerRequestAllVirtualObjects()
        // for the RequestAsync method call we need firstly an async keyword in the declaration
        {
            // initialize the Control Actor

            var _propsControlActor = Actor.FromProducer(() => new ControlActor("ControlActor", null, null));
            var _pidControlActor = Actor.Spawn(_propsControlActor);
            // initialize the recognition manager
            var _propsRecognitionManager = Actor.FromProducer(() => new RecognitionManager("RecognitionManager"));
            var _pidRecognitionManager = Actor.Spawn(_propsRecognitionManager);
            // the control Actor should tell to the recognition manager to create a virtual object
            // with the name "TestMarker". First initialize the message for usability
            var _msg = new RequestAllVirtualObjects("Create 1", _pidControlActor, "TestMarker",TimeSpan.FromSeconds(1));
            _pidRecognitionManager.Tell(_msg);
            // reply is here to get the dictionary out of the actor...just for testing
            // the syntax is the following:
            // var replyObject = pidMessageReceiver.RequestAsync<ObjectType which I wanne get in the response>(message);
            // RequestAsync is here to block the sender until he get the response. Other syntax would be to only
            // have Request but then we have the problem that the sender is not blocked till he gets a response
            // the await keyword we need for the RequestAsync to be really asynchronous
            var reply = await _pidRecognitionManager.RequestAsync<Dictionary<string,PID>>(_msg, TimeSpan.FromSeconds(1));
            // here we actually test if the RecognitionManager made a new actor and put this new
            // actor into the dictionary with a keyValuePair "stringID : pidFromTestMarker"
            Assert.AreEqual(reply.ContainsKey("TestMarker"),true);            
        }
        

        [TestMethod]
        public void ControlAsksRecognitionManagerToCreateVirtualObject()
        {
            // Do some testing here
            // erstelle actors hier
            // message von control zu reco

            // initialize the Control Actor
            var propsControlActor = Actor.FromProducer(() => new ControlActor("ControlActor", null, null));
            var pidControlActor = Actor.Spawn(propsControlActor);
            // initialize the recognition manager
            var propsRecognitionManager = Actor.FromProducer(() => new RecognitionManager("RecognitionManager"));
            var pidRecognitionManager = Actor.Spawn(propsRecognitionManager);
            // the control Actor should tell to the recognition manager to create a virtual object
            // with the name "TestMarker" first initialize the message
            var msg = new CreateNewVirtualObject("Create 1", pidControlActor, "TestMarker");
            pidRecognitionManager.Tell(msg);
            // to finish but this is a complex test
        }
    }
}
