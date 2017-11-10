using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Proto;
using System.Threading;

namespace TourBackend
{

    [TestClass]
    public class ControlToSyncActorProtocol
    {
        [TestMethod]
        public void SyncObject_must_be_correctly_constructed()
        {

            var dict = new Dictionary<string, CodeObject>();
            var cd1 = new CodeObject("cd1", 1, new[] { 1f, 2f, 3f }, new[] { 4f, 5f, 6f });
            var cd2 = new CodeObject("cd2", 1, new[] { 4f, 7f, 8f }, new[] { 1f, 19f, 3f }); // Just build two "random" CodeObjects

            dict.Add("cd1", cd1);
            dict.Add("cd2", cd2);

            Assert.AreEqual(dict["cd1"], cd1); // Check that the dict contains the right CodeObjects
            Assert.AreEqual(dict["cd2"], cd2);

            var obj = new SyncObject("sync1", dict);

            Assert.AreEqual(obj.dict["cd1"], cd1); // Check that the SyncObject contains the right CodeObjects
            Assert.AreEqual(obj.dict["cd2"], cd2);
            Assert.AreEqual(obj.objectid, "sync1");
        }
    }


    /// <summary>
    /// This class tests all message constructors which are defined in the ControlToRecognitionManagerProtocol.
    /// </summary>
    [TestClass]
    public class ControlToRecognitionManagerProtocol
    {
        /* Test all constructors of the commands. First the ones for the nano-case. */
       
        [TestMethod]
        public void SetActiveVirtualObject_Constructor_must_create_object()
        {
            string _toBeActiveVirtualObjectID = "toBeActiveVirtualObjectID";
            var testobject3 = new SetActiveVirtualObject("messageID", _toBeActiveVirtualObjectID);
            // test the constructor
            Assert.AreEqual(testobject3.messageID, "messageID");
            Assert.AreEqual(testobject3.toBeActiveVirtualObjectID, _toBeActiveVirtualObjectID);
        }

        [TestMethod]
        public void SetInActiveVirtualObject_Constructor_must_create_object()
        {
            string _toBeInActiveVirtualObjectID = "toBeActiveVirtualObjectID";
            var testobject3 = new SetInActiveVirtualObject("messageID", _toBeInActiveVirtualObjectID);
            // test the constructor
            Assert.AreEqual(testobject3.messageID, "messageID");
            Assert.AreEqual(testobject3.toBeInActiveVirtualObjectID, _toBeInActiveVirtualObjectID);
        }

        [TestMethod]
        public void RequestAllVirtualObjects_Constructor_must_create_object()
        {
            // time is here a timespan of the length 0 hours, 0 minutes and 1 seconds
            TimeSpan _timeSpan = new TimeSpan(0, 0, 1);
            var testObject = new RequestAllVirtualObjects("messageID", _timeSpan);
            // test the constructor
            Assert.AreEqual(testObject.messageID, "messageID");
            Assert.AreEqual(testObject.timeSpan, _timeSpan);
        }

        /* Test all constructors of the commands. Second the ones for the not nano-case. */

        [TestMethod]
        public void StartVirtualObject_Constructor_must_create_object()
        {
            string _virtualObjectIDToBeStarted = "virtualObjectID";
            var testObject = new StartVirtualObject("messageID", _virtualObjectIDToBeStarted);
            // test the constructor
            Assert.AreEqual(testObject.messageID, "messageID");
            Assert.AreEqual(testObject.virtualObjectIDToBeStarted, _virtualObjectIDToBeStarted);
        }

        [TestMethod]
        public void StopVirtualObject_Constructor_must_create_object()
        {
            string _virtualObjectIDToBeStopped = "virtualObjectIDToBeStopped";
            var testObject = new StopVirtualObject("messageID", _virtualObjectIDToBeStopped);
            // test the constructor
            Assert.AreEqual(testObject.messageID, "messageID");
            Assert.AreEqual(testObject.virtualObjectIDToBeStopped, _virtualObjectIDToBeStopped);
        }

        [TestMethod]
        public void KillVirtualObject_Constructor_must_create_object()
        {
            string _toBeKilledVirtualObjectID = "toBeKilledVirtualObjectID";
            var testObject = new KillVirtualObject("messageID", _toBeKilledVirtualObjectID);
            // test the constructor
            Assert.AreEqual(testObject.messageID, "messageID");
            Assert.AreEqual(testObject.toBeKilledVirtualObjectID, _toBeKilledVirtualObjectID);
        }

        /* Test all constructors of the responds to the commands. First the ones for the nano-case. */

        [TestMethod]
        public void RespondSetActiveVirtualObject_Constructor_must_create_object()
        {
            string _nowActiveVirtualObjectID = "nowActiveVirtualObjectID";
            var testobject2 = new RespondSetActiveVirtualObject("_id", _nowActiveVirtualObjectID);
            // test the constructor
            Assert.AreEqual(testobject2.messageID, "_id");
            Assert.AreEqual(testobject2.nowActiveVirtualObjectID, _nowActiveVirtualObjectID);
        }

        [TestMethod]
        public void RespondSetInActiveVirtualObject_Constructor_must_create_object()
        {
            string _nowInActiveVirtualObjectID = "nowInActiveVirtualObjectID";
            var testobject2 = new RespondSetInActiveVirtualObject("_id", _nowInActiveVirtualObjectID);
            // test the constructor
            Assert.AreEqual(testobject2.messageID, "_id");
            Assert.AreEqual(testobject2.nowInActiveVirtualObjectID, _nowInActiveVirtualObjectID);
        }

        [TestMethod]
        public void RespondRequestAllVirtualObjects_Constructor_must_create_object()
        {
            // first create a new pseude Dictionary...
            Dictionary<string, CodeObject> _dict = new Dictionary<string, CodeObject>();
            // insert a key value pair with a codeObjectID and a codeObject which is null...
            _dict.Add("codeObjectNull", null);
            var testobject = new RespondRequestAllVirtualObjects("_id", _dict);
            // test the constructor with the two arguments
            Assert.AreEqual(testobject.messageID, "_id");
            // with CollectionAssert you can test whole Dictionaries...
            CollectionAssert.AreEqual(testobject.codeObjectIDToCodeObject, _dict);
        }

        /* Test all constructors of the responds to the commands. Second the ones for the not nano-case. */

        [TestMethod]
        public void RespondStartVirtualObject_Constructor_must_create_object()
        {
            string _nowStartedVirtualObjectID = "nowStartedVirtualObjectID";
            var testObject = new RespondStartVirtualObject("messageID", _nowStartedVirtualObjectID);
            // test the constructor
            Assert.AreEqual(testObject.messageID, "messageID");
            Assert.AreEqual(testObject.nowStartedVirtualObjectID, _nowStartedVirtualObjectID);
        }

        [TestMethod]
        public void RespondStopVirtualObject_Constructor_must_create_object()
        {
            string _nowStoppedVirtualObjectID = "nowStoppedVirtualObjectID";
            var testObject = new RespondStopVirtualObject("messageID", _nowStoppedVirtualObjectID);
            // test the constructor
            Assert.AreEqual(testObject.messageID, "messageID");
            Assert.AreEqual(testObject.nowStoppedVirtualObjectID, _nowStoppedVirtualObjectID);
        }

        [TestMethod]
        public void RespondKillVirtualObject_Constructor_must_create_object()
        {
            string _nowKilledVirtualObjectID = "nowStoppedVirtualObjectID";
            var testobject = new RespondKillVirtualObject("messageID", _nowKilledVirtualObjectID);
            // test the constructor
            Assert.AreEqual(testobject.messageID, "messageID");
            Assert.AreEqual(testobject.nowKilledVirtualObjectID, _nowKilledVirtualObjectID);
        }

        /* Test all constructors of the failures to the commands. First the ones for the nano-case. */

        [TestMethod]
        public void FailedToSetActiveVirtualObject_Constructor_must_create_object()
        {
            string _messageID = "messageID";
            var testObject = new FailedToSetActiveVirtualObject(_messageID);
            Assert.AreEqual(testObject.messageID, _messageID);
        }

        [TestMethod]
        public void FailedToSetInActiveVirtualObject_Constructor_must_create_object()
        {
            string _messageID = "messageID";
            var testObject = new FailedToSetInActiveVirtualObject(_messageID);
            Assert.AreEqual(testObject.messageID, _messageID);
        }

        [TestMethod]
        public void FailedToRequestAllVirtualObjects_Constructor_must_create_object()
        {
            string _messageID = "messageID";
            var testObject = new FailedToRequestAllVirtualObjects(_messageID);
            Assert.AreEqual(testObject.messageID, _messageID);
        }

        /* Test all constructors of the failures to the commands. Second the ones for the not nano-case. */

        [TestMethod]
        public void FailedToStartVirtualObject_Constructor_must_create_object()
        {
            string _messageID = "messageID";
            var testObject = new FailedToStartVirtualObject(_messageID);
            Assert.AreEqual(testObject.messageID, _messageID);
        }

        [TestMethod]
        public void FailedToStopVirtualObject_Constructor_must_create_object()
        {
            string _messageID = "messageID";
            var testObject = new FailedToStopVirtualObject(_messageID);
            Assert.AreEqual(testObject.messageID, _messageID);
        }

        [TestMethod]
        public void FailedToKillVirtualObject_Constructor_must_create_object()
        {
            string _messageID = "messageID";
            var testObject = new FailedToKillVirtualObject(_messageID);
            Assert.AreEqual(testObject.messageID, _messageID); ;
        }

        /* test the constructor for the codeObjects. */

        [TestMethod]
        public void CodeObject_must_creat_object()
        {
            bool _isActive = false;
            string _objectid = "Object1";
            int _mediaid = 27;
            float[] _position = { 1f, 2f, 3f };
            float[] _rotation = { 2f, 5f, 7f };
            // create two testobjects with the two different constructors
            CodeObject testCodeObject1 = new CodeObject(_objectid, _mediaid, _position, _rotation);
            CodeObject testCodeObject2 = new CodeObject(_objectid, _mediaid, _position, _rotation,_isActive);
            // test the constructor with 4 arguments with default isActive = true;
            Assert.AreEqual(testCodeObject1.objectid, _objectid);
            Assert.AreEqual(testCodeObject1.mediaid, _mediaid);
            Assert.AreEqual(testCodeObject1.position, _position);
            Assert.AreEqual(testCodeObject1.rotation, _rotation);
            Assert.AreEqual(testCodeObject1.isActive, true);
            // test the constructor with 5 arguments
            Assert.AreEqual(testCodeObject2.objectid, _objectid);
            Assert.AreEqual(testCodeObject2.mediaid, _mediaid);
            Assert.AreEqual(testCodeObject2.position, _position);
            Assert.AreEqual(testCodeObject2.rotation, _rotation);
            Assert.AreEqual(testCodeObject2.isActive, _isActive);
        }
    }
}
