using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ASCII_CORE;
internal static class Extensions
{
    internal static void ToGrayScale(this Bitmap bitmap)
    {
        // Блокируем все пиксели изображения в память
        BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);

        int bytesPerPixel = Image.GetPixelFormatSize(bitmap.PixelFormat) / 8;
        int byteCount = bmpData.Stride * bitmap.Height;
        byte[] pixelBuffer = new byte[byteCount];
        IntPtr ptrFirstPixel = bmpData.Scan0;

        // Копируем данные изображения в массив пикселей
        Marshal.Copy(ptrFirstPixel, pixelBuffer, 0, byteCount);

        for (int y = 0; y < bitmap.Height; y++)
        {
            int rowOffset = y * bmpData.Stride;

            for (int x = 0; x < bitmap.Width; x++)
            {
                int index = rowOffset + x * bytesPerPixel;

                // Усреднение цветов для получения градаций серого
                byte avg = (byte)((pixelBuffer[index] + pixelBuffer[index + 1] + pixelBuffer[index + 2]) / 3);

                // Устанавливаем новый серый цвет для каждого пикселя
                pixelBuffer[index] = avg;        // Blue
                pixelBuffer[index + 1] = avg;    // Green
                pixelBuffer[index + 2] = avg;    // Red
            }
        }

        // Копируем изменённые пиксели обратно в изображение
        Marshal.Copy(pixelBuffer, 0, ptrFirstPixel, byteCount);
        bitmap.UnlockBits(bmpData);
    }
}
