using System.Drawing;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace ASCII_IMG
{
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Start();
        }

        private const double WIDTH_OFFSET = 2.2;
        public static void Start()
        {
            VideoCapture capture = new VideoCapture(0);
            using (Mat image = new Mat()) // Frame image buffer
            {
                // When the movie playback reaches end, Mat.data becomes NULL.
                while (true)
                {
                    capture.Read(image); // same as cvQueryFrame
                    if (image.Empty()) break;
                    var bitmap = BitmapConverter.ToBitmap(image);
                    bitmap = ResizeBitmap(bitmap);
                    bitmap.ToGrayScale();

                    var converter = new Converter(bitmap);
                    var rows = converter.Convert();
                    foreach (var row in rows)
                    {
                        Console.WriteLine(row);
                    }

                    Console.SetCursorPosition(0, 0);
                    Cv2.WaitKey(30);
                }
            }
        }

        private static Bitmap ResizeBitmap(Bitmap bitmap)
        {
            var maxWidth = 350;
            var newHeight = bitmap.Height / WIDTH_OFFSET * maxWidth / bitmap.Width;

            if (bitmap.Width > maxWidth || bitmap.Height > newHeight)
            {
                bitmap = new Bitmap(bitmap, new System.Drawing.Size(maxWidth, (int)newHeight));
            }

            return bitmap;
        }
    }
}
