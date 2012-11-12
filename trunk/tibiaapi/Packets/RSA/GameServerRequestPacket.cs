using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.RSA
{
    public class GameServerRequestPacket
    {

        public static NetworkMessage Create(byte OS, ushort Version,
          string AccountName, string CharacterName, string Password)
        {
            byte[] XteaKey = new byte[16];
            new Random().NextBytes(XteaKey);
            return Create(OS, Version, XteaKey, AccountName, CharacterName, Password);
        }

        public static NetworkMessage Create(byte OS, ushort Version,
         byte[] XteaKey, string AccountName, string CharacterName, string Password)
        {
            return Create(OS, Version, XteaKey, AccountName, CharacterName, Password, false);
        }


        public static NetworkMessage Create(byte OS, ushort Version,
         byte[] XteaKey, string AccountName,string CharacterName, string Password, bool OpenTibia)
        {
            NetworkMessage msg = new NetworkMessage(139);
            msg.AddByte(0x89);
            msg.AddByte(0x00);
            msg.Position += 4;
            msg.AddByte(0x0A);
            msg.AddUInt16(OS);
            msg.AddUInt16(Version);
            msg.AddByte(0x0);
            msg.AddBytes(XteaKey);
            msg.AddByte(0x0);
            msg.AddString(AccountName);
            msg.AddString(CharacterName);
            msg.AddString(Password);
            if (OpenTibia) msg.RsaOTEncrypt(11);
            else msg.RsaCipEncrypt(11);
            msg.AddAdler32();
            return msg;
        }
    }
}
