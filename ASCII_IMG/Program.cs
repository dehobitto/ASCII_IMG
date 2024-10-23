using System;
using System.Windows.Forms;
using System.Drawing;

namespace ASCII_IMG
{
    internal class Program
    {
        private const double WIDTH_OFFSET = 1.5;

        [STAThread]
        static void Main(string[] args)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Images | *.img; *.png; *.jpg;"
            };

            Console.WriteLine("Smash enter to start...");
            //openFileDialog.ShowDialog();
            //char[] colorTable = { '.', ',', ':', '+', '*', '?', '%', '$', '#', '@' };

            while (true)
            {
                Console.ReadLine();

                if (openFileDialog.ShowDialog() != DialogResult.OK) { continue; }

                Console.Clear();

                var bitmap = new Bitmap(openFileDialog.FileName);
                ResizeBitmap(bitmap);
                bitmap.ToGrayScale();

                var converter = new Converter(bitmap);
                var rows = converter.Convert();
                foreach (var row in rows)
                {
                     Console.WriteLine(row);
                }

                Console.SetCursorPosition(0, 0);
            }
        }

        /*private static Bitmap ResizeBitmap(Bitmap bitmap)
        {
            var maxWidth = 120;
            var newHeight = bitmap.Height / WIDTH_OFFSET * maxWidth / bitmap.Width;

            if (bitmap.Width > maxWidth || bitmap.Height > newHeight)
            {
                bitmap = new Bitmap(bitmap, new Size(maxWidth, (int)newHeight));
            }

            return bitmap;
        }*/

        private static Bitmap ResizeBitmap(Bitmap bitmap)
        {
            var maxHeight = 10;
            var newWidth = bitmap.Width * maxHeight / bitmap.Height;

            if (bitmap.Height > maxHeight || bitmap.Width > newWidth)
            {
                bitmap = new Bitmap(bitmap, new Size((int)newWidth, maxHeight));
            }

            return bitmap;
        }
    }
}
