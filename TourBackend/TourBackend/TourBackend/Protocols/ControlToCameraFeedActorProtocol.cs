using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourBackend
{
    public class CameraFeedSyncObject {

        public Windows.Graphics.Imaging.SoftwareBitmap bitmap;
        public string id;
        public object thisLock = new Object();

        public CameraFeedSyncObject(string _id, Windows.Graphics.Imaging.SoftwareBitmap _bitmap) {
            id = _id;
            bitmap = _bitmap;
        }
    }
}
