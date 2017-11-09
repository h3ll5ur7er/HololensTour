using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

namespace TourBackend
{
    public class GetTourStateTest
    {

        [Test]
        public void GetTourState_initialize_empty_SyncObject_correctly()
        {
            // Use the Assert class to test conditions.
            //Testobject
            //GameObject test = new GameObject();
            //Create getTourState object

            GetTourState getTourState = new GameObject().AddComponent<GetTourState>();
            getTourState.Start();

            //Compare if the object is as expected empty
            Assert.AreEqual(getTourState.syncObject.objectid, "sync_id");
            CollectionAssert.AreEqual(getTourState.syncObject.dict, new Dictionary<string, CodeObject>());
            Assert.AreEqual(getTourState.syncObject.timestamp, 10);

        }

        [Test]
        public void GetTourState_lasttimestamp_gets_updated_to_new_timestamp_of_SyncObject()
        {
            //Initiate GetTourState
            GetTourState getTourState = new GameObject().AddComponent<GetTourState>();
            //Create SyncObject
            getTourState.Start();
            //initial timestamp
            System.Int64 inititialTimestamp = 10;
            //Check old time Stamp
            Assert.AreEqual(getTourState.syncObject.timestamp, inititialTimestamp);
            //Update GetTourState
            getTourState.Update();
            //Check if copy has right timestamp
            Assert.AreEqual(getTourState.lasttimestamp, inititialTimestamp);
            //update timestamp
            System.Int64 newTimestamp = 12;
            //Change timestamp
            getTourState.syncObject.timestamp = newTimestamp;
            //Update GetTourState
            getTourState.Update();
            //Check if timestamp is changed on syncObject
            Assert.AreEqual(getTourState.lasttimestamp, newTimestamp);

        }

        // A UnityTest behaves like a coroutine in PlayMode
        // and allows you to yield null to skip a frame in EditMode
        [UnityTest]
        public IEnumerator GetTourStateTestWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // yield to skip a frame
            yield return null;



        }
    }

}