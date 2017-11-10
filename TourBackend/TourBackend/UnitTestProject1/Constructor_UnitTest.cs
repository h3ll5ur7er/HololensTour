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
    /// This class tests all classes which are defined in the ControlToRecognitionManagerProtocol.
    /// </summary>
    [TestClass]
    public class ControlToRecognitionManagerProtocol
    {
        [TestMethod]
        public void StartVirtualObject_Constructor_must_create_object()
        {
            var _senderPID = new PID();
            string _targetActor = "_targetActor";
            var testobject2 = new StartVirtualObject("_id", _senderPID);
            var testobject3 = new StartVirtualObject("_id", _senderPID, _targetActor);
            // test the constructor with 2 arguments
            Assert.AreEqual(testobject2.messageID, "_id");
            Assert.AreEqual(testobject2.senderPID, _senderPID);
            // test the constructor with 4 arguments
            Assert.AreEqual(testobject3.messageID, "_id");
            Assert.AreEqual(testobject3.senderPID, _senderPID);
            Assert.AreEqual(testobject3.targetActor, _targetActor); ;
        }
        [TestMethod]
        public void StopVirtualObject_Constructor_must_create_object()
        {
            var _senderPID = new PID();
            string _targetActor = "_targetActor";
            var testobject2 = new StopVirtualObject("_id", _senderPID);
            var testobject3 = new StopVirtualObject("_id", _senderPID, _targetActor);
            // test the constructor with 2 arguments
            Assert.AreEqual(testobject2.messageID, "_id");
            Assert.AreEqual(testobject2.senderPID, _senderPID);
            // test the constructor with 3 arguments
            Assert.AreEqual(testobject3.messageID, "_id");
            Assert.AreEqual(testobject3.senderPID, _senderPID);
            Assert.AreEqual(testobject3.targetActor, _targetActor);
        }
        [TestMethod]
        public void SetActiveVirtualObject_Constructor_must_create_object()
        {
            string _toBeActiveVirtualObjectID = "toBeActiveVirtualObjectID";
            var testobject3 = new SetActiveVirtualObject("_id", _toBeActiveVirtualObjectID);
            // test the constructor
            Assert.AreEqual(testobject3.messageID, "_id");
            Assert.AreEqual(testobject3.toBeActiveVirtualObjectID, _toBeActiveVirtualObjectID);
        }
        [TestMethod]
        public void SetInActiveVirtualObject_Constructor_must_create_object()
        {
            string _toBeInActiveVirtualObjectID = "toBeActiveVirtualObjectID";
            var testobject3 = new SetInActiveVirtualObject("_id", _toBeInActiveVirtualObjectID);
            // test the constructor
            Assert.AreEqual(testobject3.messageID, "_id");
            Assert.AreEqual(testobject3.toBeInActiveVirtualObjectID, _toBeInActiveVirtualObjectID);
        }
        [TestMethod]
        public void RequestAllVirtualObjects_Constructor_must_create_object()
        {
            // time is here a timespan of the length 0 hours, 0 minutes and 10 seconds
            TimeSpan _time = new TimeSpan(0, 0, 10);
            var _senderPID = new PID();
            string _targetActor = "_targetActor";
            var testobject3 = new RequestAllVirtualObjects("_id", _senderPID, _time);
            var testobject4 = new RequestAllVirtualObjects("_id", _senderPID, _targetActor, _time);
            // test the constructor with 3 arguments
            Assert.AreEqual(testobject3.messageID, "_id");
            Assert.AreEqual(testobject3.senderPID, _senderPID);
            Assert.AreEqual(testobject3.time, _time);
            // test the constructor with 4 arguments
            Assert.AreEqual(testobject4.messageID, "_id");
            Assert.AreEqual(testobject4.senderPID, _senderPID);
            Assert.AreEqual(testobject4.targetActor, _targetActor);
            Assert.AreEqual(testobject4.time, _time);
        }
        [TestMethod]
        public void KillVirtualObject_Constructor_must_create_object()
        {
            var _senderPID = new PID();
            string _targetActor = "_targetActor";
            var testobject2 = new KillVirtualObject("_id", _senderPID);
            var testobject3 = new KillVirtualObject("_id", _senderPID, _targetActor);
            // test the constructor with 2 arguments
            Assert.AreEqual(testobject2.messageID, "_id");
            Assert.AreEqual(testobject2.senderPID, _senderPID);
            // test the constructor with 3 arguments
            Assert.AreEqual(testobject3.messageID, "_id");
            Assert.AreEqual(testobject3.senderPID, _senderPID);
            Assert.AreEqual(testobject3.targetActor, _targetActor);
        }
        [TestMethod]
        public void RespondStartVirtualObject_Constructor_must_create_object()
        {
            var _senderPID = new PID();
            string _targetActor = "_targetActor";
            var testobject2 = new RespondStartVirtualObject("_id", _senderPID);
            var testobject3 = new RespondStartVirtualObject("_id", _senderPID, _targetActor);
            // test the constructor with 2 arguments
            Assert.AreEqual(testobject2.messageID, "_id");
            Assert.AreEqual(testobject2.senderPID, _senderPID);
            // test the constructor with 4 arguments
            Assert.AreEqual(testobject3.messageID, "_id");
            Assert.AreEqual(testobject3.senderPID, _senderPID);
            Assert.AreEqual(testobject3.targetActor, _targetActor); ;
        }
        [TestMethod]
        public void RespondStopVirtualObject_Constructor_must_create_object()
        {
            var _senderPID = new PID();
            string _targetActor = "_targetActor";
            var testobject2 = new RespondStopVirtualObject("_id", _senderPID);
            var testobject3 = new RespondStopVirtualObject("_id", _senderPID, _targetActor);
            // test the constructor with 2 arguments
            Assert.AreEqual(testobject2.messageID, "_id");
            Assert.AreEqual(testobject2.senderPID, _senderPID);
            // test the constructor with 3 arguments
            Assert.AreEqual(testobject3.messageID, "_id");
            Assert.AreEqual(testobject3.senderPID, _senderPID);
            Assert.AreEqual(testobject3.targetActor, _targetActor);
        }
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
        public void RespondSetInactiveVirtualObject_Constructor_must_create_object()
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
            CollectionAssert.AreEqual(testobject.codeObjectIDToCodeObjectPID, _dict);
        }
        [TestMethod]
        public void RespondKillVirtualObject_Constructor_must_create_object()
        {
            var _senderPID = new PID();
            string _targetActor = "_targetActor";
            var testobject2 = new RespondKillVirtualObject("_id", _senderPID);
            var testobject3 = new RespondKillVirtualObject("_id", _senderPID, _targetActor);
            // test the constructor with 2 arguments
            Assert.AreEqual(testobject2.messageID, "_id");
            Assert.AreEqual(testobject2.senderPID, _senderPID);
            // test the constructor with 3 arguments
            Assert.AreEqual(testobject3.messageID, "_id");
            Assert.AreEqual(testobject3.senderPID, _senderPID);
            Assert.AreEqual(testobject3.targetActor, _targetActor);
        }

        [TestMethod]
        public void FailedToStartVirtualObject_Constructor_must_create_object()
        {
            var _senderPID = new PID();
            string _targetActor = "_targetActor";
            var testobject2 = new FailedToStartVirtualObject("_id", _senderPID);
            var testobject3 = new FailedToStartVirtualObject("_id", _senderPID, _targetActor);
            // test the constructor with 2 arguments
            Assert.AreEqual(testobject2.messageID, "_id");
            Assert.AreEqual(testobject2.senderPID, _senderPID);
            // test the constructor with 4 arguments
            Assert.AreEqual(testobject3.messageID, "_id");
            Assert.AreEqual(testobject3.senderPID, _senderPID);
            Assert.AreEqual(testobject3.targetActor, _targetActor); ;
        }
        [TestMethod]
        public void FailedToStopVirtualObject_Constructor_must_create_object()
        {
            var _senderPID = new PID();
            string _targetActor = "_targetActor";
            var testobject2 = new FailedToStopVirtualObject("_id", _senderPID);
            var testobject3 = new FailedToStopVirtualObject("_id", _senderPID, _targetActor);
            // test the constructor with 2 arguments
            Assert.AreEqual(testobject2.messageID, "_id");
            Assert.AreEqual(testobject2.senderPID, _senderPID);
            // test the constructor with 3 arguments
            Assert.AreEqual(testobject3.messageID, "_id");
            Assert.AreEqual(testobject3.senderPID, _senderPID);
            Assert.AreEqual(testobject3.targetActor, _targetActor);
        }
        [TestMethod]
        public void FailedToSetActiveVirtualObject_Constructor_must_create_object()
        {
            var _senderPID = new PID();
            string _targetActor = "_targetActor";
            var testobject2 = new FailedToSetActiveVirtualObject("_id", _senderPID);
            var testobject3 = new FailedToSetActiveVirtualObject("_id", _senderPID, _targetActor);
            // test the constructor with 2 arguments
            Assert.AreEqual(testobject2.messageID, "_id");
            Assert.AreEqual(testobject2.senderPID, _senderPID);
            // test the constructor with 3 arguments
            Assert.AreEqual(testobject3.messageID, "_id");
            Assert.AreEqual(testobject3.senderPID, _senderPID);
            Assert.AreEqual(testobject3.targetActor, _targetActor);
        }
        [TestMethod]
        public void FailedToSetInactiveVirtualObject_Constructor_must_create_object()
        {
            var _senderPID = new PID();
            string _targetActor = "_targetActor";
            var testobject2 = new FailedToSetInactiveVirtualObject("_id", _senderPID);
            var testobject3 = new FailedToSetInactiveVirtualObject("_id", _senderPID, _targetActor);
            // test the constructor with 2 arguments
            Assert.AreEqual(testobject2.messageID, "_id");
            Assert.AreEqual(testobject2.senderPID, _senderPID);
            // test the constructor with 3 arguments
            Assert.AreEqual(testobject3.messageID, "_id");
            Assert.AreEqual(testobject3.senderPID, _senderPID);
            Assert.AreEqual(testobject3.targetActor, _targetActor);
        }
        [TestMethod]
        public void FailedToRequestAllVirtualObjects_Constructor_must_create_object()
        {
            // time is here a timespan of the length 0 hours, 0 minutes and 10 seconds
            TimeSpan _time = new TimeSpan(0, 0, 10);
            var _senderPID = new PID();
            string _targetActor = "_targetActor";
            var testobject3 = new FailedToRequestAllVirtualObjects("_id", _senderPID, _time);
            var testobject4 = new FailedToRequestAllVirtualObjects("_id", _senderPID, _targetActor, _time);
            // test the constructor with 3 arguments
            Assert.AreEqual(testobject3.messageID, "_id");
            Assert.AreEqual(testobject3.senderPID, _senderPID);
            Assert.AreEqual(testobject3.time, _time);
            // test the constructor with 4 arguments
            Assert.AreEqual(testobject4.messageID, "_id");
            Assert.AreEqual(testobject4.senderPID, _senderPID);
            Assert.AreEqual(testobject4.targetActor, _targetActor);
            Assert.AreEqual(testobject4.time, _time);
        }
        [TestMethod]
        public void FailedToKillVirtualObject_Constructor_must_create_object()
        {
            var _senderPID = new PID();
            string _targetActor = "_targetActor";
            var testobject2 = new FailedToKillVirtualObject("_id", _senderPID);
            var testobject3 = new FailedToKillVirtualObject("_id", _senderPID, _targetActor);
            // test the constructor with 2 arguments
            Assert.AreEqual(testobject2.messageID, "_id");
            Assert.AreEqual(testobject2.senderPID, _senderPID);
            // test the constructor with 3 arguments
            Assert.AreEqual(testobject3.messageID, "_id");
            Assert.AreEqual(testobject3.senderPID, _senderPID);
            Assert.AreEqual(testobject3.targetActor, _targetActor);
        }
        // test the constructor for the codeObjects
        [TestMethod]
        public void CodeObject_must_be_constructed_correctly()
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
