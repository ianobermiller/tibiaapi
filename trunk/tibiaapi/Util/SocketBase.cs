using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Diagnostics;
using Tibia.Packets;
using Tibia.Objects;
using System.Windows.Forms;
using System.Net;


namespace Tibia.Util
{
    public abstract class SocketBase
    {
        #region Vars
        Form debugForm;
        bool debugOn;
        #endregion

        #region Events
        public event Action<string> PrintDebug;

        public delegate bool IncomingPacketListener(Packets.IncomingPacket packet);
        public delegate bool OutgoingPacketListener(Packets.OutgoingPacket packet);

        public delegate void SplitPacket(byte type, byte[] packet);
        public event SplitPacket IncomingSplitPacket;
        public event SplitPacket OutgoingSplitPacket;

        //incoming
        public event IncomingPacketListener ReceivedAnimatedTextIncomingPacket;
        public event IncomingPacketListener ReceivedCancelTargetIncomingPacket;
        public event IncomingPacketListener ReceivedCanReportBugsIncomingPacket;
        public event IncomingPacketListener ReceivedChannelClosePrivateIncomingPacket;
        public event IncomingPacketListener ReceivedChannelListIncomingPacket;
        public event IncomingPacketListener ReceivedChannelOpenIncomingPacket;
        public event IncomingPacketListener ReceivedChannelOpenPrivateIncomingPacket;
        public event IncomingPacketListener ReceivedContainerAddItemIncomingPacket;
        public event IncomingPacketListener ReceivedContainerCloseIncomingPacket;
        public event IncomingPacketListener ReceivedContainerOpenIncomingPacket;
        public event IncomingPacketListener ReceivedContainerRemoveItemIncomingPacket;
        public event IncomingPacketListener ReceivedContainerUpdateItemIncomingPacket;
        public event IncomingPacketListener ReceivedCreatureHealthIncomingPacket;
        public event IncomingPacketListener ReceivedCreatureLightIncomingPacket;
        public event IncomingPacketListener ReceivedCreatureMoveIncomingPacket;
        public event IncomingPacketListener ReceivedCreatureOutfitIncomingPacket;
        public event IncomingPacketListener ReceivedCreatureSkullIncomingPacket;
        public event IncomingPacketListener ReceivedCreatureSpeakIncomingPacket;
        public event IncomingPacketListener ReceivedCreatureSpeedIncomingPacket;
        public event IncomingPacketListener ReceivedCreatureSquareIncomingPacket;
        public event IncomingPacketListener ReceivedDeathIncomingPacket;
        public event IncomingPacketListener ReceivedFloorChangeDownIncomingPacket;
        public event IncomingPacketListener ReceivedFloorChangeUpIncomingPacket;
        public event IncomingPacketListener ReceivedFyiMessageIncomingPacket;
        public event IncomingPacketListener ReceivedInventoryResetSlotIncomingPacket;
        public event IncomingPacketListener ReceivedInventorySetSlotIncomingPacket;
        public event IncomingPacketListener ReceivedItemTextWindowIncomingPacket;
        public event IncomingPacketListener ReceivedMagicEffectIncomingPacket;
        public event IncomingPacketListener ReceivedMapDescriptionIncomingPacket;
        public event IncomingPacketListener ReceivedMoveEastIncomingPacket;
        public event IncomingPacketListener ReceivedMoveNorthIncomingPacket;
        public event IncomingPacketListener ReceivedMoveSouthIncomingPacket;
        public event IncomingPacketListener ReceivedMoveWestIncomingPacket;
        public event IncomingPacketListener ReceivedOutfitWindowIncomingPacket;
        public event IncomingPacketListener ReceivedPingIncomingPacket;
        public event IncomingPacketListener ReceivedPlayerFlagsIncomingPacket;
        public event IncomingPacketListener ReceivedPlayerSkillsIncomingPacket;
        public event IncomingPacketListener ReceivedPlayerStatusIncomingPacket;
        public event IncomingPacketListener ReceivedPlayerWalkCancelIncomingPacket;
        public event IncomingPacketListener ReceivedPrivateChannelCreateIncomingPacket;
        public event IncomingPacketListener ReceivedProjectileIncomingPacket;
        public event IncomingPacketListener ReceivedRuleViolationCancelIncomingPacket;
        public event IncomingPacketListener ReceivedRuleViolationLockIncomingPacket;
        public event IncomingPacketListener ReceivedRuleViolationOpenIncomingPacket;
        public event IncomingPacketListener ReceivedRuleViolationRemoveIncomingPacket;
        public event IncomingPacketListener ReceivedSafeTradeCloseIncomingPacket;
        public event IncomingPacketListener ReceivedSafeTradeRequestAckIncomingPacket;
        public event IncomingPacketListener ReceivedSafeTradeRequestNoAckIncomingPacket;
        public event IncomingPacketListener ReceivedSelfAppearIncomingPacket;
        public event IncomingPacketListener ReceivedShopSaleGoldCountIncomingPacket;
        public event IncomingPacketListener ReceivedShopWindowCloseIncomingPacket;
        public event IncomingPacketListener ReceivedShopWindowOpenIncomingPacket;
        public event IncomingPacketListener ReceivedTextMessageIncomingPacket;
        public event IncomingPacketListener ReceivedTileAddThingIncomingPacket;
        public event IncomingPacketListener ReceivedTileRemoveThingIncomingPacket;
        public event IncomingPacketListener ReceivedTileTransformThingIncomingPacket;
        public event IncomingPacketListener ReceivedTileUpdateIncomingPacket;
        public event IncomingPacketListener ReceivedVipLoginIncomingPacket;
        public event IncomingPacketListener ReceivedVipLogoutIncomingPacket;
        public event IncomingPacketListener ReceivedVipStateIncomingPacket;
        public event IncomingPacketListener ReceivedWaitingListIncomingPacket;
        public event IncomingPacketListener ReceivedWorldLightIncomingPacket;

        //outgoing
        public event OutgoingPacketListener ReceivedAttackOutgoingPacket;
        public event OutgoingPacketListener ReceivedAutoWalkOutgoingPacket;
        public event OutgoingPacketListener ReceivedAutoWalkCancelOutgoingPacket;
        public event OutgoingPacketListener ReceivedBattleWindowOutgoingPacket;
        public event OutgoingPacketListener ReceivedCancelMoveOutgoingPacket;
        public event OutgoingPacketListener ReceivedChannelCloseOutgoingPacket;
        public event OutgoingPacketListener ReceivedChannelOpenOutgoingPacket;
        public event OutgoingPacketListener ReceivedContainerCloseOutgoingPacket;
        public event OutgoingPacketListener ReceivedContainerOpenParentOutgoingPacket;
        public event OutgoingPacketListener ReceivedItemRotateOutgoingPacket;
        public event OutgoingPacketListener ReceivedItemUseOutgoingPacket;
        public event OutgoingPacketListener ReceivedItemUseOnOutgoingPacket;
        public event OutgoingPacketListener ReceivedItemUseBattlelistOutgoingPacket;
        public event OutgoingPacketListener ReceivedFightModesOutgoingPacket;
        public event OutgoingPacketListener ReceivedFollowOutgoingPacket;
        public event OutgoingPacketListener ReceivedLogoutOutgoingPacket;
        public event OutgoingPacketListener ReceivedLookAtOutgoingPacket;
        public event OutgoingPacketListener ReceivedMoveOutgoingPacket;
        public event OutgoingPacketListener ReceivedPingOutgoingPacket;
        public event OutgoingPacketListener ReceivedPlayerSpeechOutgoingPacket;
        public event OutgoingPacketListener ReceivedSetOutfitOutgoingPacket;
        public event OutgoingPacketListener ReceivedShopBuyOutgoingPacket;
        public event OutgoingPacketListener ReceivedShopSellOutgoingPacket;
        public event OutgoingPacketListener ReceivedTurnOutgoingPacket;
        public event OutgoingPacketListener ReceivedVipAddOutgoingPacket;
        public event OutgoingPacketListener ReceivedVipRemoveOutgoingPacket;
        #endregion

        #region Event callers
        protected void OnIncomingSplitPacket(byte type, byte[] packet)
        {
            if (IncomingSplitPacket != null)
                IncomingSplitPacket.BeginInvoke(type, packet, null, null);
        }
        protected void OnOutgoingSplitPacket(byte type, byte[] packet)
        {
            if (OutgoingSplitPacket != null)
                OutgoingSplitPacket.BeginInvoke(type, packet, null, null);
        }
        #endregion

        #region ClientPacket
        protected IncomingPacket ParseClientPacket(Client client, NetworkMessage msg, ref Objects.Location pos)
        {
            IncomingPacket packet;
            IncomingPacketType type = (IncomingPacketType)msg.PeekByte();

            switch (type)
            {
                case IncomingPacketType.AnimatedText:
                    if (DebugOn)
                        WriteDebug("Incoming: AnimatedText");
                    packet = new Packets.Incoming.AnimatedTextPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedAnimatedTextIncomingPacket != null)
                            packet.Forward = ReceivedAnimatedTextIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.ContainerClose:
                    if (DebugOn)
                        WriteDebug("Incoming: ContainerClose");
                    packet = new Packets.Incoming.ContainerClosePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedContainerCloseIncomingPacket != null)
                            packet.Forward = ReceivedContainerCloseIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.CreatureSpeech:
                    if (DebugOn)
                        WriteDebug("Incoming: CreatureSpeak");
                    packet = new Packets.Incoming.CreatureSpeechPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedCreatureSpeakIncomingPacket != null)
                            packet.Forward = ReceivedCreatureSpeakIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.ChannelOpen:
                    if (DebugOn)
                        WriteDebug("Incoming: ChannelOpen");
                    packet = new Packets.Incoming.ChannelOpenPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedChannelOpenIncomingPacket != null)
                            packet.Forward = ReceivedChannelOpenIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.PlayerWalkCancel:
                    if (DebugOn)
                        WriteDebug("Incoming: PlayerWalkCancel");
                    packet = new Packets.Incoming.PlayerWalkCancelPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedPlayerWalkCancelIncomingPacket != null)
                            packet.Forward = ReceivedPlayerWalkCancelIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.ChannelList:
                    if (DebugOn)
                        WriteDebug("Incoming: ChannelList");
                    packet = new Packets.Incoming.ChannelListPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedChannelListIncomingPacket != null)
                            packet.Forward = ReceivedChannelListIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.CreatureMove:
                    if (DebugOn)
                        WriteDebug("Incoming: CreatureMove");
                    packet = new Packets.Incoming.CreatureMovePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedCreatureMoveIncomingPacket != null)
                            packet.Forward = ReceivedCreatureMoveIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.TextMessage:
                    if (DebugOn)
                        WriteDebug("Incoming: TextMessage");
                    packet = new Packets.Incoming.TextMessagePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedTextMessageIncomingPacket != null)
                            packet.Forward = ReceivedTextMessageIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.TileAddThing:
                    if (DebugOn)
                        WriteDebug("Incoming: TileAddThing");
                    packet = new Packets.Incoming.TileAddThingPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedTileAddThingIncomingPacket != null)
                            packet.Forward = ReceivedTileAddThingIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.CreatureOutfit:
                    if (DebugOn)
                        WriteDebug("Incoming: CreatureOutfit");
                    packet = new Packets.Incoming.CreatureOutfitPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedCreatureOutfitIncomingPacket != null)
                            packet.Forward = ReceivedCreatureOutfitIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.CreatureLight:
                    if (DebugOn)
                        WriteDebug("Incoming: CreatureLight");
                    packet = new Packets.Incoming.CreatureLightPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedCreatureLightIncomingPacket != null)
                            packet.Forward = ReceivedCreatureLightIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.CreatureHealth:
                    if (DebugOn)
                        WriteDebug("Incoming: CreatureHealth");
                    packet = new Packets.Incoming.CreatureHealthPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedCreatureHealthIncomingPacket != null)
                            packet.Forward = ReceivedCreatureHealthIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.CreatureSpeed:
                    if (DebugOn)
                        WriteDebug("Incoming: CreatureSpeed");
                    packet = new Packets.Incoming.CreatureSpeedPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedCreatureSpeedIncomingPacket != null)
                            packet.Forward = ReceivedCreatureSpeedIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.CreatureSquare:
                    if (DebugOn)
                        WriteDebug("Incoming: CreatureSquare");
                    packet = new Packets.Incoming.CreatureSquarePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedCreatureSquareIncomingPacket != null)
                            packet.Forward = ReceivedCreatureSquareIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.TileTransformThing:
                    if (DebugOn)
                        WriteDebug("Incoming: TileTransformThing");
                    packet = new Packets.Incoming.TileTransformThingPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedTileTransformThingIncomingPacket != null)
                            packet.Forward = ReceivedTileTransformThingIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.TileRemoveThing:
                    if (DebugOn)
                        WriteDebug("Incoming: TileRemoveThing");
                    packet = new Packets.Incoming.TileRemoveThingPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedTileRemoveThingIncomingPacket != null)
                            packet.Forward = ReceivedTileRemoveThingIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.ContainerAddItem:
                    if (DebugOn)
                        WriteDebug("Incoming: ContainerAddItem");
                    packet = new Packets.Incoming.ContainerAddItemPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedContainerAddItemIncomingPacket != null)
                            packet.Forward = ReceivedContainerAddItemIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.ContainerRemoveItem:
                    if (DebugOn)
                        WriteDebug("Incoming: ContainerRemoveItem");
                    packet = new Packets.Incoming.ContainerRemoveItemPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedContainerRemoveItemIncomingPacket != null)
                            packet.Forward = ReceivedContainerRemoveItemIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.ContainerUpdateItem:
                    if (DebugOn)
                        WriteDebug("Incoming: ContainerUpdateItem");
                    packet = new Packets.Incoming.ContainerUpdateItemPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedContainerUpdateItemIncomingPacket != null)
                            packet.Forward = ReceivedContainerUpdateItemIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.ContainerOpen:
                    if (DebugOn)
                        WriteDebug("Incoming: ContainerOpen");
                    packet = new Packets.Incoming.ContainerOpenPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedContainerOpenIncomingPacket != null)
                            packet.Forward = ReceivedContainerOpenIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.ItemTextWindow:
                    if (DebugOn)
                        WriteDebug("Incoming: ItemTextWindow");
                    packet = new Packets.Incoming.ItemTextWindowPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedItemTextWindowIncomingPacket != null)
                            packet.Forward = ReceivedItemTextWindowIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.WorldLight:
                    if (DebugOn)
                        WriteDebug("Incoming: WorldLight");
                    packet = new Packets.Incoming.WorldLightPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedWorldLightIncomingPacket != null)
                            packet.Forward = ReceivedWorldLightIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.Projectile:
                    if (DebugOn)
                        WriteDebug("Incoming: Projectile");
                    packet = new Packets.Incoming.ProjectilePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedProjectileIncomingPacket != null)
                            packet.Forward = ReceivedProjectileIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.MapDescription:
                    if (DebugOn)
                        WriteDebug("Incoming: MapDescription");
                    packet = new Packets.Incoming.MapDescriptionPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedMapDescriptionIncomingPacket != null)
                            packet.Forward = ReceivedMapDescriptionIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.MoveNorth:
                    if (DebugOn)
                        WriteDebug("Incoming: MoveNorth");
                    packet = new Packets.Incoming.MoveNorthPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedMoveNorthIncomingPacket != null)
                            packet.Forward = ReceivedMoveNorthIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.MoveSouth:
                    if (DebugOn)
                        WriteDebug("Incoming: MoveSouth");
                    packet = new Packets.Incoming.MoveSouthPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedMoveSouthIncomingPacket != null)
                            packet.Forward = ReceivedMoveSouthIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.MoveEast:
                    if (DebugOn)
                        WriteDebug("Incoming: MoveEast");
                    packet = new Packets.Incoming.MoveEastPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedMoveEastIncomingPacket != null)
                            packet.Forward = ReceivedMoveEastIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.MoveWest:
                    if (DebugOn)
                        WriteDebug("Incoming: MoveWest");
                    packet = new Packets.Incoming.MoveWestPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedMoveWestIncomingPacket != null)
                            packet.Forward = ReceivedMoveWestIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.SelfAppear:
                    if (DebugOn)
                        WriteDebug("Incoming: SelfAppear");
                    packet = new Packets.Incoming.SelfAppearPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedSelfAppearIncomingPacket != null)
                            packet.Forward = ReceivedSelfAppearIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.MagicEffect:
                    if (DebugOn)
                        WriteDebug("Incoming: MagicEffect");
                    packet = new Packets.Incoming.MagicEffectPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedMagicEffectIncomingPacket != null)
                            packet.Forward = ReceivedMagicEffectIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.FloorChangeDown:
                    if (DebugOn)
                        WriteDebug("Incoming: FloorChangeDown");
                    packet = new Packets.Incoming.FloorChangeDownPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedFloorChangeDownIncomingPacket != null)
                            packet.Forward = ReceivedFloorChangeDownIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.FloorChangeUp:
                    if (DebugOn)
                        WriteDebug("Incoming: FloorChangeUp");
                    packet = new Packets.Incoming.FloorChangeUpPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedFloorChangeUpIncomingPacket != null)
                            packet.Forward = ReceivedFloorChangeUpIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.PlayerStatus:
                    if (DebugOn)
                        WriteDebug("Incoming: PlayerStatus");
                    packet = new Packets.Incoming.PlayerStatusPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedPlayerStatusIncomingPacket != null)
                            packet.Forward = ReceivedPlayerStatusIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.CreatureSkull:
                    if (DebugOn)
                        WriteDebug("Incoming: CreatureSkull");
                    packet = new Packets.Incoming.CreatureSkullPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedCreatureSkullIncomingPacket != null)
                            packet.Forward = ReceivedCreatureSkullIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.WaitingList:
                    if (DebugOn)
                        WriteDebug("Incoming: WaitingList");
                    packet = new Packets.Incoming.WaitingListPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedWaitingListIncomingPacket != null)
                            packet.Forward = ReceivedWaitingListIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.Ping:
                    if (DebugOn)
                        WriteDebug("Incoming: Ping");
                    packet = new Packets.Incoming.PingPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedPingIncomingPacket != null)
                            packet.Forward = ReceivedPingIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.Death:
                    if (DebugOn)
                        WriteDebug("Incoming: Death");
                    packet = new Packets.Incoming.DeathPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedDeathIncomingPacket != null)
                            packet.Forward = ReceivedDeathIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.CanReportBugs:
                    if (DebugOn)
                        WriteDebug("Incoming: CanReportBugs");
                    packet = new Packets.Incoming.CanReportBugsPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedCanReportBugsIncomingPacket != null)
                            packet.Forward = ReceivedCanReportBugsIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.TileUpdate:
                    if (DebugOn)
                        WriteDebug("Incoming: TileUpdate");
                    packet = new Packets.Incoming.TileUpdatePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedTileUpdateIncomingPacket != null)
                            packet.Forward = ReceivedTileUpdateIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.FyiMessage:
                    if (DebugOn)
                        WriteDebug("Incoming: FyiMessage");
                    packet = new Packets.Incoming.FyiMessagePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedFyiMessageIncomingPacket != null)
                            packet.Forward = ReceivedFyiMessageIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.InventorySetSlot:
                    if (DebugOn)
                        WriteDebug("Incoming: InventorySetSlot");
                    packet = new Packets.Incoming.InventorySetSlotPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedInventorySetSlotIncomingPacket != null)
                            packet.Forward = ReceivedInventorySetSlotIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.InventoryResetSlot:
                    if (DebugOn)
                        WriteDebug("Incoming: InventoryResetSlot");
                    packet = new Packets.Incoming.InventoryResetSlotPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedInventoryResetSlotIncomingPacket != null)
                            packet.Forward = ReceivedInventoryResetSlotIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.SafeTradeRequestAck:
                    if (DebugOn)
                        WriteDebug("Incoming: SafeTradeRequestAck");
                    packet = new Packets.Incoming.SafeTradeRequestAckPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedSafeTradeRequestAckIncomingPacket != null)
                            packet.Forward = ReceivedSafeTradeRequestAckIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.SafeTradeRequestNoAck:
                    if (DebugOn)
                        WriteDebug("Incoming: SafeTradeRequestNoAck");
                    packet = new Packets.Incoming.SafeTradeRequestNoAckPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedSafeTradeRequestNoAckIncomingPacket != null)
                            packet.Forward = ReceivedSafeTradeRequestNoAckIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.SafeTradeClose:
                    if (DebugOn)
                        WriteDebug("Incoming: SafeTradeClose");
                    packet = new Packets.Incoming.SafeTradeClosePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedSafeTradeCloseIncomingPacket != null)
                            packet.Forward = ReceivedSafeTradeCloseIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.PlayerSkillsUpdate:
                    if (DebugOn)
                        WriteDebug("Incoming: PlayerSkillsUpdate");
                    packet = new Packets.Incoming.PlayerSkillsPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedPlayerSkillsIncomingPacket != null)
                            packet.Forward = ReceivedPlayerSkillsIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.PlayerFlags:
                    if (DebugOn)
                        WriteDebug("Incoming: PlayerFlags");
                    packet = new Packets.Incoming.PlayerFlagsPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedPlayerFlagsIncomingPacket != null)
                            packet.Forward = ReceivedPlayerFlagsIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.ChannelOpenPrivate:
                    if (DebugOn)
                        WriteDebug("Incoming: ChannelOpenPrivate");
                    packet = new Packets.Incoming.ChannelOpenPrivatePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedChannelOpenPrivateIncomingPacket != null)
                            packet.Forward = ReceivedChannelOpenPrivateIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.PrivateChannelCreate:
                    if (DebugOn)
                        WriteDebug("Incoming: PrivateChannelCreate");
                    packet = new Packets.Incoming.PrivateChannelCreatePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedPrivateChannelCreateIncomingPacket != null)
                            packet.Forward = ReceivedPrivateChannelCreateIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.ChannelClosePrivate:
                    if (DebugOn)
                        WriteDebug("Incoming: ChannelClosePrivate");
                    packet = new Packets.Incoming.ChannelClosePrivatePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedChannelClosePrivateIncomingPacket != null)
                            packet.Forward = ReceivedChannelClosePrivateIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.VipState:
                    if (DebugOn)
                        WriteDebug("Incoming: VipState");
                    packet = new Packets.Incoming.VipStatePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedVipStateIncomingPacket != null)
                            packet.Forward = ReceivedVipStateIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.VipLogin:
                    if (DebugOn)
                        WriteDebug("Incoming: VipLogin");
                    packet = new Packets.Incoming.VipLoginPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedVipLoginIncomingPacket != null)
                            packet.Forward = ReceivedVipLoginIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.VipLogout:
                    if (DebugOn)
                        WriteDebug("Incoming: VipLogout");
                    packet = new Packets.Incoming.VipLogoutPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedVipLogoutIncomingPacket != null)
                            packet.Forward = ReceivedVipLogoutIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.ShopSaleGoldCount:
                    if (DebugOn)
                        WriteDebug("Incoming: ShopSaleGoldCount");
                    packet = new Packets.Incoming.ShopSaleGoldCountPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedShopSaleGoldCountIncomingPacket != null)
                            packet.Forward = ReceivedShopSaleGoldCountIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.ShopWindowOpen:
                    if (DebugOn)
                        WriteDebug("Incoming: ShopWindowOpen");
                    packet = new Packets.Incoming.ShopWindowOpenPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedShopWindowOpenIncomingPacket != null)
                            packet.Forward = ReceivedShopWindowOpenIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.ShopWindowClose:
                    if (DebugOn)
                        WriteDebug("Incoming: ShopWindowClose");
                    packet = new Packets.Incoming.ShopWindowClosePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedShopWindowCloseIncomingPacket != null)
                            packet.Forward = ReceivedShopWindowCloseIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.OutfitWindow:
                    if (DebugOn)
                        WriteDebug("Incoming: OutfitWindow");
                    packet = new Packets.Incoming.OutfitWindowPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedOutfitWindowIncomingPacket != null)
                            packet.Forward = ReceivedOutfitWindowIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.RuleViolationOpen:
                    if (DebugOn)
                        WriteDebug("Incoming: RuleViolationOpen");
                    packet = new Packets.Incoming.RuleViolationOpenPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedRuleViolationOpenIncomingPacket != null)
                            packet.Forward = ReceivedRuleViolationOpenIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.RuleViolationRemove:
                    if (DebugOn)
                        WriteDebug("Incoming: RuleViolationRemove");
                    packet = new Packets.Incoming.RuleViolationRemovePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedRuleViolationRemoveIncomingPacket != null)
                            packet.Forward = ReceivedRuleViolationRemoveIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.RuleViolationCancel:
                    if (DebugOn)
                        WriteDebug("Incoming: RuleViolationCancel");
                    packet = new Packets.Incoming.RuleViolationCancelPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedRuleViolationCancelIncomingPacket != null)
                            packet.Forward = ReceivedRuleViolationCancelIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.RuleViolationLock:
                    if (DebugOn)
                        WriteDebug("Incoming: RuleViolationLock");
                    packet = new Packets.Incoming.RuleViolationLockPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedRuleViolationLockIncomingPacket != null)
                            packet.Forward = ReceivedRuleViolationLockIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case IncomingPacketType.CancelTarget:
                    if (DebugOn)
                        WriteDebug("Incoming: CancelTarget");

                    packet = new Packets.Incoming.CancelTargetPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Client, ref pos))
                    {
                        if (ReceivedCancelTargetIncomingPacket != null)
                            packet.Forward = ReceivedCancelTargetIncomingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                default:
                    break;
            }

            return null;
        }
        #endregion

        #region ServerPacket
        protected OutgoingPacket ParseServerPacket(Client client, NetworkMessage msg, Location pos)
        {
            OutgoingPacket packet;
            OutgoingPacketType type = (OutgoingPacketType)msg.PeekByte();

            switch (type)
            {
                case OutgoingPacketType.ChannelClose:
                    if (DebugOn)
                        WriteDebug("Outgoing: ChannelClose");

                    packet = new Packets.Outgoing.ChannelClosePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server, ref pos))
                    {
                        if (ReceivedChannelCloseOutgoingPacket != null)
                            packet.Forward = ReceivedChannelCloseOutgoingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case OutgoingPacketType.ChannelOpen:
                    if (DebugOn)
                        WriteDebug("Outgoing: ChannelOpen");

                    packet = new Packets.Outgoing.ChannelOpenPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server, ref pos))
                    {
                        if (ReceivedChannelOpenOutgoingPacket != null)
                            packet.Forward = ReceivedChannelOpenOutgoingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case OutgoingPacketType.PlayerSpeech:
                    if (DebugOn)
                        WriteDebug("Outgoing: PlayerSpeech");

                    packet = new Packets.Outgoing.PlayerSpeechPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server, ref pos))
                    {
                        if (ReceivedPlayerSpeechOutgoingPacket != null)
                            packet.Forward = ReceivedPlayerSpeechOutgoingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case OutgoingPacketType.Attack:
                    if (DebugOn)
                        WriteDebug("Outgoing: Attack");

                    packet = new Packets.Outgoing.AttackPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server, ref pos))
                    {
                        if (ReceivedAttackOutgoingPacket != null)
                            packet.Forward = ReceivedAttackOutgoingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case OutgoingPacketType.Follow:
                    if (DebugOn)
                        WriteDebug("Outgoing: Follow");

                    packet = new Packets.Outgoing.FollowPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server, ref pos))
                    {
                        if (ReceivedFollowOutgoingPacket != null)
                            packet.Forward = ReceivedFollowOutgoingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case OutgoingPacketType.LookAt:
                    if (DebugOn)
                        WriteDebug("Outgoing: LookAt");

                    packet = new Packets.Outgoing.LookAtPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server, ref pos))
                    {
                        if (ReceivedLookAtOutgoingPacket != null)
                            packet.Forward = ReceivedLookAtOutgoingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case OutgoingPacketType.ItemUse:
                    if (DebugOn)
                        WriteDebug("Outgoing: ItemUse");

                    packet = new Packets.Outgoing.ItemUsePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server, ref pos))
                    {
                        if (ReceivedItemUseOutgoingPacket != null)
                            packet.Forward = ReceivedItemUseOutgoingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case OutgoingPacketType.ItemUseOn:
                    if (DebugOn)
                        WriteDebug("Outgoing: ItemUseOn");

                    packet = new Packets.Outgoing.ItemUseOnPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server, ref pos))
                    {
                        if (ReceivedItemUseOnOutgoingPacket != null)
                            packet.Forward = ReceivedItemUseOnOutgoingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case OutgoingPacketType.ItemMove:
                    if (DebugOn)
                        WriteDebug("Outgoing: ItemMove");

                    packet = new Packets.Outgoing.ItemMovePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server, ref pos))
                    {
                        if (ReceivedItemUseBattlelistOutgoingPacket != null)
                            packet.Forward = ReceivedItemUseBattlelistOutgoingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case OutgoingPacketType.CancelMove:
                    if (DebugOn)
                        WriteDebug("Outgoing: CancelMove");

                    packet = new Packets.Outgoing.CancelMovePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server, ref pos))
                    {
                        if (ReceivedCancelMoveOutgoingPacket != null)
                            packet.Forward = ReceivedCancelMoveOutgoingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case OutgoingPacketType.ItemUseBattlelist:
                    if (DebugOn)
                        WriteDebug("Outgoing: ItemUseBattlelist");

                    packet = new Packets.Outgoing.ItemUseBattlelistPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server, ref pos))
                    {
                        if (ReceivedBattleWindowOutgoingPacket != null)
                            packet.Forward = ReceivedBattleWindowOutgoingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case OutgoingPacketType.Logout:
                    if (DebugOn)
                        WriteDebug("Outgoing: Logout");

                    packet = new Packets.Outgoing.LogoutPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server, ref pos))
                    {
                        if (ReceivedLogoutOutgoingPacket != null)
                            packet.Forward = ReceivedLogoutOutgoingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case OutgoingPacketType.ContainerClose:
                    if (DebugOn)
                        WriteDebug("Outgoing: ContainerClose");

                    packet = new Packets.Outgoing.ContainerClosePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server, ref pos))
                    {
                        if (ReceivedContainerCloseOutgoingPacket != null)
                            packet.Forward = ReceivedContainerCloseOutgoingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case OutgoingPacketType.ContainerOpenParent:
                    if (DebugOn)
                        WriteDebug("Outgoing: ContainerOpenParent");

                    packet = new Packets.Outgoing.ContainerOpenParentPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server, ref pos))
                    {
                        if (ReceivedContainerOpenParentOutgoingPacket != null)
                            packet.Forward = ReceivedContainerOpenParentOutgoingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case OutgoingPacketType.ShopBuy:
                    if (DebugOn)
                        WriteDebug("Outgoing: ShopBuy");

                    packet = new Packets.Outgoing.ShopBuyPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server, ref pos))
                    {
                        if (ReceivedShopBuyOutgoingPacket != null)
                            packet.Forward = ReceivedShopBuyOutgoingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case OutgoingPacketType.ShopSell:
                    if (DebugOn)
                        WriteDebug("Outgoing: ShopSell");

                    packet = new Packets.Outgoing.ShopSellPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server, ref pos))
                    {
                        if (ReceivedShopSellOutgoingPacket != null)
                            packet.Forward = ReceivedShopSellOutgoingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case OutgoingPacketType.TurnDown:
                    if (DebugOn)
                        WriteDebug("Outgoing: TurnDown");

                    msg.GetByte();
                    packet = new Packets.Outgoing.TurnPacket(client, Tibia.Constants.TurnDirection.Down);

                    if (ReceivedTurnOutgoingPacket != null)
                        packet.Forward = ReceivedTurnOutgoingPacket.Invoke(packet);

                    return packet;
                case OutgoingPacketType.TurnUp:
                    if (DebugOn)
                        WriteDebug("Outgoing: TurnUp");

                    msg.GetByte();
                    packet = new Packets.Outgoing.TurnPacket(client, Tibia.Constants.TurnDirection.Up);

                    if (ReceivedTurnOutgoingPacket != null)
                        packet.Forward = ReceivedTurnOutgoingPacket.Invoke(packet);

                    return packet;
                case OutgoingPacketType.TurnLeft:
                    if (DebugOn)
                        WriteDebug("Outgoing: TurnLeft");

                    msg.GetByte();
                    packet = new Packets.Outgoing.TurnPacket(client, Tibia.Constants.TurnDirection.Left);

                    if (ReceivedTurnOutgoingPacket != null)
                        packet.Forward = ReceivedTurnOutgoingPacket.Invoke(packet);

                    return packet;
                case OutgoingPacketType.TurnRight:
                    if (DebugOn)
                        WriteDebug("Outgoing: TurnRight");

                    msg.GetByte();
                    packet = new Packets.Outgoing.TurnPacket(client, Tibia.Constants.TurnDirection.Right);

                    if (ReceivedTurnOutgoingPacket != null)
                        packet.Forward = ReceivedTurnOutgoingPacket.Invoke(packet);

                    return packet;
                case OutgoingPacketType.MoveDown:
                    if (DebugOn)
                        WriteDebug("Outgoing: MoveDown");

                    msg.GetByte();
                    packet = new Packets.Outgoing.MovePacket(client, Tibia.Constants.WalkDirection.Down);

                    if (ReceivedMoveOutgoingPacket != null)
                        packet.Forward = ReceivedMoveOutgoingPacket.Invoke(packet);

                    return packet;
                case OutgoingPacketType.MoveDownLeft:
                    if (DebugOn)
                        WriteDebug("Outgoing: MoveDownLeft");

                    msg.GetByte();
                    packet = new Packets.Outgoing.MovePacket(client, Tibia.Constants.WalkDirection.DownLeft);

                    if (ReceivedMoveOutgoingPacket != null)
                        packet.Forward = ReceivedMoveOutgoingPacket.Invoke(packet);

                    return packet;
                case OutgoingPacketType.MoveDownRight:
                    if (DebugOn)
                        WriteDebug("Outgoing: MoveDownRight");

                    msg.GetByte();
                    packet = new Packets.Outgoing.MovePacket(client, Tibia.Constants.WalkDirection.DownRight);

                    if (ReceivedMoveOutgoingPacket != null)
                        packet.Forward = ReceivedMoveOutgoingPacket.Invoke(packet);

                    return packet;
                case OutgoingPacketType.MoveLeft:
                    if (DebugOn)
                        WriteDebug("Outgoing: MoveLeft");

                    msg.GetByte();
                    packet = new Packets.Outgoing.MovePacket(client, Tibia.Constants.WalkDirection.Left);

                    if (ReceivedMoveOutgoingPacket != null)
                        packet.Forward = ReceivedMoveOutgoingPacket.Invoke(packet);

                    return packet;
                case OutgoingPacketType.MoveRight:
                    if (DebugOn)
                        WriteDebug("Outgoing: MoveRight");

                    msg.GetByte();
                    packet = new Packets.Outgoing.MovePacket(client, Tibia.Constants.WalkDirection.Right);

                    if (ReceivedMoveOutgoingPacket != null)
                        packet.Forward = ReceivedMoveOutgoingPacket.Invoke(packet);

                    return packet;
                case OutgoingPacketType.MoveUp:
                    if (DebugOn)
                        WriteDebug("Outgoing: MoveUp");

                    msg.GetByte();
                    packet = new Packets.Outgoing.MovePacket(client, Tibia.Constants.WalkDirection.Up);

                    if (ReceivedMoveOutgoingPacket != null)
                        packet.Forward = ReceivedMoveOutgoingPacket.Invoke(packet);

                    return packet;
                case OutgoingPacketType.MoveUpLeft:
                    if (DebugOn)
                        WriteDebug("Outgoing: MoveUpLeft");

                    msg.GetByte();
                    packet = new Packets.Outgoing.MovePacket(client, Tibia.Constants.WalkDirection.UpLeft);

                    if (ReceivedMoveOutgoingPacket != null)
                        packet.Forward = ReceivedMoveOutgoingPacket.Invoke(packet);

                    return packet;
                case OutgoingPacketType.MoveUpRight:
                    if (DebugOn)
                        WriteDebug("Outgoing: MoveUpRight");

                    msg.GetByte();
                    packet = new Packets.Outgoing.MovePacket(client, Tibia.Constants.WalkDirection.UpRight);

                    if (ReceivedMoveOutgoingPacket != null)
                        packet.Forward = ReceivedMoveOutgoingPacket.Invoke(packet);

                    return packet;
                case OutgoingPacketType.AutoWalk:
                    if (DebugOn)
                        WriteDebug("Outgoing: AutoWalk");

                    packet = new Packets.Outgoing.AutoWalkPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server, ref pos))
                    {
                        if (ReceivedAutoWalkOutgoingPacket != null)
                            packet.Forward = ReceivedAutoWalkOutgoingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case OutgoingPacketType.VipAdd:
                    if (DebugOn)
                        WriteDebug("Outgoing: VipAdd");

                    packet = new Packets.Outgoing.VipAddPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server, ref pos))
                    {
                        if (ReceivedVipAddOutgoingPacket != null)
                            packet.Forward = ReceivedVipAddOutgoingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case OutgoingPacketType.VipRemove:
                    if (DebugOn)
                        WriteDebug("Outgoing: VipRemove");

                    packet = new Packets.Outgoing.VipRemovePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server, ref pos))
                    {
                        if (ReceivedVipRemoveOutgoingPacket != null)
                            packet.Forward = ReceivedVipRemoveOutgoingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case OutgoingPacketType.ItemRotate:
                    if (DebugOn)
                        WriteDebug("Outgoing: ItemRotate");

                    packet = new Packets.Outgoing.ItemRotatePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server, ref pos))
                    {
                        if (ReceivedItemRotateOutgoingPacket != null)
                            packet.Forward = ReceivedItemRotateOutgoingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case OutgoingPacketType.SetOutfit:
                    if (DebugOn)
                        WriteDebug("Outgoing: SetOutfit");

                    packet = new Packets.Outgoing.SetOutfitPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server, ref pos))
                    {
                        if (ReceivedSetOutfitOutgoingPacket != null)
                            packet.Forward = ReceivedSetOutfitOutgoingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case OutgoingPacketType.AutoWalkCancel:
                    if (DebugOn)
                        WriteDebug("Outgoing: AutoWalkCancel");

                    packet = new Packets.Outgoing.AutoWalkCancelPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server, ref pos))
                    {
                        if (ReceivedAutoWalkCancelOutgoingPacket != null)
                            packet.Forward = ReceivedAutoWalkCancelOutgoingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case OutgoingPacketType.Ping:
                    if (DebugOn)
                        WriteDebug("Outgoing: Ping");

                    packet = new Packets.Outgoing.PingPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server, ref pos))
                    {
                        if (ReceivedPingOutgoingPacket != null)
                            packet.Forward = ReceivedPingOutgoingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                case OutgoingPacketType.FightModes:
                    if (DebugOn)
                        WriteDebug("Outgoing: FightModes");

                    packet = new Packets.Outgoing.FightModesPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server, ref pos))
                    {
                        if (ReceivedFightModesOutgoingPacket != null)
                            packet.Forward = ReceivedFightModesOutgoingPacket.Invoke(packet);

                        return packet;
                    }
                    break;
                default:
                    break;
            }

            return null;
        }
        #endregion

        #region Debug

        protected void WriteDebug(string message)
        {
            if (PrintDebug != null)
                PrintDebug.BeginInvoke(message, null, null);
        }

        protected void StartDebugForm()
        {
            debugForm = new Form();
            RichTextBox myRichTextBox = new RichTextBox();
            myRichTextBox.Name = "richTextBox";
            myRichTextBox.Dock = DockStyle.Fill;
            debugForm.Controls.Add(myRichTextBox);
            debugForm.Disposed += new EventHandler(debugFrom_Disposed);
            PrintDebug += Proxy_PrintDebug;
            debugForm.Show();
        }

        void Proxy_PrintDebug(string message)
        {
            if (debugForm.Disposing)
                return;

            if (debugForm.InvokeRequired)
            {
                debugForm.Invoke(new Action<string>(Proxy_PrintDebug), new object[] { message });
                return;
            }

            RichTextBox myRichTextBox = (RichTextBox)debugForm.Controls["richTextBox"];
            myRichTextBox.AppendText("[" + DateTime.Now.ToLongTimeString() + "] " + message + "\n");
            myRichTextBox.Select(myRichTextBox.TextLength - 1, 0);
            myRichTextBox.ScrollToCaret();
        }

        void debugFrom_Disposed(object sender, EventArgs e)
        {
            PrintDebug -= Proxy_PrintDebug;
            debugForm = null;
        }

        public bool DebugOn
        {
            get { return debugOn; }
            set
            {
                if (value && debugForm == null)
                    StartDebugForm();
                else if (!value && debugForm != null)
                    debugForm.Close();

                debugOn = value;
            }
        }
        #endregion

        #region Port Checking
        /// <summary>
        /// Check if a port is open on localhost
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public static bool CheckPort(ushort port)
        {
            try
            {
                TcpListener tcpScan = new TcpListener(IPAddress.Any, port);
                tcpScan.Start();
                tcpScan.Stop();

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Get the first free port on localhost starting at the default 7171
        /// </summary>
        /// <returns></returns>
        public static ushort GetFreePort()
        {
            return GetFreePort(7171);
        }

        /// <summary>
        /// Get the first free port on localhost beginning at start
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        public static ushort GetFreePort(ushort start)
        {
            while (!CheckPort(start))
            {
                start++;
            }

            return start;
        }
        #endregion

        #region Misc
        public static string GetDefaultLocalIp()
        {
            string localIp = null;
            IPHostEntry hostEntry = Dns.GetHostEntry((Dns.GetHostName()));
            foreach (IPAddress ipa in hostEntry.AddressList)
            {
                // Find the first IPv4 address
                if (ipa.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIp = ipa.ToString();
                    break;
                }
            }
            return localIp;
        }
        #endregion
    }
}