using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Proto;

namespace TourBackend
{
    [TestClass]
    public class ControlRecognitionProtocol
    {
        [TestMethod]
        public void StartVirtualObject_Constructor_must_create_object()
        {
            var _pid = new PID();
            var testobject = new StartVirtualObject("_id", _pid);
            Assert.AreEqual(testobject.id, "_id");
            Assert.AreEqual(testobject.pid, _pid);
        }
        [TestMethod]
        public void StopVirtualObject_Constructor_must_create_object()
        {
            var _pid = new PID();
            var testobject = new StopVirtualObject("_id", _pid);
            Assert.AreEqual(testobject.id, "_id");
            Assert.AreEqual(testobject.pid, _pid);
        }
        [TestMethod]
        public void SetActiveVirtualObject_Constructor_must_create_object()
        {
            var _pid = new PID();
            var testobject = new SetActiveVirtualObject("_id", _pid);
            Assert.AreEqual(testobject.id, "_id");
            Assert.AreEqual(testobject.pid, _pid);
        }
        [TestMethod]
        public void SetInactiveVirtualObject_Constructor_must_create_object()
        {
            var _pid = new PID();
            var testobject = new SetInactiveVirtualObject("_id", _pid);
            Assert.AreEqual(testobject.id, "_id");
            Assert.AreEqual(testobject.pid, _pid);
        }
        [TestMethod]
        public void KillVirtualObject_Constructor_must_create_object()
        {
            var _pid = new PID();
            var testobject = new KillVirtualObject("_id", _pid);
            Assert.AreEqual(testobject.id, "_id");
            Assert.AreEqual(testobject.pid, _pid);
        }
        [TestMethod]
        public void RespondStartVirtualObject_Constructor_must_create_object()
        {
            var _pid = new PID();
            var testobject = new RespondStartVirtualObject("_id", _pid);
            Assert.AreEqual(testobject.id, "_id");
            Assert.AreEqual(testobject.pid, _pid);
        }
        [TestMethod]
        public void RespondStopVirtualObject_Constructor_must_create_object()
        {
            var _pid = new PID();
            var testobject = new RespondStopVirtualObject("_id", _pid);
            Assert.AreEqual(testobject.id, "_id");
            Assert.AreEqual(testobject.pid, _pid);
        }
        [TestMethod]
        public void RespondSetActiveVirtualObject_Constructor_must_create_object()
        {
            var _pid = new PID();
            var testobject = new RespondSetActiveVirtualObject("_id", _pid);
            Assert.AreEqual(testobject.id, "_id");
            Assert.AreEqual(testobject.pid, _pid);
        }
        [TestMethod]
        public void RespondSetInactiveVirtualObject_Constructor_must_create_object()
        {
            var _pid = new PID();
            var testobject = new RespondSetInactiveVirtualObject("_id", _pid);
            Assert.AreEqual(testobject.id, "_id");
            Assert.AreEqual(testobject.pid, _pid);
        }
        [TestMethod]
        public void RespondKillVirtualObject_Constructor_must_create_object()
        {
            var _pid = new PID();
            var testobject = new RespondKillVirtualObject("_id", _pid);
            Assert.AreEqual(testobject.id, "_id");
            Assert.AreEqual(testobject.pid, _pid);
        }
        [TestMethod]
        public void FailedToStartVirtualObject_Constructor_must_create_object()
        {
            var _pid = new PID();
            var testobject = new FailedToStartVirtualObject("_id", _pid);
            Assert.AreEqual(testobject.id, "_id");
            Assert.AreEqual(testobject.pid, _pid);
        }
        [TestMethod]
        public void FailedToStopVirtualObject_Constructor_must_create_object()
        {
            var _pid = new PID();
            var testobject = new FailedToStopVirtualObject("_id", _pid);
            Assert.AreEqual(testobject.id, "_id");
            Assert.AreEqual(testobject.pid, _pid);
        }
        [TestMethod]
        public void FailedToSetActiveVirtualObject_Constructor_must_create_object()
        {
            var _pid = new PID();
            var testobject = new FailedToSetActiveVirtualObject("_id", _pid);
            Assert.AreEqual(testobject.id, "_id");
            Assert.AreEqual(testobject.pid, _pid);
        }
        [TestMethod]
        public void FailedToSetInactiveVirtualObject_Constructor_must_create_object()
        {
            var _pid = new PID();
            var testobject = new FailedToSetInactiveVirtualObject("_id", _pid);
            Assert.AreEqual(testobject.id, "_id");
            Assert.AreEqual(testobject.pid, _pid);
        }
        [TestMethod]
        public void FailedToKillVirtualObject_Constructor_must_create_object()
        {
            var _pid = new PID();
            var testobject = new FailedToKillVirtualObject("_id", _pid);
            Assert.AreEqual(testobject.id, "_id");
            Assert.AreEqual(testobject.pid, _pid);
        }
    }
    [TestClass]
    public class ControlInputProtocol {

    }
}
