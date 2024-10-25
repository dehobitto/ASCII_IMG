using System.Drawing;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using static ASCII_CORE.Graphics;

namespace ASCII_CORE;

internal class App
{
    public void Start()
    {
        VideoCapture capture = new VideoCapture(@"C:\Users\Alex\Downloads\desktop-20240916-22590602_vS670iET.mp4");
        using (Mat image = new Mat()) // Frame image buffer
        {
            // When the movie playback reaches end, Mat.data becomes NULL.
            while (true)
            {
                capture.Read(image); // same as cvQueryFrame
                if (image.Empty()) { break;}
                
                using (Bitmap bitmap = image.ToBitmap())
                {
                    Draw(bitmap);
                }
            }
        }
    }
    
}