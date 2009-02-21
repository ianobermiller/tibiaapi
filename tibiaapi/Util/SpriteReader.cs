using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace Tibia.Util
{
    public class SpriteReader
    {
        public static Image GetSpriteImage(Objects.Client client, int spriteId)
        {
            return GetSpriteImage(
                Path.Combine(
                    Path.GetDirectoryName(client.Process.MainModule.FileName), 
                    "Tibia.spr"), 
                spriteId);
        }

        // Thanks to OpiF at http://otfans.net/showthread.php?t=102065
        public static Image GetSpriteImage(string file, int spriteId)
        {
            if (spriteId < 2 || spriteId > 28722)
                throw new ArgumentOutOfRangeException("spriteId");

            int size = 32;
            Bitmap bitmap = new Bitmap(size, size);
            using (FileStream fs = File.OpenRead(file))
            {
                byte[] array = new byte[4];

                fs.Seek(6 + (spriteId - 1) * 4, SeekOrigin.Begin);

                fs.Read(array, 0, 4);
                uint address = BitConverter.ToUInt32(array, 0);

                fs.Seek(address + 3, SeekOrigin.Begin);

                fs.Read(array, 0, 2);
                ushort datasize = BitConverter.ToUInt16(array, 0);

                int counter = 0;
                int read = 0;
                while (read < datasize)
                {
                    fs.Read(array, 0, 2);
                    ushort transparentPixels = BitConverter.ToUInt16(array, 0);

                    fs.Read(array, 0, 2);
                    ushort coloredPixels = BitConverter.ToUInt16(array, 0);

                    read += 4;

                    for (int i = 0; i < transparentPixels; i++)
                    {
                        bitmap.SetPixel(counter % size,
                            counter / size,
                            Color.Transparent);
                        counter++;
                    }

                    for (int i = 0; i < coloredPixels; i++)
                    {
                        fs.Read(array, 0, 3);
                        bitmap.SetPixel(counter % size,
                            counter / size,
                            Color.FromArgb(array[0], array[1], array[2]));
                        counter++;
                    }

                    read += 3 * coloredPixels;
                }

                while (counter < 1024)
                {
                    bitmap.SetPixel(counter % 32,
                        counter / 32,
                        Color.Transparent);
                    counter++;
                }
            }
            return bitmap;
        }
    }
}
