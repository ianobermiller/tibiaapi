using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Tibia.Objects;

namespace Tibia
{
    public static class Extensions
    {
        #region Itemlists
        public static T Find<T>(this Dictionary<uint, T> dict, Predicate<T> match) where T : Item
        {
            foreach (T t in dict.Values)
            {
                if (match(t)) return t;
            }
            return null;
        }

        public static List<T> FindAll<T>(this Dictionary<uint, T> dict, Predicate<T> match) where T : Item
        {
            List<T> list = new List<T>();
            foreach (T t in dict.Values)
            {
                if (match(t)) list.Add(t);
            }
            return list;
        }
        #endregion

        #region Packets

        public static byte[] ToByteArray(this uint[] unsignedIntegers)
        {
            byte[] temp = new byte[unsignedIntegers.Length * 4];

            for (int i = 0; i < unsignedIntegers.Length; i++)
                Array.Copy(BitConverter.GetBytes(unsignedIntegers[i]), 0, temp, i * 4, 4);

            return temp;
        }

        public static uint[] ToUInt32Array(this byte[] bytes)
        {
            if (bytes.Length % 4 > 0)
                throw new Exception();

            uint[] temp = new uint[bytes.Length / 4];

            for (int i = 0; i < temp.Length; i++)
                temp[i] = BitConverter.ToUInt32(bytes, i * 4);

            return temp;
        }

        /// <summary>
        /// Convert an array of byte to a IP String (exemple: 127.0.0.1)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToIPString(this byte[] value)
        {
            string ret = "";

            for (int i = 0; i < value.Length; i++)
                ret += value[i] + ".";

            return ret.TrimEnd('.');
        }

        /// <summary>
        /// Convert an array of byte to a printable string.
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string ToPrintableString(this byte[] bytes, int start, int length)
        {
            string text = string.Empty;
            for (int i = start; i < start + length; i++)
            {
                text += bytes[i].ToPrintableChar();
            }
            return text;
        }

        /// <summary>
        /// Convert a byte to a printable
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static char ToPrintableChar(this byte value)
        {
            if (value < 32 || value > 126)
            {
                return (char)'.';
            }
            else
            {
                return (char)value;
            }
        }

        /// <summary>
        /// Converts a char to a byte
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte ToByte(this char value)
        {
            return (byte)value;
        }

        /// <summary>
        /// Converts a string to a byte array
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] ToByteArray(this string s)
        {
            List<byte> value = new List<byte>();
            foreach (char c in s.ToCharArray())
                value.Add(c.ToByte());
            return value.ToArray();
        }

        /// <summary>Convert a string of hex digits (ex: E4 CA B2) to a byte array.</summary>
        /// <param name="s">The string containing the hex digits (with or without spaces).</param>
        /// <returns>Returns an array of bytes.</returns>
        public static byte[] ToBytesAsHex(this string s)
        {
            s = s.Replace(" ", string.Empty);
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            return buffer;
        }

        /// <summary>
        /// Convert a string of hex digits to a printable string of characters.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToPrintableStringAsHex(this string value)
        {
            byte[] temp = value.ToBytesAsHex();
            string loc = "";
            for (int i = 0; i < temp.Length; i++)
            {
                loc += temp[i].ToPrintableChar();
            }
            return loc;
        }

        /// <summary>
        /// Converts a integer to a hex string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToHexString(this int value)
        {
            byte[] temp = BitConverter.GetBytes(value);
            return temp.ToHexString(0, temp.Length);
        }

        /// <summary>
        /// Converts a hex string to a integer
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToIntAsHex(this string value)
        {
            byte[] bytes = value.ToBytesAsHex();
            if (bytes.Length >= 2)
                return BitConverter.ToInt16(bytes, 0);
            else
                return int.MinValue;
        }

        /// <summary>Converts an array of bytes into a formatted string of hex digits (ex: E4 CA B2)</summary>
        /// <param name="data">The array of bytes to be translated into a string of hex digits.</param>
        /// <param name="length">The length of data to convert</param>
        /// <returns>Returns a well formatted string of hex digits with spacing.</returns>
        public static string ToHexString(this byte[] data, int start, int length)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            for (int i = start; i < start + length; i++)
                sb.Append(Convert.ToString(data[i], 16).PadLeft(2, '0').PadRight(3, ' '));
            return sb.ToString().ToUpper();
        }

        /// <summary>Converts an array of bytes into a formatted string of hex digits (ex: E4 CA B2)</summary>
        /// <param name="data">The array of bytes to be translated into a string of hex digits.</param>
        /// <returns>Returns a well formatted string of hex digits with spacing.</returns>
        public static string ToHexString(this byte[] data)
        {
            return data.ToHexString(0, data.Length);
        }
        #endregion

        #region Map

        public static Objects.MapSquare GetPlayerMapSquare(this List<Objects.MapSquare> squares, Objects.Client client)
        {
            int playerId = client.ReadInt32(Addresses.Player.Id);

            return squares.Find(delegate(Objects.MapSquare square)
            {
                return square.Objects.Find(delegate(Objects.MapObject obj)
                {
                    return obj.Id == 0x63 && obj.Data == playerId;
                }) != null;
            });
        }

        public static Objects.MapSquare GetCreatureMapSquare(this List<Objects.MapSquare> squares, int creatureId)
        {
            return squares.Find(delegate(Objects.MapSquare square)
            {
                return square.Objects.Find(delegate(Objects.MapObject obj)
                {
                    return obj.Data == creatureId;
                }) != null;
            });
        }

        #endregion

        #region General
        public static string ToStringDeep(this object obj)
        {
            StringBuilder s = new StringBuilder();
            if (obj != null)
            {
                Type type = obj.GetType();
                s.AppendLine(type.FullName + " = ");
                s.AppendLine("{");
                List<Type> interfaces = new List<Type>(type.GetInterfaces());
                if (interfaces.Contains(typeof(IEnumerable)))
                {
                    s.AppendLine("\tValues = ");
                    s.AppendLine("\t{");
                    foreach(object val in (IEnumerable)obj)
                    {
                        s.AppendLine("\t\t" + val.ToString());
                    }
                    s.AppendLine("\t}");
                }
                foreach (FieldInfo fi in type.GetFields())
                {
                    object val;
                    try
                    {
                        val = fi.GetValue(obj);
                    }
                    catch
                    {
                        val = null;
                    }
                    s.AppendLine("\t" + fi.Name + " = " +
                        (val == null ? "null" : val.ToString()));
                }
                foreach (PropertyInfo pi in type.GetProperties())
                {
                    if (pi.CanRead)
                    {
                        object val;
                        try
                        {
                            val = pi.GetValue(obj, null);
                        }
                        catch
                        {
                            val = null;
                        }
                        s.AppendLine("\t" + pi.Name + " = " +
                            (val == null ? "null" : val.ToString()));
                    }
                }
                s.AppendLine("}");
            }
            else
            {
                s.AppendLine("null");
            }

            return s.ToString();
        }
        #endregion



    }
}
