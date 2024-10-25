using System.Drawing;
namespace ASCII_CORE;

public class Graphics
{
    private const double WidthOffset = 2.1;

    private static Bitmap ResizeBitmap(Bitmap bitmap)
    {
        int maxHeight = Console.WindowHeight * 2 / 3;
        double newWidth = bitmap.Width * WidthOffset * maxHeight / bitmap.Height;

        if (bitmap.Width > newWidth || bitmap.Height > maxHeight)
        {
            bitmap = new Bitmap(bitmap, new Size((int)newWidth, maxHeight));
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
