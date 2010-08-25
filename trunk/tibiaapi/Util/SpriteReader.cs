using System;
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
        // and to Thomac at http://otfans.net/showthread.php?t=141982
        public static Image GetSpriteImage(string file, int spriteId)
        {
            if (spriteId < 2)// || spriteId > 28722)
                throw new ArgumentOutOfRangeException("spriteId");

            int size = 32;
            Bitmap bitmap = new Bitmap(size, size);

            using (BinaryReader reader = new BinaryReader(File.OpenRead(file)))
            {
                ushort currentPixel = 0;
                long targetOffset;

                reader.BaseStream.Seek(6 + (spriteId - 1) * 4, SeekOrigin.Begin);
                reader.BaseStream.Seek(reader.ReadUInt32() + 3, SeekOrigin.Begin);

                targetOffset = reader.BaseStream.Position + reader.ReadUInt16();

                while (reader.BaseStream.Position < targetOffset)
                {
                    ushort transparentPixels = reader.ReadUInt16();
                    ushort coloredPixels = reader.ReadUInt16();
                    currentPixel += transparentPixels;
                    for (int i = 0; i < coloredPixels; i++)
                    {
                        bitmap.SetPixel(
                            currentPixel % size,
                            currentPixel / size,
                            Color.FromArgb(reader.ReadByte(), reader.ReadByte(), reader.ReadByte())
                        );
                        currentPixel++;
                    }
                }
            }

            return bitmap;
        }
    }
}
