using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TourBackend
{

    public class GetTourState : MonoBehaviour
    {
        public SyncObject syncObject;
        // Use this for initialization
        public void Start()
        {
            //Initialisierung SyncObject
            syncObject = new SyncObject("sync_id", new Dictionary<string, CodeObject>(), 10);
            //Reference syncObject
            //syncObject = GetComponent<SyncObject>();
        }

        // Update is called once per frame
        public void Update()
        {
            //GetCopyOfDictionary
            //ToDo: Test if lock is free
            Dictionary<string, CodeObject> CopyOfDict = syncObject.dict;
            //ReadDictionaryData
            foreach(string objectid in CopyOfDict.Keys)
            {
                //CodeObject with current key
                CodeObject obj = CopyOfDict[objectid];
                //TestTypeOfData
                switch (obj.mediaid)
                {
                    case 1:
                        //instantiate Texture, set position and rotation
                        break;
                    case 2:
                        //instantiate 3D model, set position and rotation
                        break;
                    case 3:
                        //instantiate video, set position and rotation
                        break;
                    default:
                        //media without mediaid -> error
                        break;


                }
               

            }


        }
    }
}