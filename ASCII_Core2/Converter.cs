using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
namespace ASCII_CORE;

internal class Converter
{
    private readonly Bitmap _bitmap;
    private readonly char[] _colorTable = { '.', ',', ':', '+', '*', '?', '%', '$', '#', '@' };

    internal Converter(Bitmap bitmap)
    {
        _bitmap = bitmap;
    }

    internal char[][] Convert()
    {
        char[][] result = new char[_bitmap.Height][];

        // Заблокируем биты изображения для прямого доступа
        BitmapData bmpData = _bitmap.LockBits(new Rectangle(0, 0, _bitmap.Width, _bitmap.Height), ImageLockMode.ReadOnly, _bitmap.PixelFormat);

        int bytesPerPixel = Image.GetPixelFormatSize(_bitmap.PixelFormat) / 8;
        int byteCount = bmpData.Stride * _bitmap.Height;
        byte[] pixelBuffer = new byte[byteCount];
        IntPtr ptrFirstPixel = bmpData.Scan0;

        // Скопируем данные изображения в массив байтов
        Marshal.Copy(ptrFirstPixel, pixelBuffer, 0, byteCount);

        for (int y = 0; y < _bitmap.Height; y++)
        {
            result[y] = new char[_bitmap.Width];
            int rowOffset = y * bmpData.Stride;

            for (int x = 0; x < _bitmap.Width; x++)
            {
                int index = rowOffset + x * bytesPerPixel;
                byte pixelValue = pixelBuffer[index]; // Получаем значение для канала Blue (т.к. они равны в серых изображениях)

                int mapIndex = (int)Map(pixelValue, 0, 255, 0, _colorTable.Length - 1);
                result[y][x] = _colorTable[mapIndex];
            }
        }

        // Разблокируем биты изображения
        _bitmap.UnlockBits(bmpData);

        return result;
    }

    private float Map(float valueToMap, float start1, float end1, float start2, float end2)
    {
        return ((valueToMap - start1) / (end1 - start1)) * (end2 - start2) + start2;
    }
}

