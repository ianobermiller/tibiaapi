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

            switch (item.Location.type)
            {
                case Objects.ItemLocation.ItemLocationType.CONTAINER:
                    packet[03] = 0xFF;
                    packet[04] = 0xFF;
                    packet[05] = Convert.ToByte(0x40 + item.Location.container);
                    packet[06] = 0x00;
                    packet[07] = item.Location.position;
                    break;
                case Objects.ItemLocation.ItemLocationType.SLOT:
                    packet[03] = 0xFF;
                    packet[04] = 0xFF;
                    packet[05] = Convert.ToByte(item.Location.slot);
                    packet[06] = 0x00;
                    packet[07] = 0x00;
                    break;
                case Objects.ItemLocation.ItemLocationType.GROUND:
                    packet[03] = Packet.Lo(item.Location.groundLocation.X);
                    packet[04] = Packet.Hi(item.Location.groundLocation.X);
                    packet[05] = Packet.Lo(item.Location.groundLocation.Y);
                    packet[06] = Packet.Hi(item.Location.groundLocation.Y);
                    packet[07] = Convert.ToByte(item.Location.groundLocation.Z);
                    break;
            }

            packet[08] = Packet.Lo(item.Id);
            packet[09] = Packet.Hi(item.Id);
            packet[10] = packet[07];
            packet[11] = 0x01;

            return packet;
        }

        /// <summary>
        /// Use an item on a tile (eg. fish, rope, pick, shovel, etc).
        /// TODO
        /// </summary>
        /// <param name="item"></param>
        /// <param name="onTile"></param>
        /// <returns></returns>
        public static byte[] Use(Objects.Item item, Objects.Tile onTile)
        {
            return null;
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

            switch (item.Location.type)
            {
                case Objects.ItemLocation.ItemLocationType.CONTAINER:
                    packet[03] = 0xFF;
                    packet[04] = 0xFF;
                    packet[05] = Convert.ToByte(0x40 + item.Location.container);
                    packet[06] = 0x00;
                    packet[07] = item.Location.position;
                    break;
                case Objects.ItemLocation.ItemLocationType.SLOT:
                    packet[03] = 0xFF;
                    packet[04] = 0xFF;
                    packet[05] = Convert.ToByte(item.Location.slot);
                    packet[06] = 0x00;
                    packet[07] = 0x00;
                    break;
                case Objects.ItemLocation.ItemLocationType.GROUND:
                    packet[03] = Packet.Lo(item.Location.groundLocation.X);
                    packet[04] = Packet.Hi(item.Location.groundLocation.X);
                    packet[05] = Packet.Lo(item.Location.groundLocation.Y);
                    packet[06] = Packet.Hi(item.Location.groundLocation.Y);
                    packet[07] = Convert.ToByte(item.Location.groundLocation.Z);
                    break;
            }

            packet[08] = Packet.Lo(item.Id);
            packet[09] = Packet.Hi(item.Id);
            packet[10] = packet[07];

            packet[11] = Packet.Lo(onCreature.Location.X); // X LOW
            packet[12] = Packet.Hi(onCreature.Location.X); // X HIGH
            packet[13] = Packet.Lo(onCreature.Location.Y); // Y LOW
            packet[14] = Packet.Hi(onCreature.Location.Y); // Y HIGH
            packet[15] = Convert.ToByte(onCreature.Location.Z); // Z

            packet[16] = 0x63;
            packet[17] = 0x00;
            packet[18] = 0x01;
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

            switch (fromItem.Location.type)
            {
                case Objects.ItemLocation.ItemLocationType.CONTAINER:
                    packet[03] = 0xFF;
                    packet[04] = 0xFF;
                    packet[05] = Convert.ToByte(0x40 + fromItem.Location.container);
                    packet[06] = 0x00;
                    packet[07] = fromItem.Location.position;
                    break;
                case Objects.ItemLocation.ItemLocationType.SLOT:
                    packet[03] = 0xFF;
                    packet[04] = 0xFF;
                    packet[05] = Convert.ToByte(fromItem.Location.slot);
                    packet[06] = 0x00;
                    packet[07] = 0x00;
                    break;
                case Objects.ItemLocation.ItemLocationType.GROUND:
                    packet[03] = Packet.Lo(fromItem.Location.groundLocation.X);
                    packet[04] = Packet.Hi(fromItem.Location.groundLocation.X);
                    packet[05] = Packet.Lo(fromItem.Location.groundLocation.Y);
                    packet[06] = Packet.Hi(fromItem.Location.groundLocation.Y);
                    packet[07] = Convert.ToByte(fromItem.Location.groundLocation.Z);
                    break;
            }

            packet[08] = Packet.Lo(fromItem.Id);
            packet[09] = Packet.Hi(fromItem.Id);

            packet[10] = packet[07];

            switch (toItem.Location.type)
            {
                case Objects.ItemLocation.ItemLocationType.CONTAINER:
                    packet[11] = 0xFF;
                    packet[12] = 0xFF;
                    packet[13] = Convert.ToByte(0x40 + toItem.Location.container);
                    packet[14] = 0x00;
                    packet[15] = toItem.Location.position;
                    break;
                case Objects.ItemLocation.ItemLocationType.SLOT:
                    packet[11] = 0xFF;
                    packet[12] = 0xFF;
                    packet[13] = Convert.ToByte(toItem.Location.slot);
                    packet[14] = 0x00;
                    packet[15] = 0x00;
                    break;
                case Objects.ItemLocation.ItemLocationType.GROUND:
                    packet[11] = Packet.Lo(toItem.Location.groundLocation.X);
                    packet[12] = Packet.Hi(toItem.Location.groundLocation.X);
                    packet[13] = Packet.Lo(toItem.Location.groundLocation.Y);
                    packet[14] = Packet.Hi(toItem.Location.groundLocation.Y);
                    packet[15] = Convert.ToByte(toItem.Location.groundLocation.Z);
                    break;
            }

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
