using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;

namespace TourBackend
{
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

            if (_stream != null)
            {
                System.Drawing.Image.FromStream(_stream).Save(temp);
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
