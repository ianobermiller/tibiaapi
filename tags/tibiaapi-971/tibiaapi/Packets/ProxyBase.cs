using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
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
        public virtual event IncomingPacketListener ReceivedInitGameIncomingPacket;
        public event IncomingPacketListener ReceivedGMActionsIncomingPacket;
        public event IncomingPacketListener ReceivedLoginErrorIncomingPacket;
        public event IncomingPacketListener ReceivedLoginAdviceIncomingPacket;
        public event IncomingPacketListener ReceivedLoginWaitIncomingPacket;
        public event IncomingPacketListener ReceivedPingBackIncomingPacket;
        public event IncomingPacketListener ReceivedPingIncomingPacket;
        public event IncomingPacketListener ReceivedChallengeIncomingPacket;
        public event IncomingPacketListener ReceivedDeathIncomingPacket;

        public event IncomingPacketListener ReceivedFullMapIncomingPacket;
        public event IncomingPacketListener ReceivedTopRowIncomingPacket;
        public event IncomingPacketListener ReceivedRightRowIncomingPacket;
        public event IncomingPacketListener ReceivedBottomRowIncomingPacket;
        public event IncomingPacketListener ReceivedLeftRowIncomingPacket;
        public event IncomingPacketListener ReceivedUpdateTileIncomingPacket;
        public event IncomingPacketListener ReceivedCreateOnMapIncomingPacket;
        public event IncomingPacketListener ReceivedChangeOnMapIncomingPacket;
        public event IncomingPacketListener ReceivedDeleteOnMapIncomingPacket;
        public event IncomingPacketListener ReceivedMoveCreatureIncomingPacket;
        public event IncomingPacketListener ReceivedOpenContainerIncomingPacket;
        public event IncomingPacketListener ReceivedCloseContainerIncomingPacket;
        public event IncomingPacketListener ReceivedCreateContainerIncomingPacket;
        public event IncomingPacketListener ReceivedChangeInContainerIncomingPacket;
        public event IncomingPacketListener ReceivedDeleteInContainerIncomingPacket;
        public event IncomingPacketListener ReceivedSetInventoryIncomingPacket;
        public event IncomingPacketListener ReceivedDeleteInventoryIncomingPacket;
        public event IncomingPacketListener ReceivedOpenNPCTradeIncomingPacket;
        public event IncomingPacketListener ReceivedPlayerGoodsIncomingPacket;
        public event IncomingPacketListener ReceivedCloseNPCTradeIncomingPacket;
        public event IncomingPacketListener ReceivedOwnTradeIncomingPacket;
        public event IncomingPacketListener ReceivedCounterTradeIncomingPacket;
        public event IncomingPacketListener ReceivedCloseTradeIncomingPacket;
        public event IncomingPacketListener ReceivedAmbientIncomingPacket;
        public event IncomingPacketListener ReceivedGraphicalEffectIncomingPacket;
        public event IncomingPacketListener ReceivedTextEffectIncomingPacket;
        public event IncomingPacketListener ReceivedMissileEffectIncomingPacket;
        public event IncomingPacketListener ReceivedMarkCreatureIncomingPacket;
        public event IncomingPacketListener ReceivedTrappersIncomingPacket;
        public event IncomingPacketListener ReceivedCreatureHealthIncomingPacket;
        public event IncomingPacketListener ReceivedCreatureLightIncomingPacket;
        public event IncomingPacketListener ReceivedCreatureOutfitIncomingPacket;
        public event IncomingPacketListener ReceivedCreatureSpeedIncomingPacket;
        public event IncomingPacketListener ReceivedCreatureSkullIncomingPacket;
        public event IncomingPacketListener ReceivedCreaturePartyIncomingPacket;
        public event IncomingPacketListener ReceivedCreatureUnpassIncomingPacket;
        public event IncomingPacketListener ReceivedEditTextIncomingPacket;
        public event IncomingPacketListener ReceivedEditListIncomingPacket;
        public event IncomingPacketListener ReceivedPlayerDataBasicIncomingPacket;
        public event IncomingPacketListener ReceivedPlayerDataCurrentIncomingPacket;
        public event IncomingPacketListener ReceivedPlayerSkillsIncomingPacket;
        public event IncomingPacketListener ReceivedPlayerStateIncomingPacket;
        public event IncomingPacketListener ReceivedClearTargetIncomingPacket;
        public event IncomingPacketListener ReceivedSpellDelayIncomingPacket;
        public event IncomingPacketListener ReceivedSpellGroupDelayIncomingPacket;
        public event IncomingPacketListener ReceivedMultiUseDelayIncomingPacket;
        public event IncomingPacketListener ReceivedTalkIncomingPacket;
        public event IncomingPacketListener ReceivedChannelsIncomingPacket;
        public event IncomingPacketListener ReceivedOpenChannelIncomingPacket;
        public event IncomingPacketListener ReceivedOpenPrivateChannelIncomingPacket;
        public event IncomingPacketListener ReceivedRuleViolationChannelIncomingPacket;
        public event IncomingPacketListener ReceivedRuleViolationRemoveIncomingPacket;
        public event IncomingPacketListener ReceivedRuleViolationCancelIncomingPacket;
        public event IncomingPacketListener ReceivedRuleViolationLockIncomingPacket;
        public event IncomingPacketListener ReceivedOpenOwnChannelIncomingPacket;
        public event IncomingPacketListener ReceivedCloseChannelIncomingPacket;
        public event IncomingPacketListener ReceivedTextMessageIncomingPacket;
        public event IncomingPacketListener ReceivedCancelWalkIncomingPacket;
        public event IncomingPacketListener ReceivedWalkWaitIncomingPacket;
        public event IncomingPacketListener ReceivedFloorChangeUpIncomingPacket;
        public event IncomingPacketListener ReceivedFloorChangeDownIncomingPacket;
        public event IncomingPacketListener ReceivedChooseOutfitIncomingPacket;
        public event IncomingPacketListener ReceivedVipAddIncomingPacket;
        public event IncomingPacketListener ReceivedVipLoginIncomingPacket;
        public event IncomingPacketListener ReceivedVipLogoutIncomingPacket;
        public event IncomingPacketListener ReceivedTutorialHintIncomingPacket;
        public event IncomingPacketListener ReceivedAutomapFlagIncomingPacket;
        public event IncomingPacketListener ReceivedQuestLogIncomingPacket;
        public event IncomingPacketListener ReceivedQuestLineIncomingPacket;
        public event IncomingPacketListener ReceivedChannelEventIncomingPacket;
        public event IncomingPacketListener ReceivedItemInfoIncomingPacket;
        public event IncomingPacketListener ReceivedPlayerInventoryIncomingPacket;
        public event IncomingPacketListener ReceivedMarketEnterIncomingPacket;
        public event IncomingPacketListener ReceivedMarketLeaveIncomingPacket;
        public event IncomingPacketListener ReceivedMarketDetailIncomingPacket;
        public event IncomingPacketListener ReceivedMarketBrowseIncomingPacket;
        public event IncomingPacketListener ReceivedShowModalDialogIncomingPacket;
        
        // outgoing
        public event OutgoingPacketListener ReceivedLoginServerRequestOutgoingPacket;
        public event OutgoingPacketListener ReceivedEnterGameOutgoingPacket;
        public event OutgoingPacketListener ReceivedQuitGameOutgoingPacket;
        public event OutgoingPacketListener ReceivedPingOutgoingPacket;
        public event OutgoingPacketListener ReceivedPingBackOutgoingPacket;

        public event OutgoingPacketListener ReceivedAutoWalkOutgoingPacket;
        public event OutgoingPacketListener ReceivedWalkNorthOutgoingPacket;
        public event OutgoingPacketListener ReceivedWalkEastOutgoingPacket;
        public event OutgoingPacketListener ReceivedWalkSouthOutgoingPacket;
        public event OutgoingPacketListener ReceivedWalkWestOutgoingPacket;
        public event OutgoingPacketListener ReceivedStopOutgoingPacket;
        public event OutgoingPacketListener ReceivedWalkNorthEastOutgoingPacket;
        public event OutgoingPacketListener ReceivedWalkSouthEastOutgoingPacket;
        public event OutgoingPacketListener ReceivedWalkSouthWestOutgoingPacket;
        public event OutgoingPacketListener ReceivedWalkNorthWestOutgoingPacket;
        public event OutgoingPacketListener ReceivedTurnNorthOutgoingPacket;
        public event OutgoingPacketListener ReceivedTurnEastOutgoingPacket;
        public event OutgoingPacketListener ReceivedTurnSouthOutgoingPacket;
        public event OutgoingPacketListener ReceivedTurnWestOutgoingPacket;
        public event OutgoingPacketListener ReceivedEquipItemOutgoingPacket;
        public event OutgoingPacketListener ReceivedMoveOutgoingPacket;
        public event OutgoingPacketListener ReceivedInspectNPCTradeOutgoingPacket;
        public event OutgoingPacketListener ReceivedBuyItemOutgoingPacket;
        public event OutgoingPacketListener ReceivedSellItemOutgoingPacket;
        public event OutgoingPacketListener ReceivedCloseNPCTradeOutgoingPacket;
        public event OutgoingPacketListener ReceivedRequestTradeOutgoingPacket;
        public event OutgoingPacketListener ReceivedInspectTradeOutgoingPacket;
        public event OutgoingPacketListener ReceivedAcceptTradeOutgoingPacket;
        public event OutgoingPacketListener ReceivedRejectTradeOutgoingPacket;
        public event OutgoingPacketListener ReceivedUseItemOutgoingPacket;
        public event OutgoingPacketListener ReceivedUseItemWithOutgoingPacket;
        public event OutgoingPacketListener ReceivedUseOnCreatureOutgoingPacket;
        public event OutgoingPacketListener ReceivedRotateItemOutgoingPacket;
        public event OutgoingPacketListener ReceivedCloseContainerOutgoingPacket;
        public event OutgoingPacketListener ReceivedUpContainerOutgoingPacket;
        public event OutgoingPacketListener ReceivedEditTextOutgoingPacket;
        public event OutgoingPacketListener ReceivedEditListOutgoingPacket;
        public event OutgoingPacketListener ReceivedLookOutgoingPacket;
        public event OutgoingPacketListener ReceivedLookCreatureOutgoingPacket;
        public event OutgoingPacketListener ReceivedTalkOutgoingPacket;
        public event OutgoingPacketListener ReceivedRequestChannelsOutgoingPacket;
        public event OutgoingPacketListener ReceivedJoinChannelOutgoingPacket;
        public event OutgoingPacketListener ReceivedLeaveChannelOutgoingPacket;
        public event OutgoingPacketListener ReceivedOpenPrivateChannelOutgoingPacket;
        public event OutgoingPacketListener ReceivedCloseNPCChannelOutgoingPacket;
        public event OutgoingPacketListener ReceivedChangeFightModesOutgoingPacket;
        public event OutgoingPacketListener ReceivedAttackOutgoingPacket;
        public event OutgoingPacketListener ReceivedFollowOutgoingPacket;
        public event OutgoingPacketListener ReceivedInviteToPartyOutgoingPacket;
        public event OutgoingPacketListener ReceivedJoinPartyOutgoingPacket;
        public event OutgoingPacketListener ReceivedRevokeInvitationOutgoingPacket;
        public event OutgoingPacketListener ReceivedPassLeadershipOutgoingPacket;
        public event OutgoingPacketListener ReceivedLeavePartyOutgoingPacket;
        public event OutgoingPacketListener ReceivedShareExperienceOutgoingPacket;
        public event OutgoingPacketListener ReceivedDisbandPartyOutgoingPacket; 
        public event OutgoingPacketListener ReceivedOpenOwnChannelOutgoingPacket;
        public event OutgoingPacketListener ReceivedInviteToOwnChannelOutgoingPacket;
        public event OutgoingPacketListener ReceivedExcludeFromOwnChannelOutgoingPacket;
        public event OutgoingPacketListener ReceivedCancelAttackAndFollowOutgoingPacket;
        public event OutgoingPacketListener ReceivedTileUpdateOutgoingPacket;
        public event OutgoingPacketListener ReceivedRefreshContainerOutgoingPacket;
        public event OutgoingPacketListener ReceivedRequestOutfitOutgoingPacket;
        public event OutgoingPacketListener ReceivedChangeOutfitOutgoingPacket;
        public event OutgoingPacketListener ReceivedMountOutgoingPacket;
        public event OutgoingPacketListener ReceivedAddVipOutgoingPacket;
        public event OutgoingPacketListener ReceivedRemoveVipOutgoingPacket;
        public event OutgoingPacketListener ReceivedEditVipOutgoingPacket;
        public event OutgoingPacketListener ReceivedBugReportOutgoingPacket;
        public event OutgoingPacketListener ReceivedRuleViolationOutgoingPacket;
        public event OutgoingPacketListener ReceivedDebugReportOutgoingPacket;
        public event OutgoingPacketListener ReceivedRequestQuestLogOutgoingPacket;
        public event OutgoingPacketListener ReceivedRequestQuestLineOutgoingPacket;
        public event OutgoingPacketListener ReceivedNewRuleViolationOutgoingPacket;
        public event OutgoingPacketListener ReceivedRequestItemInfoOutgoingPacket;
        public event OutgoingPacketListener ReceivedMarketLeaveOutgoingPacket;
        public event OutgoingPacketListener ReceivedMarketBrowseOutgoingPacket;
        public event OutgoingPacketListener ReceivedMarketCreateOutgoingPacket;
        public event OutgoingPacketListener ReceivedMarketCancelOutgoingPacket;
        public event OutgoingPacketListener ReceivedMarketAcceptOutgoingPacket;
        public event OutgoingPacketListener ReceivedAnswerModalDialogOutgoingPacket;
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
                case IncomingPacketType.InitGame:
                    packet = new Packets.Incoming.InitGamePacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedInitGameIncomingPacket != null)
                            packet.Forward = ReceivedInitGameIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.GMActions:
                    packet = new Packets.Incoming.GMActionsPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedGMActionsIncomingPacket != null)
                            packet.Forward = ReceivedGMActionsIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.LoginError:
                    packet = new Packets.Incoming.LoginErrorPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedLoginErrorIncomingPacket != null)
                            packet.Forward = ReceivedLoginErrorIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.LoginAdvice:
                    packet = new Packets.Incoming.LoginAdvicePacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedLoginAdviceIncomingPacket != null)
                            packet.Forward = ReceivedLoginAdviceIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.LoginWait:
                    packet = new Packets.Incoming.LoginWaitPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedLoginWaitIncomingPacket != null)
                            packet.Forward = ReceivedLoginWaitIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.Ping:
                    packet = new Packets.Incoming.PingPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedPingIncomingPacket != null)
                            packet.Forward = ReceivedPingIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.PingBack:
                    packet = new Packets.Incoming.PingBackPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedPingBackIncomingPacket != null)
                            packet.Forward = ReceivedPingBackIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.Challenge:
                    packet = new Packets.Incoming.ChallengePacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedChallengeIncomingPacket != null)
                            packet.Forward = ReceivedChallengeIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;    
                case IncomingPacketType.Death:
                    packet = new Packets.Incoming.DeathPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedDeathIncomingPacket != null)
                            packet.Forward = ReceivedDeathIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.FullMap:
                    packet = new Packets.Incoming.FullMapPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client, outMsg))
                    {
                        if (ReceivedFullMapIncomingPacket != null)
                            packet.Forward = ReceivedFullMapIncomingPacket.Invoke(packet);
                    }
                    break;
                case IncomingPacketType.TopRow:
                    packet = new Packets.Incoming.TopRowPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client, outMsg))
                    {
                        if (ReceivedTopRowIncomingPacket != null)
                            packet.Forward = ReceivedTopRowIncomingPacket.Invoke(packet);
                    }
                    break;
                case IncomingPacketType.RightRow:
                    packet = new Packets.Incoming.RightRowPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client, outMsg))
                    {
                        if (ReceivedRightRowIncomingPacket != null)
                            packet.Forward = ReceivedRightRowIncomingPacket.Invoke(packet);
                    }
                    break;
                case IncomingPacketType.BottomRow:
                    packet = new Packets.Incoming.BottomRowPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client, outMsg))
                    {
                        if (ReceivedBottomRowIncomingPacket != null)
                            packet.Forward = ReceivedBottomRowIncomingPacket.Invoke(packet);
                    }
                    break;
                case IncomingPacketType.LeftRow:
                    packet = new Packets.Incoming.LeftRowPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client, outMsg))
                    {
                        if (ReceivedLeftRowIncomingPacket != null)
                            packet.Forward = ReceivedLeftRowIncomingPacket.Invoke(packet);
                    }
                    break;
                case IncomingPacketType.UpdateTile:
                    packet = new Packets.Incoming.UpdateTilePacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client, outMsg))
                    {
                        if (ReceivedUpdateTileIncomingPacket != null)
                            packet.Forward = ReceivedUpdateTileIncomingPacket.Invoke(packet);
                    }
                    break;
                case IncomingPacketType.CreateOnMap:
                    packet = new Packets.Incoming.CreateOnMapPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedCreateOnMapIncomingPacket != null)
                            packet.Forward = ReceivedCreateOnMapIncomingPacket.Invoke(packet);

                        //if (packet.Forward)
                        //    packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.ChangeOnMap:
                    packet = new Packets.Incoming.ChangeOnMapPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedChangeOnMapIncomingPacket != null)
                            packet.Forward = ReceivedChangeOnMapIncomingPacket.Invoke(packet);

                        //if (packet.Forward)
                        //    packet.ToNetworkMessage(outMsg);

                        return true;
                    }
                    break;
                case IncomingPacketType.DeleteOnMap:
                    packet = new Packets.Incoming.DeleteOnMapPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedDeleteOnMapIncomingPacket != null)
                            packet.Forward = ReceivedDeleteOnMapIncomingPacket.Invoke(packet);

                        //if (packet.Forward)
                        //    packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.MoveCreature:
                    packet = new Packets.Incoming.MoveCreaturePacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedMoveCreatureIncomingPacket != null)
                            packet.Forward = ReceivedMoveCreatureIncomingPacket.Invoke(packet);

                        //if (packet.Forward)
                        //    packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.OpenContainer:
                    packet = new Packets.Incoming.OpenContainerPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedOpenContainerIncomingPacket != null)
                            packet.Forward = ReceivedOpenContainerIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.CloseContainer:
                    packet = new Packets.Incoming.CloseContainerPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedCloseContainerIncomingPacket != null)
                            packet.Forward = ReceivedCloseContainerIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.CreateContainer:
                    packet = new Packets.Incoming.CreateContainerPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedCreateContainerIncomingPacket != null)
                            packet.Forward = ReceivedCreateContainerIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.ChangeInContainer:
                    packet = new Packets.Incoming.ChangeInContainerPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedChangeInContainerIncomingPacket != null)
                            packet.Forward = ReceivedChangeInContainerIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.DeleteInContainer:
                    packet = new Packets.Incoming.DeleteInContainerPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedDeleteInContainerIncomingPacket != null)
                            packet.Forward = ReceivedDeleteInContainerIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.SetInventory:
                    packet = new Packets.Incoming.SetInventoryPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedSetInventoryIncomingPacket != null)
                            packet.Forward = ReceivedSetInventoryIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.DeleteInventory:
                    packet = new Packets.Incoming.DeleteInventoryPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedDeleteInventoryIncomingPacket != null)
                            packet.Forward = ReceivedDeleteInventoryIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.OpenNPCTrade:
                    packet = new Packets.Incoming.OpenNPCTradePacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedOpenNPCTradeIncomingPacket != null)
                            packet.Forward = ReceivedOpenNPCTradeIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.PlayerGoods:
                    packet = new Packets.Incoming.PlayerGoodsPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedPlayerGoodsIncomingPacket != null)
                            packet.Forward = ReceivedPlayerGoodsIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.CloseNPCTrade:
                    packet = new Packets.Incoming.CloseNPCTradePacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedCloseNPCTradeIncomingPacket != null)
                            packet.Forward = ReceivedCloseNPCTradeIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.OwnTrade:
                    packet = new Packets.Incoming.OwnTradePacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedOwnTradeIncomingPacket != null)
                            packet.Forward = ReceivedOwnTradeIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.CounterTrade:
                    packet = new Packets.Incoming.CounterTradePacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedCounterTradeIncomingPacket != null)
                            packet.Forward = ReceivedCounterTradeIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.CloseTrade:
                    packet = new Packets.Incoming.CloseTradePacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedCloseTradeIncomingPacket != null)
                            packet.Forward = ReceivedCloseTradeIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.Ambient:
                    packet = new Packets.Incoming.AmbientPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedAmbientIncomingPacket != null)
                            packet.Forward = ReceivedAmbientIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.GraphicalEffect:
                    packet = new Packets.Incoming.GraphicalEffectPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedGraphicalEffectIncomingPacket != null)
                            packet.Forward = ReceivedGraphicalEffectIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.TextEffect:
                    packet = new Packets.Incoming.TextEffectPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedTextEffectIncomingPacket != null)
                            packet.Forward = ReceivedTextEffectIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.MissileEffect:
                    packet = new Packets.Incoming.MissileEffectPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedMissileEffectIncomingPacket != null)
                            packet.Forward = ReceivedMissileEffectIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.MarkCreature:
                    packet = new Packets.Incoming.MarkCreaturePacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedMarkCreatureIncomingPacket != null)
                            packet.Forward = ReceivedMarkCreatureIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.Trappers:
                    packet = new Packets.Incoming.TrappersPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedTrappersIncomingPacket != null)
                            packet.Forward = ReceivedTrappersIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.CreatureHealth:
                    packet = new Packets.Incoming.CreatureHealthPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedCreatureHealthIncomingPacket != null)
                            packet.Forward = ReceivedCreatureHealthIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.CreatureLight:
                    packet = new Packets.Incoming.CreatureLightPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedCreatureLightIncomingPacket != null)
                            packet.Forward = ReceivedCreatureLightIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.CreatureOutfit:
                    packet = new Packets.Incoming.CreatureOutfitPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedCreatureOutfitIncomingPacket != null)
                            packet.Forward = ReceivedCreatureOutfitIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.CreatureSpeed:
                    packet = new Packets.Incoming.CreatureSpeedPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedCreatureSpeedIncomingPacket != null)
                            packet.Forward = ReceivedCreatureSpeedIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);

                        return true;
                    }
                    break;
                case IncomingPacketType.CreatureSkull:
                    packet = new Packets.Incoming.CreatureSkullPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedCreatureSkullIncomingPacket != null)
                            packet.Forward = ReceivedCreatureSkullIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.CreatureParty:
                    packet = new Packets.Incoming.CreaturePartyPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedCreaturePartyIncomingPacket != null)
                            packet.Forward = ReceivedCreaturePartyIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.CreatureUnpass:
                    packet = new Packets.Incoming.CreatureUnpassPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedCreatureUnpassIncomingPacket != null)
                            packet.Forward = ReceivedCreatureUnpassIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.EditText:
                    packet = new Packets.Incoming.EditTextPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedEditTextIncomingPacket != null)
                            packet.Forward = ReceivedEditTextIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.EditList:
                    packet = new Packets.Incoming.EditListPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedEditListIncomingPacket != null)
                            packet.Forward = ReceivedEditListIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.PlayerDataBasic:
                    packet = new Packets.Incoming.PlayerDataBasicPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedPlayerDataBasicIncomingPacket != null)
                            packet.Forward = ReceivedPlayerDataBasicIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.PlayerData:
                    packet = new Packets.Incoming.PlayerDataPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedPlayerDataCurrentIncomingPacket != null)
                            packet.Forward = ReceivedPlayerDataCurrentIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.PlayerSkills:
                    packet = new Packets.Incoming.PlayerSkillsPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedPlayerSkillsIncomingPacket != null)
                            packet.Forward = ReceivedPlayerSkillsIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.PlayerState:
                    packet = new Packets.Incoming.PlayerStatePacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedPlayerStateIncomingPacket != null)
                            packet.Forward = ReceivedPlayerStateIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.ClearTarget:
                    packet = new Packets.Incoming.ClearTargetPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedClearTargetIncomingPacket != null)
                            packet.Forward = ReceivedClearTargetIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.SpellDelay:
                    packet = new Packets.Incoming.SpellDelayPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedSpellDelayIncomingPacket != null)
                            packet.Forward = ReceivedSpellDelayIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.SpellGroupDelay:
                    packet = new Packets.Incoming.SpellGroupDelayPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedSpellGroupDelayIncomingPacket != null)
                            packet.Forward = ReceivedSpellGroupDelayIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.MultiUseDelay:
                    packet = new Packets.Incoming.MultiUseDelayPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedMultiUseDelayIncomingPacket != null)
                            packet.Forward = ReceivedMultiUseDelayIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.Talk:
                    packet = new Packets.Incoming.TalkPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedTalkIncomingPacket != null)
                            packet.Forward = ReceivedTalkIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.Channels:
                    packet = new Packets.Incoming.ChannelsPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedChannelsIncomingPacket != null)
                            packet.Forward = ReceivedChannelsIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.OpenChannel:
                    packet = new Packets.Incoming.OpenChannelPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedOpenChannelIncomingPacket != null)
                            packet.Forward = ReceivedOpenChannelIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.OpenPrivateChannel:
                    packet = new Packets.Incoming.OpenPrivateChannelPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedOpenPrivateChannelIncomingPacket != null)
                            packet.Forward = ReceivedOpenPrivateChannelIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.RuleViolationChannel:
                    packet = new Packets.Incoming.RuleViolationChannelPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedRuleViolationChannelIncomingPacket != null)
                            packet.Forward = ReceivedRuleViolationChannelIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.RuleViolationRemove:
                    packet = new Packets.Incoming.RuleViolationRemovePacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedRuleViolationRemoveIncomingPacket != null)
                            packet.Forward = ReceivedRuleViolationRemoveIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.RuleViolationCancel:
                    packet = new Packets.Incoming.RuleViolationCancelPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedRuleViolationCancelIncomingPacket != null)
                            packet.Forward = ReceivedRuleViolationCancelIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.RuleViolationLock:
                    packet = new Packets.Incoming.RuleViolationLockPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedRuleViolationLockIncomingPacket != null)
                            packet.Forward = ReceivedRuleViolationLockIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.OpenOwnChannel:
                    packet = new Packets.Incoming.OpenOwnChannelPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedOpenOwnChannelIncomingPacket != null)
                            packet.Forward = ReceivedOpenOwnChannelIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.CloseChannel:
                    packet = new Packets.Incoming.CloseChannelPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedCloseChannelIncomingPacket != null)
                            packet.Forward = ReceivedCloseChannelIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.TextMessage:
                    packet = new Packets.Incoming.TextMessagePacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedTextMessageIncomingPacket != null)
                            packet.Forward = ReceivedTextMessageIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.CancelWalk:
                    packet = new Packets.Incoming.CancelWalkPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedCancelWalkIncomingPacket != null)
                            packet.Forward = ReceivedCancelWalkIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.WalkWait:
                    packet = new Packets.Incoming.WalkWaitPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedWalkWaitIncomingPacket != null)
                            packet.Forward = ReceivedWalkWaitIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
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
                case IncomingPacketType.FloorChangeDown:
                    packet = new Packets.Incoming.FloorChangeDownPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client, outMsg))
                    {
                        if (ReceivedFloorChangeDownIncomingPacket != null)
                            packet.Forward = ReceivedFloorChangeDownIncomingPacket.Invoke(packet);
                    }
                    break;
                case IncomingPacketType.ChooseOutfit:
                    packet = new Packets.Incoming.ChooseOutfitPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedChooseOutfitIncomingPacket != null)
                            packet.Forward = ReceivedChooseOutfitIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.VipAdd:
                    packet = new Packets.Incoming.VipAddPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedVipAddIncomingPacket != null)
                            packet.Forward = ReceivedVipAddIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.VipLogin:
                    packet = new Packets.Incoming.VipLoginPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedVipLoginIncomingPacket != null)
                            packet.Forward = ReceivedVipLoginIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.VipLogout:
                    packet = new Packets.Incoming.VipLogoutPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedVipLogoutIncomingPacket != null)
                            packet.Forward = ReceivedVipLogoutIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.TutorialHint:
                    packet = new Packets.Incoming.TutorialHintPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedTutorialHintIncomingPacket != null)
                            packet.Forward = ReceivedTutorialHintIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.AutomapFlag:
                    packet = new Packets.Incoming.AutomapFlagPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedAutomapFlagIncomingPacket != null)
                            packet.Forward = ReceivedAutomapFlagIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.QuestLog:
                    packet = new Packets.Incoming.QuestLogPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedQuestLogIncomingPacket != null)
                            packet.Forward = ReceivedQuestLogIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.QuestLine:
                    packet = new Packets.Incoming.QuestLinePacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedQuestLineIncomingPacket != null)
                            packet.Forward = ReceivedQuestLineIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.ChannelEvent:
                    packet = new Packets.Incoming.ChannelEventPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedChannelEventIncomingPacket != null)
                            packet.Forward = ReceivedChannelEventIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.ItemInfo:
                    packet = new Packets.Incoming.ItemInfoPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedItemInfoIncomingPacket != null)
                            packet.Forward = ReceivedItemInfoIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.PlayerInventory:
                    packet = new Packets.Incoming.PlayerInventoryPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedPlayerInventoryIncomingPacket != null)
                            packet.Forward = ReceivedPlayerInventoryIncomingPacket.Invoke(packet);

                        if (packet.Forward)
                            packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.MarketEnter:
                    packet = new Packets.Incoming.MarketEnterPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedMarketEnterIncomingPacket != null)
                            packet.Forward = ReceivedMarketEnterIncomingPacket.Invoke(packet);

                        //if (packet.Forward)
                        //    packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.MarketLeave:
                    packet = new Packets.Incoming.MarketLeavePacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedMarketLeaveIncomingPacket != null)
                            packet.Forward = ReceivedMarketLeaveIncomingPacket.Invoke(packet);

                        //if (packet.Forward)
                        //    packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.MarketDetail:
                    packet = new Packets.Incoming.MarketDetailPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedMarketDetailIncomingPacket != null)
                            packet.Forward = ReceivedMarketDetailIncomingPacket.Invoke(packet);

                        //if (packet.Forward)
                        //    packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.MarketBrowse:
                    packet = new Packets.Incoming.MarketBrowsePacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedMarketBrowseIncomingPacket != null)
                            packet.Forward = ReceivedMarketBrowseIncomingPacket.Invoke(packet);

                        //if (packet.Forward)
                        //    packet.ToNetworkMessage(outMsg);
                    }
                    break;
                case IncomingPacketType.ShowModalDialog:
                    packet = new Packets.Incoming.ShowModalDialogPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Client))
                    {
                        if (ReceivedShowModalDialogIncomingPacket != null)
                            packet.Forward = ReceivedShowModalDialogIncomingPacket.Invoke(packet);

                        //if (packet.Forward)
                        //    packet.ToNetworkMessage(outMsg);
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
                case OutgoingPacketType.QuitGame:
                    packet = new Packets.Outgoing.QuitGamePacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedQuitGameOutgoingPacket != null)
                            packet.Forward = ReceivedQuitGameOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.Ping:
                    packet = new Packets.Outgoing.PingPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedPingOutgoingPacket != null)
                            packet.Forward = ReceivedPingOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.PingBack:
                    packet = new Packets.Outgoing.PingBackPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedPingBackOutgoingPacket != null)
                            packet.Forward = ReceivedPingBackOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.AutoWalk:
                    packet = new Packets.Outgoing.AutoWalkPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedAutoWalkOutgoingPacket != null)
                            packet.Forward = ReceivedAutoWalkOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.WalkNorth:
                    msg.GetByte();
                    packet = new Packets.Outgoing.WalkPacket(client, Tibia.Constants.Direction.Up);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedMoveOutgoingPacket != null)
                            packet.Forward = ReceivedMoveOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.WalkEast:
                    msg.GetByte();
                    packet = new Packets.Outgoing.WalkPacket(client, Tibia.Constants.Direction.Right);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedMoveOutgoingPacket != null)
                            packet.Forward = ReceivedMoveOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.WalkSouth:
                    msg.GetByte();
                    packet = new Packets.Outgoing.WalkPacket(client, Tibia.Constants.Direction.Down);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedMoveOutgoingPacket != null)
                            packet.Forward = ReceivedMoveOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.WalkWest:
                    msg.GetByte();
                    packet = new Packets.Outgoing.WalkPacket(client, Tibia.Constants.Direction.Left);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedMoveOutgoingPacket != null)
                            packet.Forward = ReceivedMoveOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.Stop:
                    packet = new Packets.Outgoing.StopPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedStopOutgoingPacket != null)
                            packet.Forward = ReceivedStopOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.WalkNorthEast:
                    msg.GetByte();
                    packet = new Packets.Outgoing.WalkPacket(client, Tibia.Constants.Direction.UpRight);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedMoveOutgoingPacket != null)
                            packet.Forward = ReceivedMoveOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.WalkSouthEast:
                    msg.GetByte();
                    packet = new Packets.Outgoing.WalkPacket(client, Tibia.Constants.Direction.DownRight);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedMoveOutgoingPacket != null)
                            packet.Forward = ReceivedMoveOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.WalkSouthWest:
                    msg.GetByte();
                    packet = new Packets.Outgoing.WalkPacket(client, Tibia.Constants.Direction.DownLeft);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedMoveOutgoingPacket != null)
                            packet.Forward = ReceivedMoveOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.WalkNorthWest:
                    msg.GetByte();
                    packet = new Packets.Outgoing.WalkPacket(client, Tibia.Constants.Direction.UpLeft);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedMoveOutgoingPacket != null)
                            packet.Forward = ReceivedMoveOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.TurnNorth:
                    msg.GetByte();
                    packet = new Packets.Outgoing.TurnPacket(client, Tibia.Constants.Direction.Up);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedTurnNorthOutgoingPacket != null)
                            packet.Forward = ReceivedTurnNorthOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.TurnEast:
                    msg.GetByte();
                    packet = new Packets.Outgoing.TurnPacket(client, Tibia.Constants.Direction.Right);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedTurnEastOutgoingPacket != null)
                            packet.Forward = ReceivedTurnEastOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.TurnSouth:
                    msg.GetByte();
                    packet = new Packets.Outgoing.TurnPacket(client, Tibia.Constants.Direction.Down);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedTurnSouthOutgoingPacket != null)
                            packet.Forward = ReceivedTurnSouthOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.TurnWest:
                    msg.GetByte();
                    packet = new Packets.Outgoing.TurnPacket(client, Tibia.Constants.Direction.Left);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedTurnWestOutgoingPacket != null)
                            packet.Forward = ReceivedTurnWestOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.EquipItem:
                    packet = new Packets.Outgoing.EquipItemPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedEquipItemOutgoingPacket != null)
                            packet.Forward = ReceivedEquipItemOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.Move:
                    packet = new Packets.Outgoing.MovePacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedMoveOutgoingPacket != null)
                            packet.Forward = ReceivedMoveOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.InspectNPCTrade:
                    packet = new Packets.Outgoing.InspectNPCTradePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedInspectNPCTradeOutgoingPacket != null)
                            packet.Forward = ReceivedInspectNPCTradeOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.BuyItem:
                    packet = new Packets.Outgoing.BuyItemPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedBuyItemOutgoingPacket != null)
                            packet.Forward = ReceivedBuyItemOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.SellItem:
                    packet = new Packets.Outgoing.SellItemPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedSellItemOutgoingPacket != null)
                            packet.Forward = ReceivedSellItemOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.CloseNPCTrade:
                    packet = new Packets.Outgoing.CloseNPCTradePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedCloseNPCTradeOutgoingPacket != null)
                            packet.Forward = ReceivedCloseNPCTradeOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.RequestTrade:
                    packet = new Packets.Outgoing.RequestTradePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedRequestTradeOutgoingPacket != null)
                            packet.Forward = ReceivedRequestTradeOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.InspectTrade:
                    packet = new Packets.Outgoing.InspectTradePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedInspectTradeOutgoingPacket != null)
                            packet.Forward = ReceivedInspectTradeOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.AcceptTrade:
                    packet = new Packets.Outgoing.AcceptTradePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedAcceptTradeOutgoingPacket != null)
                            packet.Forward = ReceivedAcceptTradeOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.RejectTrade:
                    packet = new Packets.Outgoing.RejectTradePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedRejectTradeOutgoingPacket != null)
                            packet.Forward = ReceivedRejectTradeOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.UseItem:
                    packet = new Packets.Outgoing.UseItemPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedUseItemOutgoingPacket != null)
                            packet.Forward = ReceivedUseItemOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.UseItemWith:
                    packet = new Packets.Outgoing.UseItemWithPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedUseItemWithOutgoingPacket != null)
                            packet.Forward = ReceivedUseItemWithOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.UseOnCreature:
                    packet = new Packets.Outgoing.UseItemOnCreaturePacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedUseOnCreatureOutgoingPacket != null)
                            packet.Forward = ReceivedUseOnCreatureOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.RotateItem:
                    packet = new Packets.Outgoing.RotateItemPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedRotateItemOutgoingPacket != null)
                            packet.Forward = ReceivedRotateItemOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.CloseContainer:
                    packet = new Packets.Outgoing.CloseContainerPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedCloseContainerOutgoingPacket != null)
                            packet.Forward = ReceivedCloseContainerOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.UpContainer:
                    packet = new Packets.Outgoing.UpContainerPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedUpContainerOutgoingPacket != null)
                            packet.Forward = ReceivedUpContainerOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.EditText:
                    packet = new Packets.Outgoing.EditTextPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedEditTextOutgoingPacket != null)
                            packet.Forward = ReceivedEditTextOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.EditList:
                    packet = new Packets.Outgoing.EditListPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedEditListOutgoingPacket != null)
                            packet.Forward = ReceivedEditListOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.Look:
                    packet = new Packets.Outgoing.LookPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedLookOutgoingPacket != null)
                            packet.Forward = ReceivedLookOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.LookCreature:
                    packet = new Packets.Outgoing.LookCreaturePacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedLookCreatureOutgoingPacket != null)
                            packet.Forward = ReceivedLookCreatureOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.Talk:
                    packet = new Packets.Outgoing.TalkPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedTalkOutgoingPacket != null)
                            packet.Forward = ReceivedTalkOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.RequestChannels:
                    packet = new Packets.Outgoing.RequestChannelsPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedRequestChannelsOutgoingPacket != null)
                            packet.Forward = ReceivedRequestChannelsOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.JoinChannel:
                    packet = new Packets.Outgoing.JoinChannelPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedJoinChannelOutgoingPacket != null)
                            packet.Forward = ReceivedJoinChannelOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.LeaveChannel:
                    packet = new Packets.Outgoing.LeaveChannelPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedLeaveChannelOutgoingPacket != null)
                            packet.Forward = ReceivedLeaveChannelOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.OpenPrivateChannel:
                    packet = new Packets.Outgoing.PrivateChannelPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedOpenPrivateChannelOutgoingPacket != null)
                            packet.Forward = ReceivedOpenPrivateChannelOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.CloseNPCChannel:
                    packet = new Packets.Outgoing.CloseNPCChannelPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedCloseNPCChannelOutgoingPacket != null)
                            packet.Forward = ReceivedCloseNPCChannelOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.ChangeFightModes:
                    packet = new Packets.Outgoing.ChangeFightModesPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedChangeFightModesOutgoingPacket != null)
                            packet.Forward = ReceivedChangeFightModesOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.Attack:
                    packet = new Packets.Outgoing.AttackPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedAttackOutgoingPacket != null)
                            packet.Forward = ReceivedAttackOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.Follow:
                    packet = new Packets.Outgoing.FollowPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedFollowOutgoingPacket != null)
                            packet.Forward = ReceivedFollowOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.InviteToParty:
                    packet = new Packets.Outgoing.InviteToPartyPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedInviteToPartyOutgoingPacket != null)
                            packet.Forward = ReceivedInviteToPartyOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.JoinParty:
                    packet = new Packets.Outgoing.JoinPartyPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedJoinPartyOutgoingPacket != null)
                            packet.Forward = ReceivedJoinPartyOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.RevokeInvitation:
                    packet = new Packets.Outgoing.RevokeInvitationPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedRevokeInvitationOutgoingPacket != null)
                            packet.Forward = ReceivedRevokeInvitationOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.PassLeadership:
                    packet = new Packets.Outgoing.PassLeadershipPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedPassLeadershipOutgoingPacket != null)
                            packet.Forward = ReceivedPassLeadershipOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.LeaveParty:
                    packet = new Packets.Outgoing.LeavePartyPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedLeavePartyOutgoingPacket != null)
                            packet.Forward = ReceivedLeavePartyOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.ShareExperience:
                    packet = new Packets.Outgoing.ShareExperiencePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedShareExperienceOutgoingPacket != null)
                            packet.Forward = ReceivedShareExperienceOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.DisbandParty:
                    throw new System.NotImplementedException();
                    break;
                case OutgoingPacketType.OpenOwnChannel:
                    packet = new Packets.Outgoing.OpenOwnChannelPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedOpenOwnChannelOutgoingPacket != null)
                            packet.Forward = ReceivedOpenOwnChannelOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.InviteToOwnChannel:
                    packet = new Packets.Outgoing.InviteToOwnChannelPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedInviteToOwnChannelOutgoingPacket != null)
                            packet.Forward = ReceivedInviteToOwnChannelOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.ExcludeFromOwnChannel:
                    packet = new Packets.Outgoing.ExcludeFromOwnChannelPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedExcludeFromOwnChannelOutgoingPacket != null)
                            packet.Forward = ReceivedExcludeFromOwnChannelOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.CancelAttackAndFollow:
                    packet = new Packets.Outgoing.CancelAttackAndFollowPacket(client);
                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedCancelAttackAndFollowOutgoingPacket != null)
                            packet.Forward = ReceivedCancelAttackAndFollowOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.TileUpdate:
                    throw new System.NotImplementedException();
                    break;
                case OutgoingPacketType.RefreshContainer:
                    packet = new Packets.Outgoing.RefreshContainerPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedRefreshContainerOutgoingPacket != null)
                            packet.Forward = ReceivedRefreshContainerOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.RequestOutfit:
                    packet = new Packets.Outgoing.RequestOutfitPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedRequestOutfitOutgoingPacket != null)
                            packet.Forward = ReceivedRequestOutfitOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.ChangeOutfit:
                    packet = new Packets.Outgoing.ChangeOutfitPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedChangeOutfitOutgoingPacket != null)
                            packet.Forward = ReceivedChangeOutfitOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.Mount:
                    packet = new Packets.Outgoing.MountPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedMountOutgoingPacket != null)
                            packet.Forward = ReceivedMountOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.AddVip:
                    packet = new Packets.Outgoing.AddVipPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedAddVipOutgoingPacket != null)
                            packet.Forward = ReceivedAddVipOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.RemoveVip:
                    packet = new Packets.Outgoing.RemoveVipPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedRemoveVipOutgoingPacket != null)
                            packet.Forward = ReceivedRemoveVipOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.EditVip:
                    packet = new Packets.Outgoing.EditVipPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedEditVipOutgoingPacket != null)
                            packet.Forward = ReceivedEditVipOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.BugReport:
                    packet = new Packets.Outgoing.BugReportPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedBugReportOutgoingPacket != null)
                            packet.Forward = ReceivedBugReportOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.RuleViolation:
                    packet = new Packets.Outgoing.RuleViolationPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedRuleViolationOutgoingPacket != null)
                            packet.Forward = ReceivedRuleViolationOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.DebugReport:
                    packet = new Packets.Outgoing.DebugReportPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedDebugReportOutgoingPacket != null)
                            packet.Forward = ReceivedDebugReportOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.RequestQuestLog:
                    packet = new Packets.Outgoing.RequestQuestLogPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedRequestQuestLogOutgoingPacket != null)
                            packet.Forward = ReceivedRequestQuestLogOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.RequestQuestLine:
                    packet = new Packets.Outgoing.RequestQuestLinePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedRequestQuestLineOutgoingPacket != null)
                            packet.Forward = ReceivedRequestQuestLineOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.NewRuleViolation:
                    packet = new Packets.Outgoing.NewRuleViolationPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedNewRuleViolationOutgoingPacket != null)
                            packet.Forward = ReceivedNewRuleViolationOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.RequestItemInfo:
                    packet = new Packets.Outgoing.RequestItemInfoPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedRequestItemInfoOutgoingPacket != null)
                            packet.Forward = ReceivedRequestItemInfoOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.MarketLeave:
                    packet = new Packets.Outgoing.MarketLeavePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedMarketLeaveOutgoingPacket != null)
                            packet.Forward = ReceivedMarketLeaveOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.MarketBrowse:
                    packet = new Packets.Outgoing.MarketBrowsePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedMarketBrowseOutgoingPacket != null)
                            packet.Forward = ReceivedMarketBrowseOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.MarketCreate:
                    packet = new Packets.Outgoing.MarketCreatePacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedMarketCreateOutgoingPacket != null)
                            packet.Forward = ReceivedMarketCreateOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.MarketCancel:
                    packet = new Packets.Outgoing.MarketCancelPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedMarketCancelOutgoingPacket != null)
                            packet.Forward = ReceivedMarketCancelOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.MarketAccept:
                    packet = new Packets.Outgoing.MarketAcceptPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedMarketAcceptOutgoingPacket != null)
                            packet.Forward = ReceivedMarketAcceptOutgoingPacket.Invoke(packet);
                    }
                    break;
                case OutgoingPacketType.AnswerModalDialog:
                    packet = new Packets.Outgoing.AnswerModalDialogPacket(client);

                    if (packet.ParseMessage(msg, PacketDestination.Server))
                    {
                        if (ReceivedAnswerModalDialogOutgoingPacket != null)
                            packet.Forward = ReceivedAnswerModalDialogOutgoingPacket.Invoke(packet);
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
#if DEBUG
            try
            {
                lock (debugLock)
                {
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(System.IO.Path.Combine(Application.StartupPath, "proxy_log.txt"), true);
                    sw.WriteLine(System.DateTime.Now.ToShortDateString() + " " + System.DateTime.Now.ToLongTimeString() + " " + msg + "\nLast received packet types: " + GetLastReceivedPacketTypesString());
                    sw.WriteLine();
                    sw.Close();
                }
            }
            catch
            {
            }
#endif
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