using System;
using System.Text;
using Tibia.Util;

namespace Tibia.Packets
{
    public class NetworkMessage
    {
        #region Constants
        static BigInteger otServerP = new BigInteger("14299623962416399520070177382898895550795403345466153217470516082934737582776038882967213386204600674145392845853859217990626450972452084065728686565928113", 10);
        static BigInteger otServerQ = new BigInteger("7630979195970404721891201847792002125535401292779123937207447574596692788513647179235335529307251350570728407373705564708871762033017096809910315212884101", 10);
        static BigInteger otServerD = new BigInteger("46730330223584118622160180015036832148732986808519344675210555262940258739805766860224610646919605860206328024326703361630109888417839241959507572247284807035235569619173792292786907845791904955103601652822519121908367187885509270025388641700821735345222087940578381210879116823013776808975766851829020659073", 10);
        static BigInteger otServerM = new BigInteger("109120132967399429278860960508995541528237502902798129123468757937266291492576446330739696001110603907230888610072655818825358503429057592827629436413108566029093628212635953836686562675849720620786279431090218017681061521755056710823876476444260558147179707119674283982419152118103759076030616683978566631413", 10);
        static BigInteger otServerE = new BigInteger("65537", 10);
        static BigInteger otServerDP = new BigInteger("11141736698610418925078406669215087697114858422461871124661098818361832856659225315773346115219673296375487744032858798960485665997181641221483584094519937", 10);
        static BigInteger otServerDQ = new BigInteger("4886309137722172729208909250386672706991365415741885286554321031904881408516947737562153523770981322408725111241551398797744838697461929408240938369297973", 10);
        static BigInteger otServerInverseQ = new BigInteger("5610960212328996596431206032772162188356793727360507633581722789998709372832546447914318965787194031968482458122348411654607397146261039733584248408719418", 10);
        static BigInteger cipM = new BigInteger("124710459426827943004376449897985582167801707960697037164044904862948569380850421396904597686953877022394604239428185498284169068581802277612081027966724336319448537811441719076484340922854929273517308661370727105382899118999403808045846444647284499123164879035103627004668521005328367415259939915284902061793", 10);
        static BigInteger cipE = new BigInteger("65537", 10);
        #endregion

        #region Instance Variables
        private byte[] _buffer;
        private int _position, _length, _bufferSize = 16394;
        public Objects.Client Client { get; set; }
        #endregion

        #region Contructors

        public NetworkMessage()
        {
            _buffer = new byte[_bufferSize];
            _position = 8;
        }

        public NetworkMessage(byte[] data)
        {
            _buffer = new byte[_bufferSize];
            Array.Copy(data, _buffer, data.Length);
            _length = data.Length;
            _position = 0;
        }

        public NetworkMessage(int length)
        {
            _bufferSize = length;
            _buffer = new byte[_bufferSize];
            _position = 8;
        }

        public NetworkMessage(byte[] data, int length)
        {
            _buffer = new byte[_bufferSize];
            Array.Copy(data, _buffer, length);
            _length = length;
            _position = 0;
        }

        public NetworkMessage(Objects.Client client)
        {
            _buffer = new byte[_bufferSize];
            Client = client;
            _position = 8;
        }

        public NetworkMessage(Objects.Client client, int size)
        {
            _bufferSize = size;
            _buffer = new byte[_bufferSize];
            Client = client;
            _position = 8;
        }

        public NetworkMessage(Objects.Client client, byte[] data)
        {
            _buffer = new byte[_bufferSize];
            Client = client;
            Array.Copy(data, _buffer, data.Length);
            _length = data.Length;
            _position = 0;
        }

        public NetworkMessage(Objects.Client client, byte[] data, int length)
        {
            _buffer = new byte[_bufferSize];
            Client = client;
            Array.Copy(data, _buffer, length);
            _length = length;
            _position = 0;
        }

        #endregion

        #region "Properties"

        public int Length
        {
            get { return _length; }
            set { _length = value; }
        }

        public int Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public byte[] GetBuffer()
        {
            return _buffer;
        }

        public byte[] Data
        {
            get
            {
                byte[] _t = new byte[_length];
                Array.Copy(_buffer, _t, _length);
                return _t;
            }
        }

        #endregion

        #region "Xtea"

        public bool XteaEncrypt()
        {
            return XteaEncrypt(Client.IO.XteaKey);
        }

        public bool XteaEncrypt(uint[] XteaKey)
        {
            if (XteaKey == null)
                return false;

            int msgSize = _length - 6;

            int pad = msgSize % 8;
            if (pad > 0)
            {
                msgSize += (8 - pad);
                _length = 6 + msgSize;
            }

            for (int i = 6; i < _length; i += 8)
                XTEAEncrypt(_buffer, i, 8, XteaKey);

            _position = 6;
            return true;
        }

        public bool XteaDecrypt()
        {
            return XteaDecrypt(Client.IO.XteaKey);
        }

        public bool XteaDecrypt(uint[] XteaKey)
        {
            if (_length <= 6 || (_length - 6) % 8 > 0 || XteaKey == null)
                return false;

            for (int i = 6; i < _length; i += 8)
                XTEADecrypt(_buffer, i, 8, XteaKey);

            int decrpytMsgLen = (int)BitConverter.ToUInt16(_buffer, 6) + 2;
            _length = decrpytMsgLen + 6;

            return true;
        }

        private void XTEAEncrypt(byte[] p_data, int p_offset, int p_count, uint[] o_key)
        {
            // defintions that are used to create the cipher text
            uint x_sum = 0, x_delta = 0x9e3779b9, x_count = 32;
            // convert the plaintext data into 32 bit words
            uint[] x_words = ConvertBytesToUints(p_data, p_offset, p_count);
            // main part of the XTEA Cipher
            while (x_count-- > 0)
            {
                x_words[0] += (x_words[1] << 4 ^ x_words[1] >> 5) + x_words[1] ^ x_sum
                    + o_key[x_sum & 3];
                x_sum += x_delta;
                x_words[1] += (x_words[0] << 4 ^ x_words[0] >> 5) + x_words[0] ^ x_sum
                    + o_key[x_sum >> 11 & 3];
            }

            Array.Copy(ConvertUintstoBytes(x_words), 0, p_data, p_offset, p_count);
        }

        private void XTEADecrypt(byte[] p_data, int p_offset, int p_count, uint[] o_key)
        {
            // defintions that are used to restore the plaintext text
            uint x_count = 32, x_sum = 0xC6EF3720, x_delta = 0x9E3779B9;
            // convert the data into 32 bit words
            uint[] x_words = ConvertBytesToUints(p_data, p_offset, p_count);

            // main part of the XTEA Cipher
            while (x_count-- > 0)
            {
                x_words[1] -= (x_words[0] << 4 ^ x_words[0] >> 5) + x_words[0] ^ x_sum
                    + o_key[x_sum >> 11 & 3];
                x_sum -= x_delta;
                x_words[0] -= (x_words[1] << 4 ^ x_words[1] >> 5) + x_words[1] ^ x_sum
                    + o_key[x_sum & 3];
            }

            // convert the unit[] into a byte[]
            Array.Copy(ConvertUintstoBytes(x_words), 0, p_data, p_offset, p_count);
        }

        private uint[] ConvertBytesToUints(byte[] p_data, int p_offset, int p_count)
        {
            // allocate an array - each uint requires 4 bytes
            uint[] x_result = new uint[p_count / 4];
            // run through the data and create the unsigned ints from
            // the array of bytes
            for (int i = p_offset, j = 0; i < p_offset + p_count; i += 4, j++)
            {
                x_result[j] = BitConverter.ToUInt32(p_data, i);
            }
            // return the array of 32-bit unsigned ints
            return x_result;
        }

        private byte[] ConvertUintstoBytes(uint[] p_data)
        {
            // convert the unit[] into a byte[]
            byte[] x_result = new byte[p_data.Length * 4];
            // run through the data and create the bytes from
            // the array of unsigned ints
            for (int i = 0, j = 0; i < p_data.Length; i++, j += 4)
            {
                byte[] x_interim = BitConverter.GetBytes(p_data[i]);
                Array.Copy(x_interim, 0, x_result, j, x_interim.Length);
            }
            // return the array of 8-bit bytes
            return x_result;
        }

        #endregion

        #region "Adler32"

        public bool CheckAdler32()
        {
            if (AdlerChecksum(_buffer, 6) != GetAdler32())
                return false;

            return true;
        }

        public void InsertAdler32()
        {
            Array.Copy(BitConverter.GetBytes(AdlerChecksum(_buffer, 6)), 0, _buffer, 2, 4);
        }

        public const uint AdlerBase = 0xFFF1;
        public const uint AdlerStart = 0x0001;
        public const uint AdlerBuff = 0x0400;

        public uint AdlerChecksum(byte[] data, int offset)
        {
            if (data == null || _length - 6 <= 0)
                return 0;

            uint unSum1 = AdlerStart & 0xFFFF;
            uint unSum2 = (AdlerStart >> 16) & 0xFFFF;

            for (int i = offset; i < _length; i++)
            {
                unSum1 = (unSum1 + data[i]) % AdlerBase;
                unSum2 = (unSum1 + unSum2) % AdlerBase;
            }

            return (unSum2 << 16) + unSum1;
        }

        #endregion

        #region "Packer Header"

        public void InsertPacketHeader()
        {
            AddPacketHeader((ushort)(_length - 2));
        }

        #endregion

        #region "Get"

        public byte GetByte()
        {
            if (_position + 1 > _length)
                throw new Exception("NetworkMessage try to get more bytes from a smaller buffer");

            return _buffer[_position++];
        }

        public byte[] GetBytes(int count)
        {
            if (_position + count > _length)
                throw new Exception("NetworkMessage try to get more bytes from a smaller buffer");

            byte[] _t = new byte[count];
            Array.Copy(_buffer, _position, _t, 0, count);
            _position += count;
            return _t;
        }

        public string GetString()
        {
            int len = (int)GetUInt16();
            string t = System.Text.ASCIIEncoding.Default.GetString(_buffer, _position, len);
            _position += len;
            return t;
        }

        public ushort GetUInt16()
        {
            return BitConverter.ToUInt16(GetBytes(2), 0);
        }

        public uint GetUInt32()
        {
            return BitConverter.ToUInt32(GetBytes(4), 0);
        }

        public Objects.Location GetLocation()
        {
            int x = (int)GetUInt16();
            int y = (int)GetUInt16();
            int z = (int)GetByte();

            return new Objects.Location(x, y, z);
        }

        private uint GetAdler32()
        {
            return BitConverter.ToUInt32(_buffer, 2);
        }

        private ushort GetPacketHeader()
        {
            return BitConverter.ToUInt16(_buffer, 0);
        }

        public Objects.Outfit GetOutfit()
        {
            byte head, body, legs, feet, addons;
            ushort looktype = GetUInt16();

            if (looktype != 0)
            {
                head = GetByte();
                body = GetByte();
                legs = GetByte();
                feet = GetByte();
                addons = GetByte();

                return new Objects.Outfit(looktype, head, body, legs, feet, addons);
            }
            else
                return new Tibia.Objects.Outfit(looktype, GetUInt16());
        }

        #endregion

        #region "Add"

        public void AddByte(byte value)
        {
            if (1 + _length > _bufferSize)
                throw new Exception("NetworkMessage try to add more bytes to a smaller buffer");

            AddBytes(new byte[] { value });
        }

        public void AddBytes(byte[] value)
        {
            if (value.Length + _length > _bufferSize)
                throw new Exception("NetworkMessage try to add more bytes to a smaller buffer");

            Array.Copy(value, 0, _buffer, _position, value.Length);
            _position += value.Length;

            if (_position > _length)
                _length = _position;
        }

        public void AddString(string value)
        {
            AddUInt16((ushort)value.Length);
            AddBytes(System.Text.ASCIIEncoding.Default.GetBytes(value));
        }

        public void AddUInt16(ushort value)
        {
            AddBytes(BitConverter.GetBytes(value));
        }

        public void AddUInt32(uint value)
        {
            AddBytes(BitConverter.GetBytes(value));
        }

        public void AddLocation(Objects.Location pos)
        {
            AddBytes(pos.ToBytes());
        }

        public void AddOutfit(Objects.Outfit outfit)
        {
            AddBytes(outfit.ToByteArray());
        }

        public void AddPaddingBytes(int count)
        {
            _position += count;

            if (_position > _length)
                _length = _position;
        }

        private void AddPacketHeader(ushort value)
        {
            Array.Copy(BitConverter.GetBytes(value), 0, _buffer, 0, 2);
        }

        #endregion

        #region "Peek"

        public byte PeekByte()
        {
            return _buffer[_position];
        }

        public byte[] PeekBytes(int count)
        {
            byte[] _t = new byte[count];
            Array.Copy(_buffer, _position, _t, 0, count);
            return _t;
        }

        public ushort PeekUInt16()
        {
            return BitConverter.ToUInt16(PeekBytes(2), 0);
        }

        public uint PeekUInt32()
        {
            return BitConverter.ToUInt32(PeekBytes(4), 0);
        }

        public string PeekString()
        {
            int len = (int)PeekUInt16();
            return System.Text.ASCIIEncoding.ASCII.GetString(PeekBytes(len + 2), 2, len);
        }

        #endregion

        #region "Replace"

        public void ReplaceBytes(int index, byte[] value)
        {
            if (_length - index >= value.Length)
                Array.Copy(value, 0, _buffer, index, value.Length);
        }

        #endregion

        #region "Other Functions"

        public void Reset()
        {
            _position = 8;
            _length = 8;
        }

        public bool PrepareToSend()
        {
            return PrepareToSend(Client.IO.XteaKey);
        }

        public bool PrepareToSend(uint[] XteaKey)
        {
            if (!XteaEncrypt(XteaKey))
                return false;

            InsertAdler32();
            InsertPacketHeader();

            return true;
        }

        public bool PrepareToRead()
        {
            return PrepareToRead(Client.IO.XteaKey);
        }

        public bool PrepareToRead(uint[] XteaKey)
        {
            if (!XteaDecrypt(XteaKey))
                return false;

            _position = 6;
            return true;
        }

        public void InsetLogicalPacketHeader()
        {
            Array.Copy(BitConverter.GetBytes((ushort)_length - 8), 0, _buffer, 6, 2);
        }

        public void UpdateLogicalPacketHeader()
        {
            Array.Copy(BitConverter.GetBytes((ushort)(_length - 8)), 0, _buffer, 6, 2);
        }

        #endregion

        #region "RSA"

        public bool RsaCipEncrypt(int start)
        {
            byte[] temp = new byte[128];
            Array.Copy(_buffer, start, temp, 0, 128);

            BigInteger input = new BigInteger(temp);
            BigInteger output = input.modPow(cipE, cipM);
            // it's sometimes possible for the results to be a byte short
            // and this can break some software so we 0x00 pad the result

            _position = start;
            Array.Copy(GetPaddedValue(output), 0, _buffer, start, 128);

            return true;
        }

        public bool RsaOTEncrypt(int start)
        {
            byte[] temp = new byte[128];

            Array.Copy(_buffer, start, temp, 0, 128);

            BigInteger input = new BigInteger(temp);
            BigInteger output = input.modPow(otServerE, otServerM);
            // it's sometimes possible for the results to be a byte short
            // and this can break some software so we 0x00 pad the result

            Array.Copy(GetPaddedValue(output), 0, _buffer, start, 128);

            return true;
        }

        public bool RsaOTDecrypt()
        {
            if (_length - _position != 128)
                return false;

            byte[] temp = new byte[128];

            Array.Copy(_buffer, _position, temp, 0, 128);

            BigInteger input = new BigInteger(temp);
            BigInteger output;

            BigInteger m1 = input.modPow(otServerDP, otServerP);
            // m2 = c^dq mod q
            BigInteger m2 = input.modPow(otServerDQ, otServerQ);
            BigInteger h;

            if (m2 > m1)
            {
                // thanks to benm!
                h = otServerP - ((m2 - m1) * otServerInverseQ % otServerP);
                output = m2 + otServerQ * h;
            }
            else
            {
                // h = (m1 - m2) * qInv mod p
                h = (m1 - m2) * otServerInverseQ % otServerP;
                // m = m2 + q * h;
                output = m2 + otServerQ * h;
            }

            Array.Copy(GetPaddedValue(output), 0, _buffer, _position, 128);
            return true;
        }

        private static byte[] GetPaddedValue(BigInteger value)
        {
            byte[] result = value.getBytes();

            int length = (1024 >> 3);
            if (result.Length >= length)
                return result;

            // left-pad 0x00 value on the result (same integer, correct length)
            byte[] padded = new byte[length];
            System.Buffer.BlockCopy(result, 0, padded, (length - result.Length), result.Length);
            // temporary result may contain decrypted (plaintext) data, clear it
            Array.Clear(result, 0, result.Length);
            return padded;
        }
        #endregion
    }
}
