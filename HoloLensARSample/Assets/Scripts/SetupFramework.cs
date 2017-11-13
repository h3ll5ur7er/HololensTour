using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TourBackend
{

    public class SetupFramework : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            GameObject program = new GameObject();
            //Initialize GetTourState
            GetTourState getTourState = program.AddComponent<GetTourState>();
            //Shared Syncobject of SyncActor and GetTourState
            SyncObject syncObject = getTourState.syncObject;
            

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
