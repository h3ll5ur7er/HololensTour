using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

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
            
            GetTourState getTourState = new GameObject().GetComponent<GetTourState>();
            
            if (getTourState)
            {
                //Compare if the object is as expected empty
                Assert.AreEqual(getTourState.syncObject.objectid, "");
                Assert.AreEqual(getTourState.syncObject.dict.Count, 0);
                Assert.AreEqual(getTourState.syncObject.timestamp, 0);
            }
            else
            {
                Debug.Log("why the fuck doesn't work!");
                Assert.AreEqual(0, 1);
            }
            
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