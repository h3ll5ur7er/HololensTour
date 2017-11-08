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
        /// The idea here is to test that if the conrtrolActor asks the recognitionManager
        /// to get all CodeObjects, that are in the current tourState, the controlActor gets a dictionary
        /// back. The dictionary consists of an CodeObjectID as a key and the CodeObject itself as a value.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task Control_Asks_RecognitionManager_RequestAllVirtualObjects()
        // for the RequestAsync method call we need firstly an async keyword in the declaration of the Task
        {
            // these objects we need to create the ControlActor and the RecognitionManager and they do not
            // have any further functionality
            SyncObject _testSyncObject = new SyncObject("", null);
            // here we create the testControlActor
            var _propsTestControlActor = Actor.FromProducer(() => new ControlActor("ControlActor", _testSyncObject, null));
            var _pidTestControlActor = Actor.Spawn(_propsTestControlActor);
            // here we create the TestRecognitionManager
            var _propsTestRecognitionManager = Actor.FromProducer(() => new RecognitionManager("RecognitionManager", null));
            var _pidTestRecognitionManager = Actor.Spawn(_propsTestRecognitionManager);
            // here we specify the attributes of the CodeObjects => look at the constructor of the codeObjects
            // we need them defined to be able to create two new CodeObjects. We need to create them in order to be able to return a non-empty dictionary to 
            // the testControlActor. Without these create Statements we could not test this unit properly
            // cause we need to be sure that the objects which have been created are the ones in the dictionary
            // and not something else or more or less..
            // CodeObject 1
            int[] position1 = { 1, 2, 3 };
            int[] rotation1 = { 2, 2, 4 };
            var _codeObject1 = new CodeObject("1", 0, position1, rotation1);
            // CodeObject 2
            int[] position2 = { 0, 2, 4 };
            int[] rotation2 = { 2, 3, 5 };
            var _codeObject2 = new CodeObject("2", 1, position2, rotation2);
            // here we say to the TestRecognitionManager to create first one CodeObject with the codeObjectID = 1
            var msg1 = new CreateNewVirtualObject("Create1", _pidTestControlActor, "1");
            _pidTestRecognitionManager.Tell(msg1);
            // and here the second. 
            var msg2 = new CreateNewVirtualObject("Create2", _pidTestControlActor, "2");
            _pidTestRecognitionManager.Tell(msg2);
            // here we really do now the request from the testControlActor to the recognitionManager and we store
            // the respond to the request in response where this must be a object of the class RespondRequestAllVirtualObjects
            // which contains of a dictionary and a messageID to know to which Request the Respond was
            var msg3 = new RequestAllVirtualObjects("Request1", _pidTestControlActor, TimeSpan.FromSeconds(1));
            var response = await _pidTestRecognitionManager.RequestAsync<RespondRequestAllVirtualObjects>(msg3, TimeSpan.FromSeconds(1));
            // here we actually test if the Call "RequestAllVirtualObjects" can what we intended
            // first we check if the response have the same messageID as the request had
            Assert.AreEqual(response.messageID, "Request1");
            // then we check if the dictionary contains the key 1 and 2 since we inserted two CodeObjects of with this ID's
            Assert.AreEqual(response.CodeObjectIDToCodeObject.ContainsKey("1"), true);
            Assert.AreEqual(response.CodeObjectIDToCodeObject.ContainsKey("2"), true);
            // here we check if the response's dictionary contains exactly what wi inserted before
            CodeObject value1 = response.CodeObjectIDToCodeObject["1"];
            // with this statement we get the value to the key in the brackets. this is dictionary syntax
            CodeObject value2 = response.CodeObjectIDToCodeObject["2"];
            Assert.AreEqual(value1, _codeObject1);
            Assert.AreEqual(value2, _codeObject2);
        }

        [TestMethod]
        public void ControlAsksRecognitionManagerToCreateVirtualObject()
        {
            // Do some testing here
        }
    }
}
