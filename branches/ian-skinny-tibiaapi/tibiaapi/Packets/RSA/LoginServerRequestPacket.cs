using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.RSA
{
    public static class LoginServerRequestPacket
    {
        public static NetworkMessage Create(ushort Version,
    byte[] Signatures, string AccountName, string Password)
        {
            return Create(0x02, Version, Signatures, AccountName, Password);
        }


        public static NetworkMessage Create(byte OS, ushort Version,
            byte[] Signatures, string AccountName, string Password)
        {
            byte[] XteaKey=new byte[16];
            new Random().NextBytes(XteaKey);
            return Create(OS, Version, Signatures, XteaKey, AccountName, Password, false);
        }
        
        public static NetworkMessage Create(byte OS, ushort Version,
            byte[] Signatures, byte[] XteaKey, string AccountName, string Password)
        {
            return Create(OS, Version, Signatures, XteaKey, AccountName, Password, false);
        }

        public static NetworkMessage Create(byte OS, ushort Version,
            byte[] Signatures, byte[] XteaKey, string AccountName, string Password,bool OpenTibia)
        {
            NetworkMessage msg = new NetworkMessage(149);
            msg.AddByte(0x95);
            msg.AddByte(0x00);
            msg.Position += 4;
            msg.AddByte(0x01);
            msg.AddUInt16(OS);
            msg.AddUInt16(Version);
            msg.AddBytes(Signatures);
            msg.AddByte(0x0);
            msg.AddBytes(XteaKey);
            msg.AddString(AccountName);
            msg.AddString(Password);
            if (OpenTibia) msg.RsaOTEncrypt(23);
            else msg.RsaCipEncrypt(23);
            msg.InsertAdler32();
            return msg;
        }
        
             


    }
}
