using System;

namespace Tibia.Packets
{
    /// <summary>
    /// Create packets pertaining to attacking.
    /// </summary>
    public static class Attack
    {
        /// <summary>
        /// Attack a creature by its id.
        /// </summary>
        /// <param name="creatureId"></param>
        /// <returns></returns>
        public static byte[] AttackCreature(int creatureId)
        {
            byte[] packet = new byte[7];
            packet[0] = 0x05;
            packet[1] = 0x00;
            packet[2] = 0xA1;

            byte[] idBytes = BitConverter.GetBytes(creatureId);
            Array.Copy(idBytes, 0, packet, 3, idBytes.Length);
            return packet;
        }

        /// <summary>
        /// Attack a creature.
        /// </summary>
        /// <param name="creature"></param>
        /// <returns></returns>
        public static byte[] AttackCreature(Objects.Creature creature)
        {
            return AttackCreature(creature.Id);
        }

        /// <summary>
        /// Attack a creature by id and set the client's red square.
        /// </summary>
        /// <param name="creatureId"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        public static byte[] AttackCreature(int creatureId, Objects.Client client)
        {
            client.WriteInt(Memory.Addresses.Player.Target_ID, creatureId);
            return AttackCreature(creatureId);
        }

        /// <summary>
        /// Attack a creature and set the client's red square
        /// </summary>
        /// <param name="creature"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        public static byte[] AttackCreature(Objects.Creature creature, Objects.Client client)
        {
            return AttackCreature(creature.Id, client);
        }

        /// <summary>
        /// Set the client attack mode (sends a packet and changes the client memory).
        /// TODO
        /// </summary>
        /// <param name="attackMode"></param>
        /// <returns></returns>
        public static byte[] SetAttackMode(Memory.Addresses.Client.Attack attackMode)
        {
            return null;
        }

        /// <summary>
        /// Set the client follow mode (sends a packet and changes the client memory).
        /// TODO
        /// </summary>
        /// <param name="followMode"></param>
        /// <returns></returns>
        public static byte[] SetFollowMode(Memory.Addresses.Client.Follow followMode)
        {
            return null;
        }
    }
}
