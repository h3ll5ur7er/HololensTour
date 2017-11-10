using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Proto;
using System.Threading.Tasks;
// the next ones are needed for the test with the frames
using Windows.Graphics.Imaging;
using System.Reflection;
using System.IO;



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
            // create a reference for the PID with the help of a empty PID. The idea here is to store the pid
            // of the created recognition manager in the controlActorconstructor to the _pidTestRecognitionManager
            // this is implicit done in the debug constructor of the controlActor with the additional argument ref _PIDdebug
            var _pidTestRecognitionManager = new PID();
            // here we create the testControlActor, the 1 is that the ref _PIDdebug is the RecognitionManager
            var _propsTestControlActor = Actor.FromProducer(() => new ControlActor("ControlActor", _testSyncObject, null, ref _pidTestRecognitionManager, 1));
            var _pidTestControlActor = Actor.Spawn(_propsTestControlActor);
            // here we specify the attributes of the CodeObjects => look at the constructor of the codeObjects
            // we need them defined to be able to create two new CodeObjects. We need to create them in order to be able to return a non-empty dictionary to 
            // the testControlActor. Without these create Statements we could not test this unit properly
            // cause we need to be sure that the objects which have been created are the ones in the dictionary
            // and not something else or more or less..
            // CodeObject 1 is Active
            float[] position1 = { 1, 2, 3 };
            float[] rotation1 = { 2, 2, 4 };
            var _codeObject1 = new CodeObject("1", 0, position1, rotation1, true);
            // CodeObject 2 is inActive
            float[] position2 = { 0, 2, 4 };
            float[] rotation2 = { 2, 3, 5 };
            var _codeObject2 = new CodeObject("2", 1, position2, rotation2, false);
            // CodeObject 3 is inActive
            float[] position3 = { 2, 5, 10 };
            float[] rotation3 = { 11, 1, 14 };
            var _codeObject3 = new CodeObject("3", 1, position3, rotation3, true);
            // here we say to the TestRecognitionManager to create first one CodeObject with the codeObjectID = 1
            // and we give the CodeObject itself also.
            var msg1 = new CreateNewVirtualObject("1", _codeObject1);
            _pidTestRecognitionManager.Tell(msg1);
            // and here the second. 
            var msg2 = new CreateNewVirtualObject("2", _codeObject2);
             _pidTestRecognitionManager.Tell(msg2);
            // and here the third. 
           var msg3 = new CreateNewVirtualObject("3", _codeObject3);
            _pidTestRecognitionManager.Tell(msg3);
            // here we really do now the request from the testControlActor to the recognitionManager and we store
            // the respond to the request in response where this must be a object of the class RespondRequestAllVirtualObjects
            // which contains of a dictionary and a messageID to know to which Request the Respond was
            var msg4 = new RequestAllVirtualObjects("Request1", TimeSpan.FromSeconds(1));
            var response = await _pidTestRecognitionManager.RequestAsync<RespondRequestAllVirtualObjects>(msg4, TimeSpan.FromSeconds(1));
            // here we actually test if the Call "RequestAllVirtualObjects" can what we intended
            // first we check if the response have the same messageID as the request had
            Assert.AreEqual(response.messageID, "Request1");
            // then we check if the dictionaries are the same. First define the expected Dictionary
            Dictionary<string, CodeObject> expectedDictionary = new Dictionary<string, CodeObject>();
            expectedDictionary.Add(_codeObject1.objectid, _codeObject1);
            //here we do not expect the _codeObject2 since his isActive == false
            expectedDictionary.Add(_codeObject3.objectid, _codeObject3);
            CollectionAssert.AreEqual(response.codeObjectIDToCodeObject, expectedDictionary);
        }
        /// <summary>
        /// The idea here is that we send a message to the Recognition Manager to SetActive a specific 
        /// VirtualObject. The Recognition Manager should response with the messageID of the SetActive Command
        /// and the VirtualObjectID of the now active VirtualObject
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task Control_Asks_RecognitionManager_To_SetActiveVirtualObject()
        {
            // these objects we need to create the ControlActor and the RecognitionManager and they do not
            // have any further functionality
            SyncObject _testSyncObject = new SyncObject("", null);
            // create a reference for the PID with the help of a empty PID. The idea here is to store the pid
            // of the created recognition manager in the controlActorconstructor to the _pidTestRecognitionManager
            // this is implicit done in the debug constructor of the controlActor with the additional argument ref _PIDdebug
            var _pidTestRecognitionManager = new PID();
            // here we create the testControlActor, the 1 is that the ref _PIDdebug is the RecognitionManager
            var _propsTestControlActor = Actor.FromProducer(() => new ControlActor("ControlActor", _testSyncObject, null, ref _pidTestRecognitionManager, 1));
            var _pidTestControlActor = Actor.Spawn(_propsTestControlActor);
            // here we specify the attribute of the CodeObject => look at the constructor of the codeObject
            // we need this to be defined in order to be able to say which VirtualObject we want to setActive
            // CodeObject 1
            float[] position1 = { 1, 2, 3 };
            float[] rotation1 = { 2, 2, 4 };
            // here we use the non default constructor such that the isActive boolean is initialised false
            var _codeObject1 = new CodeObject("1", 0, position1, rotation1, false);
            // define the message
            var msg = new SetActiveVirtualObject("SetActive1", "1");
            // Now send the message to the RecognitionManager to SetActive the _codeObject 1 and store the response
            var response = await _pidTestRecognitionManager.RequestAsync<RespondSetActiveVirtualObject>(msg,TimeSpan.FromSeconds(1));
            // now test if the response does contain the information that we want...
            Assert.AreEqual(response.messageID, "SetActive1");
            Assert.AreEqual(response.nowActiveVirtualObjectID, "1");
            // and test that the CodeObject changed his internal state from isActive = false to true
            Assert.AreEqual(_codeObject1, true);
        }

        /// <summary>
        /// The idea here is that we send a message to the Recognition Manager to SetInActive a specific 
        /// VirtualObject. The Recognition Manager should response with the messageID of the SetInActive Command
        /// and the VirtualObjectID of the now inactive VirtualObject
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task Control_Asks_RecognitionManager_To_SetInActiveVirtualObject()
        {
            // these objects we need to create the ControlActor and the RecognitionManager and they do not
            // have any further functionality
            SyncObject _testSyncObject = new SyncObject("", null);
            // create a reference for the PID with the help of a empty PID. The idea here is to store the pid
            // of the created recognition manager in the controlActorconstructor to the _pidTestRecognitionManager
            // this is implicit done in the debug constructor of the controlActor with the additional argument ref _PIDdebug
            var _pidTestRecognitionManager = new PID();
            // here we create the testControlActor, the 1 is that the ref _PIDdebug is the RecognitionManager
            var _propsTestControlActor = Actor.FromProducer(() => new ControlActor("ControlActor", _testSyncObject, null, ref _pidTestRecognitionManager, 1));
            var _pidTestControlActor = Actor.Spawn(_propsTestControlActor);
            // here we specify the attribute of the CodeObject => look at the constructor of the codeObject
            // we need this to be defined in order to be able to say which VirtualObject we want to setInActive
            // CodeObject 1
            float[] position1 = { 1, 2, 3 };
            float[] rotation1 = { 2, 2, 4 };
            // here we use the non default constructor such that the isActive boolean is initialised true
            // we could also use the other constructor but here we see it explicitely
            var _codeObject1 = new CodeObject("1", 0, position1, rotation1, true);
            // define the message
            var msg = new SetActiveVirtualObject("SetInActive1", "1");
            // Now send the message to the RecognitionManager to SetInActive the _codeObject 1 and store the response
            var response = await _pidTestRecognitionManager.RequestAsync<RespondSetActiveVirtualObject>(msg, TimeSpan.FromSeconds(1));
            // now test if the response does contain the information that we want...
            Assert.AreEqual(response.messageID, "SetInActive1");
            Assert.AreEqual(response.nowActiveVirtualObjectID, "1");
            // and test that the CodeObject changed his internal state isActive to false
            Assert.AreEqual(_codeObject1, false);
        }
        /// <summary>
        /// here the idea is that if the controlActor gets a message from the cameraFeedActor that a new 
        /// frame arrived, he should forward this message to the recognitionManager. The communication between
        /// the controlActor and the recognitionManager is here ONLY tested and not the whole flow from the camerafeedActor.
        /// Therefore if the message NewFrameArrived comes to the Recognition Manager than he should start to work
        /// with this Frame and if he is finished he should return the message that the 
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task Control_forwards_message_NewFrameArrived_to_the_recognitionManager()
        {
            // these objects we need to create the ControlActor and the RecognitionManager and they do not
            // have any further functionality
            SyncObject _testSyncObject = new SyncObject("", null);
            // create a reference for the PID with the help of a empty PID. The idea here is to store the pid
            // of the created recognition manager in the controlActorconstructor to the _pidTestRecognitionManager
            // this is implicit done in the debug constructor of the controlActor with the additional argument ref _PIDdebug
            var _pidTestRecognitionManager = new PID();
            // here we create the testControlActor, the 1 is that the ref _PIDdebug is the RecognitionManager
            var _propsTestControlActor = Actor.FromProducer(() => new ControlActor("ControlActor", _testSyncObject, null, ref _pidTestRecognitionManager, 1));
            var _pidTestControlActor = Actor.Spawn(_propsTestControlActor);
            // create a new object of the message type NewFrameArrived. for this we need firstly a new messageID
            string _messageID = "NewFrameArrived1";
            // and secondly a SoftwareBitmap and to get a testbitmap we need to follow these steps...they are
            // copied from the CameraFeedActor_UnitTest.cs and where there defined
            Windows.Graphics.Imaging.SoftwareBitmap _testbitmap;
            // Creates a testframe with the right Type
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            path = Path.Combine(path, "Resources");
            path = Path.Combine(path, "TestVideo_000.bmp");
            Stream testfile = File.OpenRead(path);
            _testbitmap = await Utils.CreateTestFrame(testfile);
            // now we are able to create the message
            var msg = new NewFrameArrived(_messageID, _testbitmap);
            // now send this message to the recognitionManager and get the answer in the response variable
            var response = await _pidTestRecognitionManager.RequestAsync<RespondNewFrameArrived>(msg, TimeSpan.FromSeconds(1));
            // now check if we get the right answer meaning the right message id
            Assert.AreEqual(response.messageID, _messageID);
        }
    }
}
