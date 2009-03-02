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
        protected MessageStream messageStream;
        public Objects.Client Client { get; set; }
        #endregion

        #region Contructors

        public NetworkMessage()
        {
            messageStream = new MessageStream();
            messageStream.Position = 6;
        }

        public NetworkMessage(byte[] data)
        {
            messageStream = new MessageStream(data);
            messageStream.Position = 0;
        }

        public NetworkMessage( int length)
        {
            messageStream = new MessageStream(length);
            messageStream.Position = 0;
        }

        public NetworkMessage(byte[] data, int length)
        {
            messageStream = new MessageStream(data,0,length);
            messageStream.Position = 0;
        }

        public NetworkMessage(Objects.Client client)
        {
            Client = client;
            messageStream = new MessageStream();
            messageStream.Position = 6;
        }

        public NetworkMessage(Objects.Client client, int size)
        {
            Client = client;
            messageStream = new MessageStream(size);
            messageStream.Position = 0;
        }

        public NetworkMessage(Objects.Client client, byte[] data)
        {
            Client = client;
            messageStream = new MessageStream(data);
            messageStream.Position = 0;
        }

        public NetworkMessage(Objects.Client client,byte[] data, int length)
        {
            Client = client;
            messageStream = new MessageStream(data, 0, length);
            messageStream.Position = 0;
        }

        #endregion

        #region Properties

        public int Length
        {
            get { return this.messageStream.Length; }
            set { messageStream.Length = value; }
        }

        public int Position
        {
            get { return this.messageStream.Position; }
            set { this.messageStream.Position = value; }
        }

        public byte[] Data
        {
            get
            {
                return messageStream.ToArray();
            }
        }

        public byte[] GetBuffer()
        {
            return messageStream.GetBuffer();
        }

        #endregion

        #region Xtea

        public bool XteaEncrypt()
        {
            if (Client != null)
                if (Client.IO.XteaKey == null)
                    return false;

            return XteaEncrypt(Client.IO.XteaKey);
        }

        public bool XteaEncrypt(uint[] xteaKey)
        {
            int msgSize = messageStream.Length - 6;
            int pad = msgSize % 8;

            if (pad > 0)
                msgSize += (8 - pad);

            byte[] originalMsg = new byte[msgSize];
            Array.Copy(messageStream.GetBuffer(), 6, originalMsg, 0, messageStream.Length - 6);

            uint[] msgUInt = originalMsg.ToUInt32Array();

            for (int i = 0; i < msgUInt.Length; i += 2)
                XteaEncode(ref msgUInt, i, xteaKey);


            byte[] encryptMsg = msgUInt.ToByteArray();
            messageStream.Position = 6;
            messageStream.Write(encryptMsg, 0, encryptMsg.Length);

            return true;
        }

        public bool XteaDecrypt()
        {
            return XteaDecrypt(Client.IO.XteaKey);
        }

        public bool XteaDecrypt(uint[] XteaKey)
        {
            if (messageStream.Length <= 6)
                return false;

            if ((messageStream.Length - 6) % 8 > 0)
                return false;

            byte[] encryptMsg = new byte[messageStream.Length - 6];
            Array.Copy(messageStream.GetBuffer(), 6, encryptMsg, 0, messageStream.Length - 6);

            uint[] encryptUInt = encryptMsg.ToUInt32Array();

            for (int i = 0; i < encryptUInt.Length; i += 2)
                XteaDecode(ref encryptUInt, i, XteaKey);

            byte[] decrpytMsg = encryptUInt.ToByteArray();
            int decrpytMsgLen = (int)BitConverter.ToUInt16(decrpytMsg, 0) + 2;

            messageStream.Length = decrpytMsgLen + 6;
            Array.Copy(decrpytMsg, 0, messageStream.GetBuffer(), 6, decrpytMsgLen);

            return true;
        }

        private static void XteaEncode(ref uint[] v, int index, uint[] XteaKey)
        {
            uint y = v[index];
            uint z = v[index + 1];
            uint sum = 0;
            uint delta = 0x9e3779b9;
            uint n = 32;

            while (n-- > 0)
            {
                y += (z << 4 ^ z >> 5) + z ^ sum + XteaKey[sum & 3];
                sum += delta;
                z += (y << 4 ^ y >> 5) + y ^ sum + XteaKey[sum >> 11 & 3];
            }

            v[index] = y;
            v[index + 1] = z;
        }

        private static void XteaDecode(ref uint[] v, int index, uint[] XteaKey)
        {
            uint n = 32;
            uint sum;
            uint y = v[index];
            uint z = v[index + 1];
            uint delta = 0x9e3779b9;

            sum = delta << 5;

            while (n-- > 0)
            {
                z -= (y << 4 ^ y >> 5) + y ^ sum + XteaKey[sum >> 11 & 3];
                sum -= delta;
                y -= (z << 4 ^ z >> 5) + z ^ sum + XteaKey[sum & 3];
            }

            v[index] = y;
            v[index + 1] = z;

        }

        #endregion

        #region Adler32

        public bool CheckAdler32()
        {
            byte[] data = new byte[messageStream.Length - 6];
            Array.Copy(messageStream.GetBuffer(), 6, data, 0, data.Length);

            if (AdlerChecksum(data) != GetAdler32())
                return false;

            return true;
        }

        public void InsertAdler32()
        {
            byte[] data = new byte[messageStream.Length - 6];
            Array.Copy(messageStream.GetBuffer(), 6, data, 0, data.Length);
            AddAdler32(AdlerChecksum(data));
        }

        public const uint AdlerBase = 0xFFF1;
        public const uint AdlerStart = 0x0001;
        public const uint AdlerBuff = 0x0400;

        public static uint AdlerChecksum(byte[] data)
        {
            lock ("CalculateAdlerChecksum")
            {
                if (Object.Equals(data, null))
                    return 0;

                int nSize = data.GetLength(0);

                if (nSize == 0)
                    return 0;

                uint unSum1 = AdlerStart & 0xFFFF;
                uint unSum2 = (AdlerStart >> 16) & 0xFFFF;

                for (int i = 0; i < nSize; i++)
                {
                    unSum1 = (unSum1 + data[i]) % AdlerBase;
                    unSum2 = (unSum1 + unSum2) % AdlerBase;
                }

                return (unSum2 << 16) + unSum1;
            }
        }

        #endregion

        #region Packer Header

        public void InsertPacketHeader()
        {
            AddPacketHeader((ushort)(messageStream.Length - 2));
        }

        #endregion

        #region Get

        public byte GetByte()
        {
            return messageStream.ReadByte();
        }

        public byte[] GetBytes(int count)
        {
            return messageStream.Read(count);
        }

        public string GetString()
        {
            int len = (int)GetUInt16();
            return System.Text.ASCIIEncoding.Default.GetString(GetBytes(len));
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
            return BitConverter.ToUInt32(messageStream.GetBuffer(), 2);
        }

        private ushort GetPacketHeader()
        {
            return BitConverter.ToUInt16(messageStream.GetBuffer(), 0);
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

        #region Add

        public void AddByte(byte value)
        {
            messageStream.WriteByte(value);
        }

        public void AddBytes(byte[] value)
        {
            messageStream.Write(value, 0, value.Length);
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
            int p = messageStream.Position;

            for (int i = 0; i < count; i++)
                messageStream.WriteByte(0xAC);

            messageStream.Position = p;
        }

        private void AddAdler32(uint value)
        {
            Array.Copy(BitConverter.GetBytes(value), 0, messageStream.GetBuffer(), 2, 4);
        }

        private void AddPacketHeader(ushort value)
        {
            Array.Copy(BitConverter.GetBytes(value), 0, messageStream.GetBuffer(), 0, 2);
        }

        #endregion

        #region Peek

        public byte PeekByte()
        {
            return messageStream.PeekByte();
        }

        public byte[] PeekBytes(int count)
        {
            return messageStream.Peek(count);
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

        #region Replace

        public void ReplaceBytes(int index, byte[] value)
        {
            if (messageStream.Length - index >= value.Length)
                Array.Copy(value, 0, messageStream.GetBuffer(), index, value.Length);
        }

        #endregion

        #region Other Functions

        public bool PrepareToSend()
        {
            return PrepareToSend(Client.IO.XteaKey);
        }

        public bool PrepareToSend(uint[] xteaKey)
        {
            if (!XteaEncrypt(xteaKey))
                return false;

            InsertAdler32();
            InsertPacketHeader();

            return true;
        }

        public bool PrepareToRead()
        {
            return PrepareToRead(Client.IO.XteaKey);
        }

        public bool PrepareToRead(uint[] xteaKey)
        {
            if (!XteaDecrypt(xteaKey))
                return false;

            messageStream.Position = 6;
            return true;
        }

        public void InsetLogicalPacketHeader()
        {
            byte[] data = new byte[messageStream.Length - 4];
            Array.Copy(messageStream.GetBuffer(), 6, data, 2, data.Length - 2);
            Array.Copy(BitConverter.GetBytes((ushort)(data.Length - 2)), 0, data, 0, 2);
            messageStream.Position = 6;
            messageStream.Write(data, 0, data.Length);
        }

        public void UpdateLogicalPacketHeader()
        {
            Array.Copy(BitConverter.GetBytes((ushort)(messageStream.Length - 8)), 0, messageStream.GetBuffer(), 6, 2);
        }

        #endregion

        #region RSA

        public bool RsaCipEncrypt(int start)
        {
            byte[] temp = new byte[128];
            Array.Copy(messageStream.GetBuffer(), start, temp, 0, 128);

            BigInteger input = new BigInteger(temp);
            BigInteger output = input.modPow(cipE, cipM);
            // it's sometimes possible for the results to be a byte short
            // and this can break some software so we 0x00 pad the result

            messageStream.Position = start;
            messageStream.Write(GetPaddedValue(output), 0, 128);

            return true;
        }

        public bool RsaOTEncrypt(int start)
        {
            byte[] temp = new byte[128];

            Array.Copy(messageStream.GetBuffer(), start, temp, 0, 128);

            BigInteger input = new BigInteger(temp);
            BigInteger output = input.modPow(otServerE, otServerM);
            // it's sometimes possible for the results to be a byte short
            // and this can break some software so we 0x00 pad the result

            messageStream.Position = start;
            messageStream.Write(GetPaddedValue(output), 0, 128);

            return true;
        }

        public bool RsaOTDecrypt()
        {
            if (messageStream.Length - messageStream.Position != 128)
                return false;

            byte[] temp = new byte[128];

            Array.Copy(messageStream.GetBuffer(), messageStream.Position, temp, 0, 128);

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

            Array.Copy(GetPaddedValue(output), 0, messageStream.GetBuffer(), messageStream.Position, 128);
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
