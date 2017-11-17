using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proto;
using System.Threading;

namespace TourBackend
{

    public class FrameWork {

        #region Public Enums

        /// <summary>
        /// ThresholdMode types, same definition as ARToolKit. [public use]
        /// </summary>
        public enum ThresholdMode
        {
            AR_LABELING_THRESH_MODE_MANUAL = ARUWP.AR_LABELING_THRESH_MODE_MANUAL,
            AR_LABELING_THRESH_MODE_AUTO_MEDIAN = ARUWP.AR_LABELING_THRESH_MODE_AUTO_MEDIAN,
            AR_LABELING_THRESH_MODE_AUTO_OTSU = ARUWP.AR_LABELING_THRESH_MODE_AUTO_OTSU,
            AR_LABELING_THRESH_MODE_AUTO_ADAPTIVE = ARUWP.AR_LABELING_THRESH_MODE_AUTO_ADAPTIVE,
            AR_LABELING_THRESH_MODE_AUTO_BRACKETING = ARUWP.AR_LABELING_THRESH_MODE_AUTO_BRACKETING
        }

        /// <summary>
        /// LabelingMode types, same definiton as ARToolKit. [public use]
        /// </summary>
        public enum LabelingMode
        {
            AR_LABELING_WHITE_REGION = ARUWP.AR_LABELING_WHITE_REGION,
            AR_LABELING_BLACK_REGION = ARUWP.AR_LABELING_BLACK_REGION
        }

        /// <summary>
        /// PatternDetectionMode types, same definition as ARToolKit. [public use]
        /// </summary>
        public enum PatternDetectionMode
        {
            AR_TEMPLATE_MATCHING_COLOR = ARUWP.AR_TEMPLATE_MATCHING_COLOR,
            AR_TEMPLATE_MATCHING_MONO = ARUWP.AR_TEMPLATE_MATCHING_MONO,
            AR_MATRIX_CODE_DETECTION = ARUWP.AR_MATRIX_CODE_DETECTION,
            AR_TEMPLATE_MATCHING_COLOR_AND_MATRIX = ARUWP.AR_TEMPLATE_MATCHING_COLOR_AND_MATRIX,
            AR_TEMPLATE_MATCHING_MONO_AND_MATRIX = ARUWP.AR_TEMPLATE_MATCHING_MONO_AND_MATRIX
        }

        /// <summary>
        /// MatrixCodeType types, same definition as ARToolKit. [public use]
        /// </summary>
        public enum MatrixCodeType
        {
            AR_MATRIX_CODE_3x3 = ARUWP.AR_MATRIX_CODE_3x3,
            AR_MATRIX_CODE_3x3_PARITY65 = ARUWP.AR_MATRIX_CODE_3x3_PARITY65,
            AR_MATRIX_CODE_3x3_HAMMING63 = ARUWP.AR_MATRIX_CODE_3x3_HAMMING63,
            AR_MATRIX_CODE_4x4 = ARUWP.AR_MATRIX_CODE_4x4,
            AR_MATRIX_CODE_4x4_BCH_13_9_3 = ARUWP.AR_MATRIX_CODE_4x4_BCH_13_9_3,
            AR_MATRIX_CODE_4x4_BCH_13_5_5 = ARUWP.AR_MATRIX_CODE_4x4_BCH_13_5_5
        }

        /// <summary>
        /// Image processing mode, same definition as ARToolKit. [public use]
        /// </summary>
        public enum ImageProcMode
        {
            AR_IMAGE_PROC_FIELD_IMAGE = ARUWP.AR_IMAGE_PROC_FIELD_IMAGE,
            AR_IMAGE_PROC_FRAME_IMAGE = ARUWP.AR_IMAGE_PROC_FRAME_IMAGE
        }

        /// <summary>
        /// Log level of native library, same definition as ARToolKit. [public use]
        /// </summary>
        public enum AR_LOG_LEVEL
        {
            AR_LOG_LEVEL_DEBUG = 0,
            AR_LOG_LEVEL_INFO,
            AR_LOG_LEVEL_WARN,
            AR_LOG_LEVEL_ERROR,
            AR_LOG_LEVEL_REL_INFO
        }

        #endregion

        #region setters when access is granted


        /// <summary>
        /// Log the version of ARToolKit using Debug.Log(). [internal use]
        /// </summary>
        private void LogVersionString()
        {
                StringBuilder buffer = new StringBuilder(20);
                ARUWP.aruwpGetARToolKitVersion(buffer, 20);
        }

        /// <summary>
        /// Log the current frame information using Debug.Log(). [internal use]
        /// </summary>
        private void LogFrameInforamtion()
        {
                int width, height, pixelSize;
                StringBuilder buffer = new StringBuilder(30);
                ARUWP.aruwpGetFrameParams(out width, out height, out pixelSize, buffer, 30);
        }


        /// <summary>
        /// Set threshold parameter at runtime. [public use]
        /// </summary>
        /// <param name="o">New parameter</param>
        public void SetVideoThreshold(int o)
        {

                ARUWP.aruwpSetVideoThreshold(o);
        }

        /// <summary>
        /// Set thresholdMode parameter at runtime. [public use]
        /// </summary>
        /// <param name="o">New parameter</param>
        public void SetVideoThresholdMode(ThresholdMode o)
        {
                ARUWP.aruwpSetVideoThresholdMode((int)o);
        }

        /// <summary>
        /// Set labelingMode parameter at runtime. [public use]
        /// </summary>
        /// <param name="o">New parameter</param>
        public void SetLabelingMode(LabelingMode o)
        {
                ARUWP.aruwpSetLabelingMode((int)o);
        }


        /// <summary>
        /// Set patternDetectionMode parameter at runtime. [public use]
        /// </summary>
        /// <param name="o">New parameter</param>
        public void SetPatternDetectionMode(PatternDetectionMode o)
        {
                ARUWP.aruwpSetPatternDetectionMode((int)o);
        }

        /// <summary>
        /// Set borderSize parameter at runtime. [public use]
        /// </summary>
        /// <param name="o">New parameter</param>
        public void SetBorderSize(float o)
        {

                ARUWP.aruwpSetBorderSize(o);

        }

        /// <summary>
        /// Set matrixCodeType parameter at runtime. [public use]
        /// </summary>
        /// <param name="o">New parameter</param>
        public void SetMatrixCodeType(MatrixCodeType o)
        {

                ARUWP.aruwpSetMatrixCodeType((int)o);
        }

        /// <summary>
        /// Set imageProcMode parameter at runtime. [public use]
        /// </summary>
        /// <param name="o">New parameter</param>
        public void SetImageProcMode(ImageProcMode o)
        {
                ARUWP.aruwpSetImageProcMode((int)o);
        }

        #endregion

        #region used enums

        public ThresholdMode thresholdMode = ThresholdMode.AR_LABELING_THRESH_MODE_MANUAL;
        public LabelingMode labelingMode = LabelingMode.AR_LABELING_BLACK_REGION;
        public PatternDetectionMode patternDetectionMode = PatternDetectionMode.AR_TEMPLATE_MATCHING_COLOR;
        public MatrixCodeType matrixCodeType = MatrixCodeType.AR_MATRIX_CODE_3x3;
        public ImageProcMode imageProcMode = ImageProcMode.AR_IMAGE_PROC_FRAME_IMAGE;

        #endregion

        public bool useCameraParamFile = true;
        public string cameraParam = "hololens896x504.dat";
        public int frameWidth = 896;
        public int frameHeight = 504;
        private byte[] cameraParamBuffer = null;
        public CodeObject[] markers;
        public float borderSize = 0.25f;
        public int threshold = 100;

        public Dictionary<int, CodeObject> idToCodeObject;

        private PID pidctrl;
        private SyncObject syncobj;
        private CameraFeedSyncObject video;


        public FrameWork(SyncObject _syncobj, CameraFeedSyncObject _video, CodeObject[] _markers) {
            syncobj = _syncobj;
            video = _video;
            markers = _markers;
        }

        public int AddMarker(CodeObject m)
        {
                string str = "";
                switch (m.type)
                {
                    case CodeObject.MarkerType.single:
                        str = "single;Data/StreamingAssets/" + m.singleFileName + ";" + m.singleWidth;
                        break;
                }
                m.id = ARUWP.aruwpAddMarker(str);
            return m.id;
            
        }

        public bool InitializeDLL() {

        ARUWP.aruwpRegisterLogCallbackWrapper(ARUWP.Log);
            ARUWP.aruwpSetLogLevel((int)(AR_LOG_LEVEL.AR_LOG_LEVEL_REL_INFO));
            var ret = ARUWP.aruwpInitialiseAR(frameWidth, frameHeight, ARUWP.AR_PIXEL_FORMAT_RGBA);
            foreach (var m in markers)
            {
                var temp = AddMarker(m);
                m.id = temp;
                idToCodeObject.Add(temp, new CodeObject(m));
            }
            if (useCameraParamFile)
            {
                ret = ARUWP.aruwpStartRunning("Data/StreamingAssets/" + cameraParam);
            }
            else
            {
                if (cameraParamBuffer != null)
                {
                    ret = ARUWP.aruwpStartRunningBuffer(cameraParamBuffer, cameraParamBuffer.Length);
                }
                else
                {
                    return false;
                }
            }
            if (!ret)
            {
                return false;
            }

            // Set default tracking options
            SetVideoThreshold(threshold);
            SetVideoThresholdMode(thresholdMode);
            SetVideoThresholdMode(thresholdMode);
            SetLabelingMode(labelingMode);
            SetPatternDetectionMode(patternDetectionMode);
            SetBorderSize(borderSize);
            SetMatrixCodeType(matrixCodeType);
            SetImageProcMode(imageProcMode);

            return true;

        }

        public void Initialize()
        {
            var propsctrl = Actor.FromProducer(() => new ControlActor("ctrl", syncobj, video, idToCodeObject));
            pidctrl = Actor.Spawn(propsctrl);

            InitializeDLL();

            pidctrl.RequestAsync<RespondStartFramework>(new StartFramework(pidctrl), TimeSpan.FromSeconds(1));
        }

        public PID GetPID() {
            return this.pidctrl;
        }

    }

    public class StartFramework {
        public PID ctrl;
        public StartFramework(PID _ctrl) {
            ctrl = _ctrl;
        }
    }

    public class RespondStartFramework {
        public RespondStartFramework(){ }
    }

    // Attention: This Actor is subject to change!
    // Neither the constructors nor any other function is guaranteed to stay unchanged!

    public class ControlActor : IActor
    {
        public string id { get; }
        public SyncObject sync;
        public CameraFeedSyncObject video; // This field will *certainly* change
        public PID recognitionManager;
        public PID syncActor;
        public PID cameraFeedSyncActor;
        public PID self;
        public Dictionary<int, CodeObject> dict;
        // More managed PIDs are to follow

        public void Start() {
            var syncprops = Actor.FromProducer(() => new SyncActor("syncactor", sync));
            this.syncActor = Actor.Spawn(syncprops);

            var recogprops = Actor.FromProducer(() => new RecognitionManager("recognitionmanager", dict));
            this.recognitionManager = Actor.Spawn(recogprops);

            var cameraFeedSyncprops = Actor.FromProducer(() => new CameraFeedActor("camerafeedactor", video, self));
            this.recognitionManager = Actor.Spawn(cameraFeedSyncprops);
        }

        public ControlActor(string _id, SyncObject _sync, CameraFeedSyncObject _video, Dictionary<int, CodeObject> _dict)
        {
            id = _id;
            sync = _sync;
            video = _video;
            dict = _dict;
        }

        // This constructor is only meant for debugging 
        // It is functionally identically to the usual constructor except that it 
        // links the reference _debugPID to either of the newly created Actors, chosen according to the int argument,
        // as this allows for more convenient testing.
        // It also doesn't spawn the CameraFeedActor
        // Key for the int argument:
        // 1: RecognitionManager
        // 2: SyncActor

        public ControlActor(string _id, SyncObject _sync, CameraFeedSyncObject _video, ref PID _debugPID, int debug)
        {
            id = _id;
            sync = _sync;
            video = _video;

            var syncprops = Actor.FromProducer(() => new SyncActor("syncactor", sync));
            this.syncActor = Actor.Spawn(syncprops);

            var recogprops = Actor.FromProducer(() => new RecognitionManager("recognitionmanager", dict));
            this.recognitionManager = Actor.Spawn(recogprops);

            switch (debug)
            {
                case 1:
                    _debugPID = recognitionManager;
                    break;
                case 2:
                    _debugPID = syncActor;
                    break;
                default:
                    break;
            }
        }



        public Task ReceiveAsync(IContext context)
        {
            var msg = context.Message;
            switch (msg)
            {
                case StartFramework s:
                    self = s.ctrl;
                    this.Start();
                    context.Sender.Tell(new RespondStartFramework());
                    break;
                case NewFrameArrived n:
                    recognitionManager.Tell(n);
                    break;
                case RespondNewFrameArrived r:
                    recognitionManager.Request(new RequestAllVirtualObjects(r.messageID,TimeSpan.FromMilliseconds(25)), self);
                break;
                case RespondRequestAllVirtualObjects r:
                    syncActor.Request(new WriteCurrentTourState(r.messageID, r.newCodeObjectIDToCodeObject), self);
                    break;
            }
            return Actor.Done;
        }

    }
}
