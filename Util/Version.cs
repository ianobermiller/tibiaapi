using System;
using System.Collections.Generic;
using System.Text;

namespace Tibia
{
    public class Version
    {
        public static void Set(string version)
        {
            if (version == "8.00")
            {
                #region 8.00 Addresses
                Addresses.BattleList.Start = 0x60EB30;
                Addresses.BattleList.End = 0x6148F0;
                Addresses.BattleList.Step_Creatures = 0xA0;
                Addresses.BattleList.Max_Creatures = 100;

                Addresses.Client.XTeaKey = 0x7637AC; 
                //Addresses.Client.FrameRate = 0x7661F4; 
                Addresses.Client.MultiClient = 0x4EFB71; 
                Addresses.Client.MultiClientValue = 0xEB; 
                Addresses.Client.Status = 0x766DF8;           
                Addresses.Client.FollowMode = 0x763BD0; 
                Addresses.Client.AttackMode = 0x763BD4; 
                Addresses.Client.SafeMode = 0x763BCC; 
                Addresses.Client.MouseCursor = 0x751BD8;
                Addresses.Client.CurrentWindow = 0x6198B4;    
                Addresses.Client.LastMSGAuthor = 0x768680; 
                Addresses.Client.LastMSGText = 0x7686A8; 
                Addresses.Client.Statusbar_Text = 0x00768458;
                Addresses.Client.Statusbar_Time = 0x00768454;
                Addresses.Client.Click_Id = 0x766E94;
                Addresses.Client.Click_Count = 0x766E98;
                Addresses.Client.Click_Z = 0x766E2C;
                Addresses.Client.See_Id = 0x766EA0; 
                Addresses.Client.See_Count = 0x766EA4; 
                Addresses.Client.See_Z = 0x766E00; 
                Addresses.Client.LoginServerStart = 0x75EAE8; 
                Addresses.Client.Step_LoginServer = 112;
                Addresses.Client.Distance_Port = 100;
                Addresses.Client.Max_LoginServers = 10;
                Addresses.Client.RSA = 0x593610;
                Addresses.Client.LoginCharList = 0x766DBC;
                Addresses.Client.LoginSelectedChar = 0x766DB8;

                Addresses.Container.Start = 0x617000;
                Addresses.Container.End = 0x618EC0;
                Addresses.Container.Step_Container = 492;                    
                Addresses.Container.Step_Slot = 12;                   
                Addresses.Container.Max_Containers = 16;
                Addresses.Container.Max_Stack = 100;
                Addresses.Container.Distance_IsOpen = 0;   
                Addresses.Container.Distance_Id = 4;       
                Addresses.Container.Distance_Name = 16;    
                Addresses.Container.Distance_Volume = 48;  
                Addresses.Container.Distance_Amount = 56;  
                Addresses.Container.Distance_Item_Id = 60;                  
                Addresses.Container.Distance_Item_Count = 64;   
                            
                Addresses.Creature.Distance_Id = 0;
                Addresses.Creature.Distance_Type = 3;
                Addresses.Creature.Distance_Name = 4;
                Addresses.Creature.Distance_X = 36;
                Addresses.Creature.Distance_Y = 40;
                Addresses.Creature.Distance_Z = 44;
                Addresses.Creature.Distance_ScreenOffsetHoriz = 48;
                Addresses.Creature.Distance_ScreenOffsetVert = 52;
                Addresses.Creature.Distance_IsWalking = 76;
                Addresses.Creature.Distance_WalkSpeed = 140;
                Addresses.Creature.Distance_Direction = 80;
                Addresses.Creature.Distance_IsVisible = 144;
                Addresses.Creature.Distance_BlackSquare = 128;
                Addresses.Creature.Distance_Light = 120;
                Addresses.Creature.Distance_LightColor = 124;
                Addresses.Creature.Distance_HPBar = 136;
                Addresses.Creature.Distance_Skull = 148;
                Addresses.Creature.Distance_Party = 152;
                Addresses.Creature.Distance_Outfit = 96;
                Addresses.Creature.Distance_Color_Head = 100;
                Addresses.Creature.Distance_Color_Body = 104;
                Addresses.Creature.Distance_Color_Legs = 108;
                Addresses.Creature.Distance_Color_Feet = 112;
                Addresses.Creature.Distance_Addon = 116;

                Addresses.Map.MapPointer = 0x61E408;     
                Addresses.Map.Step_Square = 172;          
                Addresses.Map.Step_Square_Object = 12;
                Addresses.Map.Distance_Square_ObjectCount = 0;
                Addresses.Map.Distance_Square_Objects = 4;
                Addresses.Map.Distance_Object_Id = 0;        
                Addresses.Map.Distance_Object_Data = 4;      
                Addresses.Map.Distance_Object_Data_Ex = 8;      
                Addresses.Map.Max_Squares = 2016;
                Addresses.Map.Max_Square_Objects = 13;
                Addresses.Map.Z_Axis_Default = 7; 

                Addresses.Player.Flags = 0x60EA58; 
                Addresses.Player.Exp = 0x60EAC4;
                Addresses.Player.Id = Addresses.Player.Exp + 12;
                Addresses.Player.HP = Addresses.Player.Exp + 8;
                Addresses.Player.HP_Max = Addresses.Player.Exp + 4;
                Addresses.Player.Level = Addresses.Player.Exp - 4;
                Addresses.Player.MagicLevel = Addresses.Player.Exp - 8;
                Addresses.Player.Level_Percent = Addresses.Player.Exp - 12;
                Addresses.Player.MagicLevel_Percent = Addresses.Player.Exp - 16;
                Addresses.Player.Mana = Addresses.Player.Exp - 20;
                Addresses.Player.Mana_Max = Addresses.Player.Exp - 24;
                Addresses.Player.Soul = Addresses.Player.Exp - 28;
                Addresses.Player.Stamina = Addresses.Player.Exp - 32;
                Addresses.Player.Cap = Addresses.Player.Exp - 36; 
                Addresses.Player.Fist_Percent = 0x60EA5C;
                Addresses.Player.Club_Percent = Addresses.Player.Fist_Percent + 4;
                Addresses.Player.Sword_Percent = Addresses.Player.Fist_Percent + 8;
                Addresses.Player.Axe_Percent = Addresses.Player.Fist_Percent + 12;
                Addresses.Player.Distance_Percent = Addresses.Player.Fist_Percent + 16;
                Addresses.Player.Shielding_Percent = Addresses.Player.Fist_Percent + 20;
                Addresses.Player.Fishing_Percent = Addresses.Player.Fist_Percent + 24;
                Addresses.Player.Fist = Addresses.Player.Fist_Percent + 28;
                Addresses.Player.Club = Addresses.Player.Fist_Percent + 32;
                Addresses.Player.Sword = Addresses.Player.Fist_Percent + 36;
                Addresses.Player.Axe = Addresses.Player.Fist_Percent + 40;
                Addresses.Player.Distance = Addresses.Player.Fist_Percent + 44;
                Addresses.Player.Shielding = Addresses.Player.Fist_Percent + 48;
                Addresses.Player.Fishing = Addresses.Player.Fist_Percent + 52;             
                Addresses.Player.Slot_Head = 0x616F88;
                Addresses.Player.Slot_Neck = Addresses.Player.Slot_Head + 12;
                Addresses.Player.Slot_Backpack = Addresses.Player.Slot_Head + 24;
                Addresses.Player.Slot_Armor = Addresses.Player.Slot_Head + 36;
                Addresses.Player.Slot_Right = Addresses.Player.Slot_Head + 48;
                Addresses.Player.Slot_Left = Addresses.Player.Slot_Head + 60;
                Addresses.Player.Slot_Legs = Addresses.Player.Slot_Head + 72;
                Addresses.Player.Slot_Feet = Addresses.Player.Slot_Head + 84;
                Addresses.Player.Slot_Ring = Addresses.Player.Slot_Head + 96;
                Addresses.Player.Slot_Ammo = Addresses.Player.Slot_Head + 108;     
                Addresses.Player.Distance_Slot_Count = 4;
                Addresses.Player.Slot_Right_Count = Addresses.Player.Slot_Right + 4;
                Addresses.Player.Slot_Left_Count = Addresses.Player.Slot_Left + 4;
                Addresses.Player.Slot_Ammo_Count = Addresses.Player.Slot_Ammo + 4; 
                Addresses.Player.GoTo_X = 0x60EB10;
                Addresses.Player.GoTo_Y = Addresses.Player.GoTo_X - 4;
                Addresses.Player.GoTo_Z = Addresses.Player.GoTo_X - 8;           
                Addresses.Player.RedSquare = 0x60EA9C;
                Addresses.Player.GreenSquare = Addresses.Player.RedSquare - 4;
                Addresses.Player.WhiteSquare = Addresses.Player.GreenSquare - 8;      
                Addresses.Player.AccessN = 0x766DF4;          
                Addresses.Player.AccessS = 0x766DC4;          
                Addresses.Player.Target_ID = 0x60EA9C;        
                Addresses.Player.Target_Type = 0x60EA9F;      
                Addresses.Player.Target_BList_ID = 0x60EA94;  
                Addresses.Player.Target_BList_Type = 0x60EA97;

                Addresses.Vip.Start = 0x60C7F0;
                Addresses.Vip.End = 0x60C840;
                Addresses.Vip.Step_Players = 0x2C;
                Addresses.Vip.Max_Players = 100;
                Addresses.Vip.Distance_Id = 0;
                Addresses.Vip.Distance_Name = 4;
                Addresses.Vip.Distance_Status = 34; 
                Addresses.Vip.Distance_Icon = 40;
                #endregion
            }
        }
    }
}
