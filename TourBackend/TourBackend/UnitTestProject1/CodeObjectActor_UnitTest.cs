using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace TourBackend
{
    [TestClass]
    public class CodeObjectActor_UnitTest
    {
        [TestMethod]
        public void UpdateCodeObjectActor_must_be_constructed_correctly()
        {
            var update = new UpdateCodeObjectActor("1","marker1", 7,new[] {1,2,3},new[] {7,15,28} );
            Assert.AreEqual(update.messageid, "1");
            Assert.AreEqual(update.objectid, "marker1");
            Assert.AreEqual(update.mediaid, 7);
            CollectionAssert.AreEqual(update.position, new[] { 1,2,3});
            CollectionAssert.AreEqual(update.rotation, new[] { 7,15,28});
        }

        [TestMethod]
        public void RequestCodeObject_must_be_constructed_correctly() {
            var request = new RequestCodeObject("message1");
            Assert.AreEqual(request.messageid, "message1");
        }

        [TestMethod]
        public void CodeObjectActor_must_respond_to_RequestCodeObject() {

        }

    }
}
