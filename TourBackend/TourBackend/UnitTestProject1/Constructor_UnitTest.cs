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
        public void SyncObject_must_be_correctly_constructed() { 

            var dict = new Dictionary<string, CodeObject>();
            var cd1 = new CodeObject("cd1", 1,new[]{ 1,2, 3}, new[]{4,5,6 });
            var cd2 = new CodeObject("cd2", 1, new[] { 4, 7, 8 }, new[] { 1, 19, 3 }); // Just build two "random" CodeObjects

            dict.Add("cd1",cd1);
            dict.Add("cd2", cd2);

            Assert.AreEqual(dict["cd1"],cd1); // Check that the dict contains the right CodeObjects
            Assert.AreEqual(dict["cd2"],cd2);

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
        public void CreateNewVirtualObject_Constructor_must_create_object()
        {
            var _senderPID = new PID();
            string _targetActor = "_targetActor";
            var testobject2 = new CreateNewVirtualObject("_id", _senderPID);
            var testobject3 = new CreateNewVirtualObject("_id", _senderPID, _targetActor);
            // test the constructor with 2 arguments
            Assert.AreEqual(testobject2.messageID, "_id");
            Assert.AreEqual(testobject2.senderPID, _senderPID);
            // test the constructor with 3 arguments
            Assert.AreEqual(testobject3.messageID, "_id");
            Assert.AreEqual(testobject3.senderPID, _senderPID);
            Assert.AreEqual(testobject3.targetActor, _targetActor);
        }
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
            var _senderPID = new PID();
            string _targetActor = "_targetActor";
            var testobject2 = new SetActiveVirtualObject("_id", _senderPID);
            var testobject3 = new SetActiveVirtualObject("_id", _senderPID, _targetActor);
            // test the constructor with 2 arguments
            Assert.AreEqual(testobject2.messageID, "_id");
            Assert.AreEqual(testobject2.senderPID, _senderPID);
            // test the constructor with 3 arguments
            Assert.AreEqual(testobject3.messageID, "_id");
            Assert.AreEqual(testobject3.senderPID, _senderPID);
            Assert.AreEqual(testobject3.targetActor, _targetActor);
        }
        [TestMethod]
        public void SetInactiveVirtualObject_Constructor_must_create_object()
        {
            var _senderPID = new PID();
            string _targetActor = "_targetActor";
            var testobject2 = new SetInactiveVirtualObject("_id", _senderPID);
            var testobject3 = new SetInactiveVirtualObject("_id", _senderPID, _targetActor);
            // test the constructor with 2 arguments
            Assert.AreEqual(testobject2.messageID, "_id");
            Assert.AreEqual(testobject2.senderPID, _senderPID);
            // test the constructor with 3 arguments
            Assert.AreEqual(testobject3.messageID, "_id");
            Assert.AreEqual(testobject3.senderPID, _senderPID);
            Assert.AreEqual(testobject3.targetActor, _targetActor);
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
        public void RespondCreateNewVirtualObject_Constructor_must_create_object()
        {
            var _senderPID = new PID();
            string _targetActor = "_targetActor";
            var testobject2 = new RespondCreateNewVirtualObject("_id", _senderPID);
            var testobject3 = new RespondCreateNewVirtualObject("_id", _senderPID, _targetActor);
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
            var _senderPID = new PID();
            string _targetActor = "_targetActor";
            var testobject2 = new RespondSetActiveVirtualObject("_id", _senderPID);
            var testobject3 = new RespondSetActiveVirtualObject("_id", _senderPID, _targetActor);
            // test the constructor with 2 arguments
            Assert.AreEqual(testobject2.messageID, "_id");
            Assert.AreEqual(testobject2.senderPID, _senderPID);
            // test the constructor with 3 arguments
            Assert.AreEqual(testobject3.messageID, "_id");
            Assert.AreEqual(testobject3.senderPID, _senderPID);
            Assert.AreEqual(testobject3.targetActor, _targetActor);
        }
        [TestMethod]
        public void RespondSetInactiveVirtualObject_Constructor_must_create_object()
        {
            var _senderPID = new PID();
            string _targetActor = "_targetActor";
            var testobject2 = new RespondSetInactiveVirtualObject("_id", _senderPID);
            var testobject3 = new RespondSetInactiveVirtualObject("_id", _senderPID, _targetActor);
            // test the constructor with 2 arguments
            Assert.AreEqual(testobject2.messageID, "_id");
            Assert.AreEqual(testobject2.senderPID, _senderPID);
            // test the constructor with 3 arguments
            Assert.AreEqual(testobject3.messageID, "_id");
            Assert.AreEqual(testobject3.senderPID, _senderPID);
            Assert.AreEqual(testobject3.targetActor, _targetActor);
        }
        [TestMethod]
        public void RespondRequestAllVirtualObjects_Constructor_must_create_object()
        {
            // time is here a timespan of the length 0 hours, 0 minutes and 10 seconds
            TimeSpan _time = new TimeSpan(0, 0, 10);
            var _senderPID = new PID();
            string _targetActor = "_targetActor";
            var testobject3 = new RespondRequestAllVirtualObjects("_id", _senderPID, _time);
            var testobject4 = new RespondRequestAllVirtualObjects("_id", _senderPID, _targetActor, _time);
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
        public void FailedToCreateNewVirtualObject_Constructor_must_create_object()
        {
            var _senderPID = new PID();
            string _targetActor = "_targetActor";
            var testobject2 = new FailedToCreateNewVirtualObject("_id", _senderPID);
            var testobject3 = new FailedToCreateNewVirtualObject("_id", _senderPID, _targetActor);
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
    }
}
