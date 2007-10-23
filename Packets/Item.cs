using System;

namespace Tibia.Packets
{
    /// <summary>
    /// Create packets pertaining to items.
    /// </summary>
    public static class Item
    {
        /// <summary>
        /// Use an item (eg. eat food).
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static byte[] Use(Objects.Item item)
        {
            byte[] packet = new byte[12];
            packet[00] = 0x0A;
            packet[01] = 0x00;
            packet[02] = 0x82;

            Array.Copy(ItemLocationToBytes(item.Location), 0, packet, 3, 5);

            packet[08] = Packet.Lo(item.Id);
            packet[09] = Packet.Hi(item.Id);
            packet[10] = packet[07];
            packet[11] = 0x01;

            return packet;
        }

        /// <summary>
        /// Use an item on a tile (eg. fish, rope, pick, shovel, etc).
        /// </summary>
        /// <param name="item"></param>
        /// <param name="onTile"></param>
        /// <returns></returns>
        public static byte[] Use(Objects.Item item, Objects.Tile onTile)
        {
            byte[] packet = new byte[19];

            packet[00] = 0x11;
            packet[01] = 0x00;
            packet[02] = 0x83;

            Array.Copy(ItemLocationToBytes(item.Location), 0, packet, 3, 5);

            packet[08] = Packet.Lo(item.Id);
            packet[09] = Packet.Hi(item.Id);
            packet[10] = 0x00;
            packet[11] = Packet.Lo(onTile.Location.X);
            packet[12] = Packet.Hi(onTile.Location.X);
            packet[13] = Packet.Lo(onTile.Location.Y);
            packet[14] = Packet.Hi(onTile.Location.Y);
            packet[15] = Packet.Lo(onTile.Location.Z);
            packet[16] = 0x63;
            packet[17] = 0x00;
            packet[18] = Packet.Lo(onTile.Id);

            return packet;
        }

        /// <summary>
        /// Use an item on another item.
        /// TODO
        /// </summary>
        /// <param name="item"></param>
        /// <param name="onItem"></param>
        /// <returns></returns>
        public static byte[] Use(Objects.Item item, Objects.Item onItem)
        {
            return null;
        }

        /// <summary>
        /// Use an item on a creature (eg. use a rune on someone, drink a manafluid).
        /// </summary>
        /// <param name="item"></param>
        /// <param name="onCreature"></param>
        /// <returns></returns>
        public static byte[] Use(Objects.Item item, Objects.Creature onCreature)
        {
            byte[] packet = new byte[19];
            packet[00] = 0x11;
            packet[01] = 0x00;
            packet[02] = 0x83;

            Array.Copy(ItemLocationToBytes(item.Location), 0, packet, 3, 5);

            packet[08] = Packet.Lo(item.Id);
            packet[09] = Packet.Hi(item.Id);
            packet[10] = packet[07];

            int x = onCreature.Location.X;
            packet[11] = Packet.Lo(x);
            packet[12] = Packet.Hi(x);

            int y = onCreature.Location.Y;
            packet[13] = Packet.Lo(y);
            packet[14] = Packet.Hi(y);
            packet[15] = Convert.ToByte(onCreature.Location.Z);

            packet[16] = 0x63;
            packet[17] = 0x00;
            packet[18] = 0x01;
            return packet;
        }

        /// <summary>
        /// Use an item on a creature in the battlelist. Is the same as shooting a rune on the battlelist.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="onCreature"></param>
        /// <returns></returns>
        public static byte[] UseOnBattlelist(Objects.Item item, Objects.Creature onCreature)
        {
            byte[] packet = new byte[15];
            packet[00] = 0x0D;
            packet[01] = 0x00;
            packet[02] = 0x84;

            packet[03] = 0xFF;
            packet[04] = 0xFF;
            packet[05] = 0x00;
            packet[06] = 0x00;
            packet[07] = 0x00;

            packet[08] = Packet.Lo(item.Id);
            packet[09] = Packet.Hi(item.Id);

            packet[10] = 0x00;
            packet[11] = BitConverter.GetBytes(onCreature.Id)[0];
            packet[12] = BitConverter.GetBytes(onCreature.Id)[1];

            packet[13] = BitConverter.GetBytes(onCreature.Id)[2];
            packet[14] = BitConverter.GetBytes(onCreature.Id)[3];

            return packet;
        }

        /// <summary>
        /// Move an item to a location (eg. move a blank rune to the right hand).
        /// </summary>
        /// <param name="fromItem"></param>
        /// <param name="toLocation"></param>
        /// <returns></returns>
        public static byte[] Move(Objects.Item fromItem, Objects.ItemLocation toLocation)
        {
            return Move(fromItem, new Objects.Item(toLocation));
        }

        /// <summary>
        /// Move an item into another item (eg. put an item into a backpack).
        /// </summary>
        /// <param name="fromItem"></param>
        /// <param name="toItem"></param>
        /// <returns></returns>
        public static byte[] Move(Objects.Item fromItem, Objects.Item toItem)
        {
            if (!fromItem.Found) return null;
            
            byte[] packet = new byte[17];
            packet[00] = 0x0F;
            packet[01] = 0x00;
            packet[02] = 0x78;

            Array.Copy(ItemLocationToBytes(fromItem.Location), 0, packet, 3, 5);

            packet[08] = Packet.Lo(fromItem.Id);
            packet[09] = Packet.Hi(fromItem.Id);

            packet[10] = packet[07];

            Array.Copy(ItemLocationToBytes(toItem.Location), 0, packet, 11, 5);

            packet[16] = fromItem.Count;

            return packet;
        }

        public static byte[] ItemLocationToBytes(Objects.ItemLocation location)
        {
            byte[] bytes = new byte[5];

            switch (location.type)
            {
                case Objects.ItemLocation.ItemLocationType.CONTAINER:
                    bytes[00] = 0xFF;
                    bytes[01] = 0xFF;
                    bytes[02] = Convert.ToByte(0x40 + location.container);
                    bytes[03] = 0x00;
                    bytes[04] = location.position;
                    break;
                case Objects.ItemLocation.ItemLocationType.SLOT:
                    bytes[00] = 0xFF;
                    bytes[01] = 0xFF;
                    bytes[02] = Convert.ToByte(location.slot);
                    bytes[03] = 0x00;
                    bytes[04] = 0x00;
                    break;
                case Objects.ItemLocation.ItemLocationType.GROUND:
                    bytes[00] = Packet.Lo(location.groundLocation.X);
                    bytes[01] = Packet.Hi(location.groundLocation.X);
                    bytes[02] = Packet.Lo(location.groundLocation.Y);
                    bytes[03] = Packet.Hi(location.groundLocation.Y);
                    bytes[04] = Convert.ToByte(location.groundLocation.Z);
                    break;
            }

            return bytes;
        }
    }
}
