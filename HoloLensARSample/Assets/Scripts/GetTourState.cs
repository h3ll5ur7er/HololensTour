using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TourBackend
{

    public class GetTourState : MonoBehaviour
    {
        public SyncObject syncObject;
        public CameraFeedSyncObject cfSyncObject;
        public Dictionary<int, CodeObject> CopyOfDict;// = new Dictionary<string, CodeObject>();
        public System.Int64 lasttimestamp;
        public int numberOfMarkers = 2;
        //pattername to mediaid
        public Dictionary<string, string> mediaidDict;


        // Use this for initialization
        public void Start()
        {
            //Initialisierung SyncObject
            lasttimestamp = 10;
            syncObject = new SyncObject("syncid", new Dictionary<int, CodeObject>());
            syncObject.SetTimeStamp(lasttimestamp);
            CopyOfDict = new Dictionary<int, CodeObject>();
            //Initialisation of CameraFeedSyncObject
            cfSyncObject = new CameraFeedSyncObject("cfsyncid");

            //Todo:
            //Initialize ARVideo and give reference of CameraFeedSyncObject
            VideoController videoController = this.gameObject.AddComponent<VideoController>();
            videoController.TaskStarter(cfSyncObject);
            //Initialize Framework and give refences of SyncObject and CameraFeedSyncObject 
            //start it in dll somehow -> Talk with Moritz

            
            //loading pattern names from maybe a txt file called single.txt For the beginning hardcoded
            string[,] patterns = new string[2,2];
            //numberOfMarkers = numberOfPatterns
            patterns[0,0] = "hiro.patt";
            patterns[0,1] = "1";
            patterns[1,0] = "kanji.patt";
            patterns[1,1] = "2";
            

            //Marker codeObject array
            CodeObject[] markers = new CodeObject[numberOfMarkers];
            mediaidDict = new Dictionary<string, string>();
            for (int index = 0; index < numberOfMarkers; index++)
            {
                markers[index] = new CodeObject();
                markers[index].singleFileName = patterns[index,0];
                markers[index].type = CodeObject.MarkerType.single;
                
                //mediaidDict is created
                mediaidDict.Add(patterns[index,0], patterns[index,1]);
            }
            FrameWork frameWork = new FrameWork(syncObject, cfSyncObject, markers);
            frameWork.Initialize();


    
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
                //only reference 
                //CopyOfDict = syncObject.dict;

                //Deep copy of dict
                CopyOfDict = CopySyncDict.CopyInt(syncObject.dict);

                //ReadDictionaryData
                foreach (int id in CopyOfDict.Keys)
                {
                    //CodeObject with current key
                    CodeObject obj = CopyOfDict[id];

                    string mediaid = mediaidDict[obj.singleFileName];
                   
                    switch (mediaid)
                    {
                        case "1":
                            //instantiate Texture, set position and rotation
                            InstantiateTexture(obj);
                            break;
                        case "2":
                            //instantiate 3D model, set position and rotation
                            InstantiateModel(obj);
                            break;
                        case "3":
                            //instantiate video, set position and rotation
                            InstantiateVideo(obj);
                            break;
                        default:
                            //media without mediaid -> error
                            break;


                    }

                }
            }


        }

        public void InstantiateTexture(CodeObject obj)
        {
            //Let the option open to load the texture itself instead of loading a prefab. Can be done later
            GameObject textureobj = (GameObject)Resources.Load("Texture/" + obj.singleFileName);
            textureobj = Instantiate(textureobj);
            textureobj.transform.name = obj.id.ToString();

            //Set position
            textureobj.transform.position = SetPosition(obj);

            //Set eulerangles
            textureobj.transform.eulerAngles = SetEulerangles(obj);

        }

        public void InstantiateModel(CodeObject obj)
        {
            GameObject modelobj = (GameObject)Resources.Load("Model/" + obj.singleFileName);
            modelobj = Instantiate(modelobj);
            modelobj.transform.name = obj.id.ToString();

            //Set position
            modelobj.transform.position = SetPosition(obj);

            //Set eulerangles
            modelobj.transform.eulerAngles = SetEulerangles(obj);

        }

        public void InstantiateVideo(CodeObject obj)
        {
            //Let the option open to load the texture itself instead of loading a prefab. Can be done later
            GameObject videoobj = (GameObject)Resources.Load("Video/" + obj.singleFileName);
            videoobj = Instantiate(videoobj);
            videoobj.transform.name = obj.id.ToString();

            //Set position
            videoobj.transform.position = SetPosition(obj);

            //Set eulerangles
            videoobj.transform.eulerAngles = SetEulerangles(obj);

            //Get video component of prefab
            var videoplayer = videoobj.GetComponent<UnityEngine.Video.VideoPlayer>();

            Debug.Log(videoplayer.isPrepared + " : Status of the videoplayer");

            videoplayer.playOnAwake = true;
            videoplayer.waitForFirstFrame = true;

            //Start playing the video
            //videoplayer.Play();
            //videoplay.isPlaying is not necesarry true now. It has to be loaded first.
            //To do: possibility to stop and restart the video

        }


        public void DestroyObject(GameObject obj)
        {
            DestroyImmediate(obj);
        }

        public Vector3 SetPosition(CodeObject obj)
        {
            float x = obj.position[0];
            float y = obj.position[1];
            float z = obj.position[2];
            Vector3 position = new Vector3(x, y, z);
            return position;

        }

        public Vector3 SetEulerangles(CodeObject obj)
        {
            float rotX = obj.rotation[0];
            float rotY = obj.rotation[1];
            float rotZ = obj.rotation[2];
            Vector3 eulerangles = new Vector3(rotX, rotY, rotZ);
            return eulerangles;
        }
    }
}