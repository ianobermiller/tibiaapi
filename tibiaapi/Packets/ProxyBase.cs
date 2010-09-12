using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Linq;
using Tibia.Constants;
using Tibia.Objects;
using Tibia.Util;

namespace Tibia.Packets
{
    public abstract class ProxyBase
    {
        #region Events

        public delegate void DataListener(byte[] data);
        public event DataListener ReceivedDataFromClient;
        public event DataListener ReceivedDataFromServer;

        protected void OnReceivedDataFromClient(byte[] data)
        {
            if (ReceivedDataFromClient != null)
                ReceivedDataFromClient.Invoke(data);
        }

        protected void OnReceivedDataFromServer(byte[] data)
        {
            if (ReceivedDataFromServer != null)
                ReceivedDataFromServer.Invoke(data);
        }

        public delegate void SplitPacket(byte type, byte[] data);

        public event SplitPacket SplitPacketFromServer;
        public event SplitPacket SplitPacketFromClient;

        protected void OnSplitPacketFromServer(byte type, byte[] data)
        {
            if (SplitPacketFromServer != null)
                SplitPacketFromServer.Invoke(type, data);
        }

        protected void OnSplitPacketFromClient(byte type, byte[] data)
        {
            if (SplitPacketFromClient != null)
                SplitPacketFromClient.Invoke(type, data);
        }

        public delegate bool IncomingPacketListener(Packets.IncomingPacket packet);
        public delegate bool OutgoingPacketListener(Packets.OutgoingPacket packet);

        // incoming
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
        public event IncomingPacketListener ReceivedCreatureSpeechIncomingPacket;
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
        public virtual event IncomingPacketListener ReceivedSelfAppearIncomingPacket;
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

        // outgoing
        public event OutgoingPacketListener ReceivedAttackOutgoingPacket;
        public event OutgoingPacketListener ReceivedAutoWalkOutgoingPacket;
        public event OutgoingPacketListener ReceivedAutoWalkCancelOutgoingPacket;
        public event OutgoingPacketListener ReceivedItemUseBattlelistOutgoingPacket;
        public event OutgoingPacketListener ReceivedCancelMoveOutgoingPacket;
        public event OutgoingPacketListener ReceivedChannelCloseOutgoingPacket;
        public event OutgoingPacketListener ReceivedChannelListOutgoingPacket;
        public event OutgoingPacketListener ReceivedChannelOpenOutgoingPacket;
        public event OutgoingPacketListener ReceivedContainerCloseOutgoingPacket;
        public event OutgoingPacketListener ReceivedContainerOpenParentOutgoingPacket;
        public event OutgoingPacketListener ReceivedItemRotateOutgoingPacket;
        public event OutgoingPacketListener ReceivedItemUseOutgoingPacket;
        public event OutgoingPacketListener ReceivedItemUseOnOutgoingPacket;
        public event OutgoingPacketListener ReceivedItemMoveOutgoingPacket;
        public event OutgoingPacketListener ReceivedFightModesOutgoingPacket;
        public event OutgoingPacketListener ReceivedFollowOutgoingPacket;
        public event OutgoingPacketListener ReceivedLogoutOutgoingPacket;
        public event OutgoingPacketListener ReceivedLookAtOutgoingPacket;
        public event OutgoingPacketListener ReceivedMoveOutgoingPacket;
        public event OutgoingPacketListener ReceivedNpcChannelCloseOutgoingPacket;
        public event OutgoingPacketListener ReceivedPingOutgoingPacket;
        public event OutgoingPacketListener ReceivedPlayerSpeechOutgoingPacket;
        public event OutgoingPacketListener ReceivedPrivateChannelOpenOutgoingPacket;
        public event OutgoingPacketListener ReceivedSetOutfitOutgoingPacket;
        public event OutgoingPacketListener ReceivedShopBuyOutgoingPacket;
        public event OutgoingPacketListener ReceivedShopCloseOutgoingPacket;
        public event OutgoingPacketListener ReceivedShopSellOutgoingPacket;
        public event OutgoingPacketListener ReceivedTitleUpdateOutgoingPacket;
        public event OutgoingPacketListener ReceivedTurnOutgoingPacket;
        public event OutgoingPacketListener ReceivedVipAddOutgoingPacket;
        public event OutgoingPacketListener ReceivedVipRemoveOutgoingPacket;
        #endregion

        #region ServerPacket
        public bool ParseServerPacket(Client client, byte[] packet)
        {
            NetworkMessage inMsg = new NetworkMessage(client, packet);
            NetworkMessage outMsg = new NetworkMessage(client);
            while (inMsg.Position < packet.Length)
            {
                if (!ParsePacketFromServer(client, inMsg, outMsg))
                    return false;
                outMsg.Reset();
            }
            return true;
        }

        protected bool ParsePacketFromServer(Client client, NetworkMessage msg, NetworkMessage outMsg)
        {
            bool packetKnown = true;
            IncomingPacket packet = null;
            IncomingPacketType type = (IncomingPacketType)msg.PeekByte();

            switch (type)
            {
                case IncomingPacketType.AnimatedText:
                    packet = new Packets.Incoming.AnimatedTextPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedAnimatedTextIncomingPacket != null)
                            packet.Forward = ReceivedAnimatedTextIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case IncomingPacketType.ContainerClose:
                    packet = new Packets.Incoming.ContainerClosePacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedContainerCloseIncomingPacket != null)
                            packet.Forward = ReceivedContainerCloseIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case IncomingPacketType.CreatureSpeech:
                    packet = new Packets.Incoming.CreatureSpeechPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedCreatureSpeechIncomingPacket != null)
                            packet.Forward = ReceivedCreatureSpeechIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case IncomingPacketType.ChannelOpen:
                    packet = new Packets.Incoming.ChannelOpenPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedChannelOpenIncomingPacket != null)
                            packet.Forward = ReceivedChannelOpenIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case IncomingPacketType.PlayerWalkCancel:
                    packet = new Packets.Incoming.PlayerWalkCancelPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedPlayerWalkCancelIncomingPacket != null)
                            packet.Forward = ReceivedPlayerWalkCancelIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case IncomingPacketType.ChannelList:
                    packet = new Packets.Incoming.ChannelListPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedChannelListIncomingPacket != null)
                            packet.Forward = ReceivedChannelListIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case IncomingPacketType.CreatureMove:
                    packet = new Packets.Incoming.CreatureMovePacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedCreatureMoveIncomingPacket != null)
                            packet.Forward = ReceivedCreatureMoveIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case IncomingPacketType.TextMessage:
                    packet = new Packets.Incoming.TextMessagePacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedTextMessageIncomingPacket != null)
                            packet.Forward = ReceivedTextMessageIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case IncomingPacketType.TileAddThing:
                    packet = new Packets.Incoming.TileAddThingPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedTileAddThingIncomingPacket != null)
                            packet.Forward = ReceivedTileAddThingIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case IncomingPacketType.CreatureOutfit:
                    packet = new Packets.Incoming.CreatureOutfitPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedCreatureOutfitIncomingPacket != null)
                            packet.Forward = ReceivedCreatureOutfitIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case IncomingPacketType.CreatureLight:
                    packet = new Packets.Incoming.CreatureLightPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedCreatureLightIncomingPacket != null)
                            packet.Forward = ReceivedCreatureLightIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case IncomingPacketType.CreatureHealth:
                    packet = new Packets.Incoming.CreatureHealthPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedCreatureHealthIncomingPacket != null)
                            packet.Forward = ReceivedCreatureHealthIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case IncomingPacketType.CreatureSpeed:
                    packet = new Packets.Incoming.CreatureSpeedPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedCreatureSpeedIncomingPacket != null)
                            packet.Forward = ReceivedCreatureSpeedIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);

                        return true;
                    }
                    break;
                case IncomingPacketType.CreatureSquare:
                    packet = new Packets.Incoming.CreatureSquarePacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedCreatureSquareIncomingPacket != null)
                            packet.Forward = ReceivedCreatureSquareIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case IncomingPacketType.TileTransformThing:
                    packet = new Packets.Incoming.TileTransformThingPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedTileTransformThingIncomingPacket != null)
                            packet.Forward = ReceivedTileTransformThingIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);

                        return true;
                    }
                    break;
                case IncomingPacketType.TileRemoveThing:
                    packet = new Packets.Incoming.TileRemoveThingPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedTileRemoveThingIncomingPacket != null)
                            packet.Forward = ReceivedTileRemoveThingIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case IncomingPacketType.ContainerAddItem:
                    packet = new Packets.Incoming.ContainerAddItemPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedContainerAddItemIncomingPacket != null)
                            packet.Forward = ReceivedContainerAddItemIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case IncomingPacketType.ContainerRemoveItem:
                    packet = new Packets.Incoming.ContainerRemoveItemPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedContainerRemoveItemIncomingPacket != null)
                            packet.Forward = ReceivedContainerRemoveItemIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case IncomingPacketType.ContainerUpdateItem:
                    packet = new Packets.Incoming.ContainerUpdateItemPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedContainerUpdateItemIncomingPacket != null)
                            packet.Forward = ReceivedContainerUpdateItemIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case IncomingPacketType.ContainerOpen:
                    packet = new Packets.Incoming.ContainerOpenPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedContainerOpenIncomingPacket != null)
                            packet.Forward = ReceivedContainerOpenIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case IncomingPacketType.ItemTextWindow:
                    packet = new Packets.Incoming.ItemTextWindowPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedItemTextWindowIncomingPacket != null)
                            packet.Forward = ReceivedItemTextWindowIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case IncomingPacketType.WorldLight:
                    packet = new Packets.Incoming.WorldLightPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedWorldLightIncomingPacket != null)
                            packet.Forward = ReceivedWorldLightIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case IncomingPacketType.Projectile:
                    packet = new Packets.Incoming.ProjectilePacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedProjectileIncomingPacket != null)
                            packet.Forward = ReceivedProjectileIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case IncomingPacketType.MapDescription:
                    packet = new Packets.Incoming.MapDescriptionPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client, outMsg))
                    {
                        if (ReceivedMapDescriptionIncomingPacket != null)
                            packet.Forward = ReceivedMapDescriptionIncomingPacket.Invoke(packet);
                    }
                    break;
                case IncomingPacketType.MoveNorth:
                    packet = new Packets.Incoming.MoveNorthPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client, outMsg))
                    {
                        if (ReceivedMoveNorthIncomingPacket != null)
                            packet.Forward = ReceivedMoveNorthIncomingPacket.Invoke(packet);
                    }
                    break;
                case IncomingPacketType.MoveSouth:
                    packet = new Packets.Incoming.MoveSouthPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client, outMsg))
                    {
                        if (ReceivedMoveSouthIncomingPacket != null)
                            packet.Forward = ReceivedMoveSouthIncomingPacket.Invoke(packet);
                    }
                    break;
                case IncomingPacketType.MoveEast:
                    packet = new Packets.Incoming.MoveEastPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client, outMsg))
                    {
                        if (ReceivedMoveEastIncomingPacket != null)
                            packet.Forward = ReceivedMoveEastIncomingPacket.Invoke(packet);
                    }
                    break;
                case IncomingPacketType.MoveWest:
                    packet = new Packets.Incoming.MoveWestPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client, outMsg))
                    {
                        if (ReceivedMoveWestIncomingPacket != null)
                            packet.Forward = ReceivedMoveWestIncomingPacket.Invoke(packet);
                    }
                    break;
                case IncomingPacketType.SelfAppear:
                    packet = new Packets.Incoming.SelfAppearPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedSelfAppearIncomingPacket != null)
                            packet.Forward = ReceivedSelfAppearIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case IncomingPacketType.MagicEffect:
                    packet = new Packets.Incoming.MagicEffectPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedMagicEffectIncomingPacket != null)
                            packet.Forward = ReceivedMagicEffectIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case IncomingPacketType.FloorChangeDown:
                    packet = new Packets.Incoming.FloorChangeDownPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client, outMsg))
                    {
                        if (ReceivedFloorChangeDownIncomingPacket != null)
                            packet.Forward = ReceivedFloorChangeDownIncomingPacket.Invoke(packet);
                    }
                    break;
                case IncomingPacketType.FloorChangeUp:
                    packet = new Packets.Incoming.FloorChangeUpPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client, outMsg))
                    {
                        if (ReceivedFloorChangeUpIncomingPacket != null)
                            packet.Forward = ReceivedFloorChangeUpIncomingPacket.Invoke(packet);
                    }
                    break;
                case IncomingPacketType.PlayerStatus:
                    packet = new Packets.Incoming.PlayerStatusPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedPlayerStatusIncomingPacket != null)
                            packet.Forward = ReceivedPlayerStatusIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case IncomingPacketType.CreatureSkull:
                    packet = new Packets.Incoming.CreatureSkullPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedCreatureSkullIncomingPacket != null)
                            packet.Forward = ReceivedCreatureSkullIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case IncomingPacketType.WaitingList:
                    packet = new Packets.Incoming.WaitingListPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedWaitingListIncomingPacket != null)
                            packet.Forward = ReceivedWaitingListIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case IncomingPacketType.Ping:
                    packet = new Packets.Incoming.PingPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedPingIncomingPacket != null)
                            packet.Forward = ReceivedPingIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case IncomingPacketType.Death:
                    packet = new Packets.Incoming.DeathPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedDeathIncomingPacket != null)
                            packet.Forward = ReceivedDeathIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case IncomingPacketType.CanReportBugs:
                    packet = new Packets.Incoming.CanReportBugsPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedCanReportBugsIncomingPacket != null)
                            packet.Forward = ReceivedCanReportBugsIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case IncomingPacketType.TileUpdate:
                    packet = new Packets.Incoming.TileUpdatePacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client, outMsg))
                    {
                        if (ReceivedTileUpdateIncomingPacket != null)
                            packet.Forward = ReceivedTileUpdateIncomingPacket.Invoke(packet);
                    }
                    break;
                case IncomingPacketType.FyiMessage:
                    packet = new Packets.Incoming.FyiMessagePacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedFyiMessageIncomingPacket != null)
                            packet.Forward = ReceivedFyiMessageIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case IncomingPacketType.InventorySetSlot:
                    packet = new Packets.Incoming.InventorySetSlotPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedInventorySetSlotIncomingPacket != null)
                            packet.Forward = ReceivedInventorySetSlotIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case IncomingPacketType.InventoryResetSlot:
                    packet = new Packets.Incoming.InventoryResetSlotPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedInventoryResetSlotIncomingPacket != null)
                            packet.Forward = ReceivedInventoryResetSlotIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case IncomingPacketType.SafeTradeRequestAck:
                    packet = new Packets.Incoming.SafeTradeRequestAckPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedSafeTradeRequestAckIncomingPacket != null)
                            packet.Forward = ReceivedSafeTradeRequestAckIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case IncomingPacketType.SafeTradeRequestNoAck:
                    packet = new Packets.Incoming.SafeTradeRequestNoAckPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedSafeTradeRequestNoAckIncomingPacket != null)
                            packet.Forward = ReceivedSafeTradeRequestNoAckIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case IncomingPacketType.SafeTradeClose:
                    packet = new Packets.Incoming.SafeTradeClosePacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedSafeTradeCloseIncomingPacket != null)
                            packet.Forward = ReceivedSafeTradeCloseIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case IncomingPacketType.PlayerSkillsUpdate:
                    packet = new Packets.Incoming.PlayerSkillsPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedPlayerSkillsIncomingPacket != null)
                            packet.Forward = ReceivedPlayerSkillsIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case IncomingPacketType.PlayerFlags:
                    packet = new Packets.Incoming.PlayerFlagsPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedPlayerFlagsIncomingPacket != null)
                            packet.Forward = ReceivedPlayerFlagsIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case IncomingPacketType.ChannelOpenPrivate:
                    packet = new Packets.Incoming.ChannelOpenPrivatePacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedChannelOpenPrivateIncomingPacket != null)
                            packet.Forward = ReceivedChannelOpenPrivateIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case IncomingPacketType.PrivateChannelCreate:
                    packet = new Packets.Incoming.PrivateChannelCreatePacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedPrivateChannelCreateIncomingPacket != null)
                            packet.Forward = ReceivedPrivateChannelCreateIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case IncomingPacketType.ChannelClosePrivate:
                    packet = new Packets.Incoming.ChannelClosePrivatePacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedChannelClosePrivateIncomingPacket != null)
                            packet.Forward = ReceivedChannelClosePrivateIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case IncomingPacketType.VipState:
                    packet = new Packets.Incoming.VipStatePacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedVipStateIncomingPacket != null)
                            packet.Forward = ReceivedVipStateIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case IncomingPacketType.VipLogin:
                    packet = new Packets.Incoming.VipLoginPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedVipLoginIncomingPacket != null)
                            packet.Forward = ReceivedVipLoginIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case IncomingPacketType.VipLogout:
                    packet = new Packets.Incoming.VipLogoutPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedVipLogoutIncomingPacket != null)
                            packet.Forward = ReceivedVipLogoutIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case IncomingPacketType.ShopSaleGoldCount:
                    packet = new Packets.Incoming.ShopSaleGoldCountPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedShopSaleGoldCountIncomingPacket != null)
                            packet.Forward = ReceivedShopSaleGoldCountIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case IncomingPacketType.ShopWindowOpen:
                    packet = new Packets.Incoming.ShopWindowOpenPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedShopWindowOpenIncomingPacket != null)
                            packet.Forward = ReceivedShopWindowOpenIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case IncomingPacketType.ShopWindowClose:
                    packet = new Packets.Incoming.ShopWindowClosePacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedShopWindowCloseIncomingPacket != null)
                            packet.Forward = ReceivedShopWindowCloseIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case IncomingPacketType.OutfitWindow:
                    packet = new Packets.Incoming.OutfitWindowPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedOutfitWindowIncomingPacket != null)
                            packet.Forward = ReceivedOutfitWindowIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case IncomingPacketType.RuleViolationOpen:
                    packet = new Packets.Incoming.RuleViolationOpenPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedRuleViolationOpenIncomingPacket != null)
                            packet.Forward = ReceivedRuleViolationOpenIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case IncomingPacketType.RuleViolationRemove:
                    packet = new Packets.Incoming.RuleViolationRemovePacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedRuleViolationRemoveIncomingPacket != null)
                            packet.Forward = ReceivedRuleViolationRemoveIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case IncomingPacketType.RuleViolationCancel:
                    packet = new Packets.Incoming.RuleViolationCancelPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedRuleViolationCancelIncomingPacket != null)
                            packet.Forward = ReceivedRuleViolationCancelIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case IncomingPacketType.RuleViolationLock:
                    packet = new Packets.Incoming.RuleViolationLockPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedRuleViolationLockIncomingPacket != null)
                            packet.Forward = ReceivedRuleViolationLockIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case IncomingPacketType.CancelTarget:
                    packet = new Packets.Incoming.CancelTargetPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedCancelTargetIncomingPacket != null)
                            packet.Forward = ReceivedCancelTargetIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                default:
                    packetKnown = false;
                    break;
            }

            return packetKnown;
        }
        #endregion

        #region ParsePacketFromClient
        protected bool ParsePacketFromClient(Client client, NetworkMessage msg, NetworkMessage outMsg)
        {
            bool packetKnown = true;
            OutgoingPacket packet = null;
            OutgoingPacketType type = (OutgoingPacketType)msg.PeekByte();
            //System.Console.WriteLine(type.ToString());

            switch (type)
            {
                case OutgoingPacketType.ChannelClose:
                    packet = new Packets.Outgoing.ChannelClosePacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedChannelCloseOutgoingPacket != null)
                            packet.Forward = ReceivedChannelCloseOutgoingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case OutgoingPacketType.ChannelOpen:
                    packet = new Packets.Outgoing.ChannelOpenPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedChannelOpenOutgoingPacket != null)
                            packet.Forward = ReceivedChannelOpenOutgoingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case OutgoingPacketType.PlayerSpeech:
                    packet = new Packets.Outgoing.PlayerSpeechPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedPlayerSpeechOutgoingPacket != null)
                            packet.Forward = ReceivedPlayerSpeechOutgoingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case OutgoingPacketType.Attack:
                    packet = new Packets.Outgoing.AttackPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedAttackOutgoingPacket != null)
                            packet.Forward = ReceivedAttackOutgoingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case OutgoingPacketType.Follow:
                    packet = new Packets.Outgoing.FollowPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedFollowOutgoingPacket != null)
                            packet.Forward = ReceivedFollowOutgoingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case OutgoingPacketType.LookAt:
                    packet = new Packets.Outgoing.LookAtPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedLookAtOutgoingPacket != null)
                            packet.Forward = ReceivedLookAtOutgoingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case OutgoingPacketType.ItemUse:
                    packet = new Packets.Outgoing.ItemUsePacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedItemUseOutgoingPacket != null)
                            packet.Forward = ReceivedItemUseOutgoingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case OutgoingPacketType.ItemUseOn:
                    packet = new Packets.Outgoing.ItemUseOnPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedItemUseOnOutgoingPacket != null)
                            packet.Forward = ReceivedItemUseOnOutgoingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case OutgoingPacketType.ItemMove:
                    packet = new Packets.Outgoing.ItemMovePacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedItemMoveOutgoingPacket != null)
                            packet.Forward = ReceivedItemMoveOutgoingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case OutgoingPacketType.CancelMove:
                    packet = new Packets.Outgoing.CancelMovePacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedCancelMoveOutgoingPacket != null)
                            packet.Forward = ReceivedCancelMoveOutgoingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case OutgoingPacketType.ItemUseBattlelist:
                    packet = new Packets.Outgoing.ItemUseBattlelistPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedItemUseBattlelistOutgoingPacket != null)
                            packet.Forward = ReceivedItemUseBattlelistOutgoingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case OutgoingPacketType.Logout:
                    packet = new Packets.Outgoing.LogoutPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedLogoutOutgoingPacket != null)
                            packet.Forward = ReceivedLogoutOutgoingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case OutgoingPacketType.ContainerClose:
                    packet = new Packets.Outgoing.ContainerClosePacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedContainerCloseOutgoingPacket != null)
                            packet.Forward = ReceivedContainerCloseOutgoingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case OutgoingPacketType.ContainerOpenParent:
                    packet = new Packets.Outgoing.ContainerOpenParentPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedContainerOpenParentOutgoingPacket != null)
                            packet.Forward = ReceivedContainerOpenParentOutgoingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case OutgoingPacketType.ShopBuy:
                    packet = new Packets.Outgoing.ShopBuyPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedShopBuyOutgoingPacket != null)
                            packet.Forward = ReceivedShopBuyOutgoingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case OutgoingPacketType.ShopSell:
                    packet = new Packets.Outgoing.ShopSellPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedShopSellOutgoingPacket != null)
                            packet.Forward = ReceivedShopSellOutgoingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case OutgoingPacketType.TurnDown:
                    msg.GetByte();
                    packet = new Packets.Outgoing.TurnPacket(client, Tibia.Constants.Direction.Down);

                    if (ReceivedTurnOutgoingPacket != null)
                        packet.Forward = ReceivedTurnOutgoingPacket.Invoke(packet);

                    if (packet.Forward)
                        packet.ToNetworkMessage(ref outMsg);
                    break;
                case OutgoingPacketType.TurnUp:
                    msg.GetByte();
                    packet = new Packets.Outgoing.TurnPacket(client, Tibia.Constants.Direction.Up);

                    if (ReceivedTurnOutgoingPacket != null)
                        packet.Forward = ReceivedTurnOutgoingPacket.Invoke(packet);

                    if (packet.Forward)
                        packet.ToNetworkMessage(ref outMsg);

                    break;
                case OutgoingPacketType.TurnLeft:
                    msg.GetByte();
                    packet = new Packets.Outgoing.TurnPacket(client, Tibia.Constants.Direction.Left);

                    if (ReceivedTurnOutgoingPacket != null)
                        packet.Forward = ReceivedTurnOutgoingPacket.Invoke(packet);

                    if (packet.Forward)
                        packet.ToNetworkMessage(ref outMsg);

                    break;
                case OutgoingPacketType.TurnRight:
                    msg.GetByte();
                    packet = new Packets.Outgoing.TurnPacket(client, Tibia.Constants.Direction.Right);

                    if (ReceivedTurnOutgoingPacket != null)
                        packet.Forward = ReceivedTurnOutgoingPacket.Invoke(packet);

                    if (packet.Forward)
                        packet.ToNetworkMessage(ref outMsg);

                    break;
                case OutgoingPacketType.MoveDown:
                    msg.GetByte();
                    packet = new Packets.Outgoing.MovePacket(client, Tibia.Constants.Direction.Down);

                    if (ReceivedMoveOutgoingPacket != null)
                        packet.Forward = ReceivedMoveOutgoingPacket.Invoke(packet);

                    if (packet.Forward)
                        packet.ToNetworkMessage(ref outMsg);

                    break;
                case OutgoingPacketType.MoveDownLeft:
                    msg.GetByte();
                    packet = new Packets.Outgoing.MovePacket(client, Tibia.Constants.Direction.DownLeft);

                    if (ReceivedMoveOutgoingPacket != null)
                        packet.Forward = ReceivedMoveOutgoingPacket.Invoke(packet);

                    if (packet.Forward)
                        packet.ToNetworkMessage(ref outMsg);

                    break;
                case OutgoingPacketType.MoveDownRight:
                    msg.GetByte();
                    packet = new Packets.Outgoing.MovePacket(client, Tibia.Constants.Direction.DownRight);

                    if (ReceivedMoveOutgoingPacket != null)
                        packet.Forward = ReceivedMoveOutgoingPacket.Invoke(packet);

                    if (packet.Forward)
                        packet.ToNetworkMessage(ref outMsg);

                    break;
                case OutgoingPacketType.MoveLeft:
                    msg.GetByte();
                    packet = new Packets.Outgoing.MovePacket(client, Tibia.Constants.Direction.Left);

                    if (ReceivedMoveOutgoingPacket != null)
                        packet.Forward = ReceivedMoveOutgoingPacket.Invoke(packet);

                    if (packet.Forward)
                        packet.ToNetworkMessage(ref outMsg);

                    break;
                case OutgoingPacketType.MoveRight:
                    msg.GetByte();
                    packet = new Packets.Outgoing.MovePacket(client, Tibia.Constants.Direction.Right);

                    if (ReceivedMoveOutgoingPacket != null)
                        packet.Forward = ReceivedMoveOutgoingPacket.Invoke(packet);

                    if (packet.Forward)
                        packet.ToNetworkMessage(ref outMsg);

                    break;
                case OutgoingPacketType.MoveUp:
                    msg.GetByte();
                    packet = new Packets.Outgoing.MovePacket(client, Tibia.Constants.Direction.Up);

                    if (ReceivedMoveOutgoingPacket != null)
                        packet.Forward = ReceivedMoveOutgoingPacket.Invoke(packet);

                    if (packet.Forward)
                        packet.ToNetworkMessage(ref outMsg);

                    break;
                case OutgoingPacketType.MoveUpLeft:
                    msg.GetByte();
                    packet = new Packets.Outgoing.MovePacket(client, Tibia.Constants.Direction.UpLeft);

                    if (ReceivedMoveOutgoingPacket != null)
                        packet.Forward = ReceivedMoveOutgoingPacket.Invoke(packet);

                    if (packet.Forward)
                        packet.ToNetworkMessage(ref outMsg);

                    break;
                case OutgoingPacketType.MoveUpRight:
                    msg.GetByte();
                    packet = new Packets.Outgoing.MovePacket(client, Tibia.Constants.Direction.UpRight);

                    if (ReceivedMoveOutgoingPacket != null)
                        packet.Forward = ReceivedMoveOutgoingPacket.Invoke(packet);

                    if (packet.Forward)
                        packet.ToNetworkMessage(ref outMsg);

                    break;
                case OutgoingPacketType.AutoWalk:
                    packet = new Packets.Outgoing.AutoWalkPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedAutoWalkOutgoingPacket != null)
                            packet.Forward = ReceivedAutoWalkOutgoingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case OutgoingPacketType.VipAdd:
                    packet = new Packets.Outgoing.VipAddPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedVipAddOutgoingPacket != null)
                            packet.Forward = ReceivedVipAddOutgoingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case OutgoingPacketType.VipRemove:
                    packet = new Packets.Outgoing.VipRemovePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedVipRemoveOutgoingPacket != null)
                            packet.Forward = ReceivedVipRemoveOutgoingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case OutgoingPacketType.ItemRotate:
                    packet = new Packets.Outgoing.ItemRotatePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedItemRotateOutgoingPacket != null)
                            packet.Forward = ReceivedItemRotateOutgoingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case OutgoingPacketType.SetOutfit:
                    packet = new Packets.Outgoing.SetOutfitPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedSetOutfitOutgoingPacket != null)
                            packet.Forward = ReceivedSetOutfitOutgoingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case OutgoingPacketType.AutoWalkCancel:
                    packet = new Packets.Outgoing.AutoWalkCancelPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedAutoWalkCancelOutgoingPacket != null)
                            packet.Forward = ReceivedAutoWalkCancelOutgoingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case OutgoingPacketType.Ping:
                    packet = new Packets.Outgoing.PingPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedPingOutgoingPacket != null)
                            packet.Forward = ReceivedPingOutgoingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case OutgoingPacketType.FightModes:
                    packet = new Packets.Outgoing.FightModesPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedFightModesOutgoingPacket != null)
                            packet.Forward = ReceivedFightModesOutgoingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case OutgoingPacketType.TileUpdate:
                    packet = new Packets.Outgoing.TileUpdatePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedTitleUpdateOutgoingPacket != null)
                            packet.Forward = ReceivedTitleUpdateOutgoingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case OutgoingPacketType.ShopClose:
                    packet = new Packets.Outgoing.ShopClosePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedShopCloseOutgoingPacket != null)
                            packet.Forward = ReceivedShopCloseOutgoingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case OutgoingPacketType.NpcChannelClose:
                    packet = new Packets.Outgoing.NpcChannelClosePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedNpcChannelCloseOutgoingPacket != null)
                            packet.Forward = ReceivedNpcChannelCloseOutgoingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case OutgoingPacketType.PrivateChannelOpen:
                    packet = new Packets.Outgoing.PrivateChannelOpenPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedPrivateChannelOpenOutgoingPacket != null)
                            packet.Forward = ReceivedPrivateChannelOpenOutgoingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                case OutgoingPacketType.ChannelList:
                    packet = new Packets.Outgoing.ChannelListPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedChannelListOutgoingPacket != null)
                            packet.Forward = ReceivedChannelListOutgoingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(ref outMsg);
                    }
                    break;
                default:
                    packetKnown = false;
                    break;
            }

            return packetKnown;
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

        protected enum Protocol { None, Login, World }

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

        private object debugLock = new object();
        protected void WriteDebug(string msg)
        {
            try
            {
                lock (debugLock)
                {
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(System.IO.Path.Combine(Application.StartupPath, "proxy_log.txt"), true);
                    sw.WriteLine(System.DateTime.Now.ToShortDateString() + " " + System.DateTime.Now.ToLongTimeString() + " " + msg + "\nLast received packet types: " + GetLastReceivedPacketTypesString());
                    sw.Close();
                }
            }
            catch
            {
            }
        }

        protected FixedCollector<byte> lastReceivedPacketTypes = new FixedCollector<byte>(10);

        public string GetLastReceivedPacketTypesString()
        {
            return String.Join(", ", lastReceivedPacketTypes.Select(delegate(byte b)
            {
                if (Enum.IsDefined(typeof(IncomingPacketType), b))
                {
                    return ((IncomingPacketType)b).ToString();
                }
                else
                {
                    return b.ToString("X2");
                }
            }).ToArray());
        }

        #endregion

    }
}