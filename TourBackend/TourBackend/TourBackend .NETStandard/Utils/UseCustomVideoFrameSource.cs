using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Proto;

namespace TourBackend
{

    // TestActor which links every received message to a referenced object
    public class TestActor : IActor
    {
        public object testMsg;

        public TestActor(ref object _msg)
        {
            testMsg = new Object();
            _msg = testMsg;
        }

        public Task ReceiveAsync(IContext context)
        {
            var msg = context.Message;
            testMsg = msg;
            return Actor.Done;
        }
    }

    public static class Utils
    {
        // Hilfsfunktion. Wandelt einen "normalen" Stream in einen RandomAccessStream um
        public static async Task<Windows.Storage.Streams.IRandomAccessStream> ConvertToRandomAccessStream(Stream _stream)
        {
            var memoryStream = new MemoryStream();
            await _stream.CopyToAsync(memoryStream);

            var randomAccessStream = new Windows.Storage.Streams.InMemoryRandomAccessStream();

            var outputStream = randomAccessStream.GetOutputStreamAt(0);
            var dw = new Windows.Storage.Streams.DataWriter(outputStream);
            var task = new Task(() => dw.WriteBytes(memoryStream.ToArray()));
            task.Start();

            await task;
            await dw.StoreAsync();

            await outputStream.FlushAsync();

            return randomAccessStream;
        }

        // Stellt einen Testframe zu Verfügung
        public static async Task<SoftwareBitmap> CreateTestFrame(Stream _stream)
        {

            string temp = Path.GetTempFileName();
            
            if (_stream != null && temp != null)
            {
                var filestream = File.Open(temp, System.IO.FileMode.OpenOrCreate);
                _stream.Seek(0, SeekOrigin.Begin);
                _stream.CopyTo(filestream);
                _stream.Flush();
                filestream.Flush();
                filestream.Close();
            }
            
            var inputFile = await Windows.Storage.StorageFile.GetFileFromPathAsync(temp);

            Windows.Graphics.Imaging.SoftwareBitmap softwareBitmap;

            using (Windows.Storage.Streams.IRandomAccessStream stream = await inputFile.OpenAsync(Windows.Storage.FileAccessMode.Read))
            {
                Windows.Graphics.Imaging.BitmapDecoder decoder = await Windows.Graphics.Imaging.BitmapDecoder.CreateAsync(stream);
                softwareBitmap = await decoder.GetSoftwareBitmapAsync();
            }

            return softwareBitmap;
        }
    }
}
