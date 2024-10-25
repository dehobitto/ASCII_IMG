using System.Drawing;
namespace ASCII_CORE;

public class Graphics
{
    private const double WidthOffset = 2.1;

    private static Bitmap ResizeBitmap(Bitmap bitmap)
    {
        int maxWidth = Console.WindowWidth * 2 / 3;
        double newHeight = bitmap.Height / WidthOffset * maxWidth / bitmap.Width;

        if (bitmap.Width > maxWidth || bitmap.Height > newHeight)
        {
            bitmap = new Bitmap(bitmap, new Size(maxWidth, (int)newHeight));
        }

        return bitmap;
    }

    internal static void Draw(Bitmap bitmap)
    {
        Bitmap bitmapResized = ResizeBitmap(bitmap);
        bitmapResized.ToGrayScale();

        Converter converter = new Converter(bitmapResized);
        char[][] rows = converter.Convert();
        foreach (var row in rows)
        {
            Console.WriteLine(row);
        }

        Console.SetCursorPosition(0, 0);
    }
    
}
