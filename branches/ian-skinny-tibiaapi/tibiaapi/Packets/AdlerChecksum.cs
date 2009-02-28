using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets
{
    public class AdlerChecksum
    {
        #region Parameters
        /// <summary>
        /// AdlerBase is Adler-32 checksum algorithm parameter.
        /// </summary>
        public const uint AdlerBase = 0xFFF1;
        /// <summary>
        /// AdlerStart is Adler-32 checksum algorithm parameter.
        /// </summary>
        public const uint AdlerStart = 0x0001;
        /// <summary>
        /// AdlerBuff is Adler-32 checksum algorithm parameter.
        /// </summary>
        public const uint AdlerBuff = 0x0400;
        /// Adler-32 checksum value
        private uint m_unChecksumValue = 0;
        #endregion

        /// <value>
        /// ChecksumValue is property which enables the user
        /// to get Adler-32 checksum value for the last calculation 
        /// </value>
        public uint ChecksumValue
        {
            get
            {
                return m_unChecksumValue;
            }
        }

        /// <summary>
        /// Calculate Adler-32 checksum for buffer
        /// </summary>
        /// <param name="bytesBuff">Bites array for checksum calculation</param>
        /// <param name="unAdlerCheckSum">Checksum start value (default=1)</param>
        /// <returns>Returns true if the checksum values is successflly calculated</returns>
        public bool MakeForBuff(byte[] bytesBuff, uint unAdlerCheckSum)
        {
            if (Object.Equals(bytesBuff, null))
            {
                m_unChecksumValue = 0;
                return false;
            }
            int nSize = bytesBuff.GetLength(0);
            if (nSize == 0)
            {
                m_unChecksumValue = 0;
                return false;
            }
            uint unSum1 = unAdlerCheckSum & 0xFFFF;
            uint unSum2 = (unAdlerCheckSum >> 16) & 0xFFFF;
            for (int i = 0; i < nSize; i++)
            {
                unSum1 = (unSum1 + bytesBuff[i]) % AdlerBase;
                unSum2 = (unSum1 + unSum2) % AdlerBase;
            }
            m_unChecksumValue = (unSum2 << 16) + unSum1;
            return true;
        }

        /// <summary>
        /// Calculate Adler-32 checksum for buffer
        /// </summary>
        /// <param name="bytesBuff">Bites array for checksum calculation</param>
        /// <returns>Returns true if the checksum values is successflly calculated</returns>
        public bool MakeForBuff(byte[] bytesBuff)
        {
            return MakeForBuff(bytesBuff, AdlerStart);
        }

        /// Equals determines whether two files (buffers) 
        /// have the same checksum value (identical).
        /// </summary>
        /// <param name="obj">A AdlerChecksum object for comparison</param>
        /// <returns>Returns true if the value of checksum is the same
        /// as this instance; otherwise, false
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (this.GetType() != obj.GetType())
                return false;
            AdlerChecksum other = (AdlerChecksum)obj;
            return (this.ChecksumValue == other.ChecksumValue);
        }

        /// <summary>
        /// operator== determines whether AdlerChecksum objects are equal.
        /// </summary>
        /// <param name="objA">A AdlerChecksum object for comparison</param>
        /// <param name="objB">A AdlerChecksum object for comparison</param>
        /// <returns>Returns true if the values of its operands are equal</returns>
        public static bool operator ==(AdlerChecksum objA, AdlerChecksum objB)
        {
            if (Object.Equals(objA, null) && Object.Equals(objB, null)) return true;
            if (Object.Equals(objA, null) || Object.Equals(objB, null)) return false;
            return objA.Equals(objB);
        }

        /// <summary>
        /// operator!= determines whether AdlerChecksum objects are not equal.
        /// </summary>
        /// <param name="objA">A AdlerChecksum object for comparison</param>
        /// <param name="objB">A AdlerChecksum object for comparison</param>
        /// <returns>Returns true if the values of its operands are not equal</returns>
        public static bool operator !=(AdlerChecksum objA, AdlerChecksum objB)
        {
            return !(objA == objB);
        }

        /// <summary>
        /// GetHashCode returns hash code for this instance.
        /// </summary>
        /// <returns>hash code of AdlerChecksum</returns>
        public override int GetHashCode()
        {
            return ChecksumValue.GetHashCode();
        }

        /// <summary>
        /// ToString is a method for current AdlerChecksum object
        /// representation in textual form.
        /// </summary>
        /// <returns>Returns current checksum or
        /// or "Unknown" if checksum value is unavailable 
        /// </returns>
        public override string ToString()
        {
            if (ChecksumValue != 0)
                return ChecksumValue.ToString();
            return "Unknown";
        }
    }
}
