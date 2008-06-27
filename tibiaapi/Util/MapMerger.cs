using System;
using System.Collections.Generic;
using System.Text;

using System.IO;


namespace Tibia.Util
{
    /// <summary>
    /// Contains the method to merge tibia maps.
    /// </summary>
    public static class MapMerger
    {
        /*
        Map File Format
        
        The first 65536 bytes of the map file is the graphical portion of the map.
        The next 65536 bytes appears to be the map that is used for path finding.
        
        The next 4 bytes is the number of markers on the map.
        
        The markers than follow here. If there are no markers than nothing
        is beyond the marker count bytes.
        
        
        Marker Format
        
        The first byte appears to be the x position
        The second byte appears to be the map tile it is in on the x axis
        Two blank bytes
        
        The next byte appears to the y position
        The next byte appears to be the map tile it is in on the y axis
        Two blank bytes
        
        The next 4 bytes are the image id of the image
        
        The next 2 bytes are the size of the string that fallows
        The string of text for the marker
        */


        /// <summary>
        /// Merges tibia maps together. The input files are only read from.
        /// </summary>
        /// <param name="serverType">The type of server (Real or OT)</param>
        /// <param name="outputDirectory">The directory where the merged maps will go.</param>
        /// <param name="inputDirectories">A list of directories that contain the tibia maps to be merged together.</param>
        /// <returns>
        /// Returns true if successful. If it returns false the files in the output
        /// directory may be corrupted and incorrect.
        /// </returns>
        public static bool Merge(Constants.ServerType serverType, string outputDirectory, params string[] inputDirectories)
        {
            if (inputDirectories.Length < 1)
                return false;

            string mask = null;

            if (serverType == Tibia.Constants.ServerType.Real)
                mask = "1??1????.map";
            else
                mask = "0??0????.map";

            string[] files = Directory.GetFiles(inputDirectories[0], mask);

            try
            {
                foreach (string file in files)
                {
                    File.Copy(file, outputDirectory + "/" + Path.GetFileName(file), true);
                }
            }
            catch
            {
                return false;
            }

            for (int i = 1; i < inputDirectories.Length; ++i)
            {
                files = Directory.GetFiles(inputDirectories[i]);

                foreach (string file in files)
                {
                    if (!File.Exists(outputDirectory + "/" + Path.GetFileName(file)))
                    {
                        try
                        {
                            File.Copy(file, outputDirectory + "/" + Path.GetFileName(file));
                        }
                        catch
                        {
                            return false;
                        }
                    }
                    else
                    {
                        FileStream sourcefile = null;
                        FileStream inputfile = null;
                        BufferedStream sourcebuffered = null;
                        BufferedStream inputbuffered = null;

                        try
                        {
                            //Setup the streams and buffers
                            sourcefile = new FileStream(outputDirectory + "/" + Path.GetFileName(file), FileMode.Open);
                            inputfile = new FileStream(file, FileMode.Open);
                            sourcebuffered = new BufferedStream(sourcefile);
                            inputbuffered = new BufferedStream(inputfile);


                            //Read and write the graphical data
                            byte[] source = new byte[65536];
                            sourcebuffered.Read(source, 0, 65536);

                            byte[] input = new byte[65536];
                            inputbuffered.Read(input, 0, 65536);

                            Compare(source, input, 0, 65536);

                            sourcebuffered.Seek(0, SeekOrigin.Begin);
                            sourcebuffered.Write(source, 0, 65536);


                            //Read and write the pathfinding data
                            sourcebuffered.Seek(65536, SeekOrigin.Begin);
                            inputbuffered.Seek(65536, SeekOrigin.Begin);

                            sourcebuffered.Read(source, 0, 65536);
                            inputbuffered.Read(input, 0, 65536);

                            Compare(source, input, 0xfa, 65536);

                            sourcebuffered.Seek(65536, SeekOrigin.Begin);
                            sourcebuffered.Write(source, 0, 65536);


                            //Read and write the marker data
                            sourcebuffered.Seek(131072, SeekOrigin.Begin);
                            byte[] sourcemarkercountbytes = new byte[4];
                            sourcebuffered.Read(sourcemarkercountbytes, 0, 4);
                            int sourcemarkercount = BitConverter.ToInt32(sourcemarkercountbytes, 0);

                            inputbuffered.Seek(131072, SeekOrigin.Begin);
                            byte[] inputmarkercountbytes = new byte[4];
                            inputbuffered.Read(inputmarkercountbytes, 0, 4);
                            int inputmarkercount = BitConverter.ToInt32(inputmarkercountbytes, 0);

                            List<Marker> sourcemarkers = new List<Marker>();

                            for (int r = 0; r < sourcemarkercount; ++r)
                            {
                                byte[] data = new byte[12];
                                sourcebuffered.Read(data, 0, 12);

                                byte[] stringlength = new byte[2];
                                sourcebuffered.Read(stringlength, 0, 2);
                                int len = BitConverter.ToUInt16(stringlength, 0);

                                byte[] str = new byte[len];
                                sourcebuffered.Read(str, 0, len);
                                sourcebuffered.Seek(len, SeekOrigin.Current);

                                Marker marker = new Marker(data, stringlength, str);
                                sourcemarkers.Add(marker);
                            }

                            for (int r = 0; r < inputmarkercount; ++r)
                            {
                                byte[] data = new byte[12];
                                inputbuffered.Read(data, 0, 12);

                                byte[] stringlength = new byte[2];
                                inputbuffered.Read(stringlength, 0, 2);
                                int len = BitConverter.ToUInt16(stringlength, 0);

                                byte[] str = new byte[len];
                                inputbuffered.Read(str, 0, len);
                                inputbuffered.Seek(len, SeekOrigin.Current);

                                Marker marker = new Marker(data, stringlength, str);

                                //Make sure we arn't copying a marker that already exists
                                if (!sourcemarkers.Contains(marker))
                                {
                                    sourcemarkercount++;

                                    byte[] write = marker.GetBytes();
                                    sourcebuffered.SetLength(sourcebuffered.Length + write.Length);
                                    sourcebuffered.Seek(-write.Length, SeekOrigin.End);
                                    sourcebuffered.Write(write, 0, write.Length);
                                }
                            }

                            sourcebuffered.Seek(131072, SeekOrigin.Begin);
                            sourcemarkercountbytes = BitConverter.GetBytes(sourcemarkercount);
                            sourcebuffered.Write(sourcemarkercountbytes, 0, 4);
                        }
                        catch
                        {
                            return false;
                        }
                        finally
                        {
                            if (sourcebuffered != null)
                                sourcebuffered.Close();

                            if (inputbuffered != null)
                                inputbuffered.Close();

                            if (sourcefile != null)
                                sourcefile.Close();

                            if (inputfile != null)
                                inputfile.Close();
                        }
                    }
                }
            }

            return true;
        }

        private static void Compare(byte[] source, byte[] input, byte comp, int length)
        {
            for (int i = 0; i < length; ++i)
            {
                if (source[i] == comp && input[i] != comp)
                {
                    source[i] = input[i];
                }
            }
        }

        private struct Marker
        {
            public byte[] data;
            public byte[] len;
            public byte[] str;


            public Marker(byte[] data, byte[] len, byte[] str)
            {
                this.data = data;
                this.len = len;
                this.str = str;
            }

            public byte[] GetBytes()
            {
                byte[] bytes = new byte[data.Length + len.Length + str.Length];

                Array.Copy(data, bytes, data.Length);
                Array.Copy(len, 0, bytes, data.Length, len.Length);
                Array.Copy(str, 0, bytes, data.Length + len.Length, str.Length);

                return bytes;
            }

            public override bool Equals(object obj)
            {
                if (obj.GetType() != typeof(Marker))
                    return false;

                return this == (Marker)obj;
            }

            public override int GetHashCode()
            {
                return data.Length + len.Length + str.Length;
            }

            public static bool operator ==(Marker a, Marker b)
            {
                if (CompareBytes(a.data, b.data) && a.len == b.len && CompareBytes(a.str, b.str))
                    return true;

                return false;
            }

            public static bool operator !=(Marker a, Marker b)
            {
                if (!CompareBytes(a.data, b.data) && a.len != b.len && !CompareBytes(a.str, b.str))
                    return true;

                return false;
            }

            private static bool CompareBytes(byte[] data1, byte[] data2)
            {
                for (int i = 0; i < data1.Length; ++i)
                {
                    if (data1[i] != data2[i])
                        return false;
                }

                return true;
            }
        }
    }
}