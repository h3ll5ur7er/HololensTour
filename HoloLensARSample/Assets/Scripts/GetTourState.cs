using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TourBackend
{

    public class GetTourState : MonoBehaviour
    {
        public SyncObject syncObject;
        public Dictionary<string, CodeObject> CopyOfDict = new Dictionary<string, CodeObject>();
        public System.Int64 lasttimestamp;
        // Use this for initialization
        public void Start()
        {
            //Initialisierung SyncObject
            lasttimestamp = 10;
            syncObject = new SyncObject("syncid", new Dictionary<string, CodeObject>(), lasttimestamp);
            CopyOfDict = new Dictionary<string, CodeObject>();

        }

        // Update is called once per frame
        public void Update()
        {
            //GetCopyOfDictionary
            //ToDo: Test if lock is free


            //Only copy if timestamp changed
            if (syncObject.timestamp != lasttimestamp)
            {
                lasttimestamp = syncObject.timestamp;
                CopyOfDict = syncObject.dict;
                //ReadDictionaryData
                foreach (string objectid in CopyOfDict.Keys)
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
}