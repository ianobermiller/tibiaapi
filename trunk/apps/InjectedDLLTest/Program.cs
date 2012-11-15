using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tibia;
using Tibia.Objects;
using s=System;

namespace InjectedDLLTest
{
    class Program
    {
        static Tibia.Objects.Client client;
        static void Main(string[] args)
        {
            client = Client.GetClients().FirstOrDefault();
            if (client != null)
            {
                Test();
            }
        }

        static void Test()
        {
            s.Console.WriteLine("Extracting dll.");
            client.Dll.Extract();

            s.Console.Write("Initializing pipe...");
            client.Dll.InitializePipe();
            client.Dll.PipeIsReady.WaitOne();
            client.Dll.Pipe.OnSocketRecv += new Tibia.Packets.Pipe.PipeListener(Pipe_OnSocketRecv);
            client.Dll.Pipe.OnSocketSend += new Tibia.Packets.Pipe.PipeListener(Pipe_OnSocketSend);
            s.Console.Write("done.\n");

            s.Console.Write("Adding some text to screen...test1, test2, test3, test4...");
            client.Screen.DrawScreenText("test1", new Location(20, 20, 0), System.Drawing.Color.Red, Tibia.Constants.ClientFont.Normal, "test1");
            client.Screen.DrawScreenText("test2", new Location(20, 40, 0), System.Drawing.Color.Green, Tibia.Constants.ClientFont.NormalBorder, "test2");
            client.Screen.DrawScreenText("test3", new Location(20, 60, 0), System.Drawing.Color.Blue, Tibia.Constants.ClientFont.Small, "test3");
            client.Screen.DrawScreenText("test4", new Location(20, 80, 0), System.Drawing.Color.Blue, Tibia.Constants.ClientFont.Weird, "test4");
            s.Console.WriteLine("hit any key after verifying...");
            s.Console.ReadKey();

            s.Console.Write("Adding some text to creatures...");
            while (!client.LoggedIn) { System.Threading.Thread.Sleep(100); }
            client.BattleList.GetCreatures().Foreach(c => client.Screen.DrawCreatureText(c.Id, new Location(-10, 0, 0), System.Drawing.Color.OrangeRed, Tibia.Constants.ClientFont.NormalBorder, c.Name[0].ToString()));
            s.Console.WriteLine("hit any key after verifying...");
            s.Console.ReadKey();


            s.Console.Write("Adding some context menus...");
            client.ContextMenu.Click += new ContextMenu.ContextMenuEvent(ContextMenu_Click);
            client.ContextMenu.AddContextMenu(11, "SetOutfit 1", Tibia.Constants.ContextMenuType.SetOutfitContextMenu, false);
            client.ContextMenu.AddContextMenu(12, "SetOutfit 2", Tibia.Constants.ContextMenuType.SetOutfitContextMenu, true);
            client.ContextMenu.AddContextMenu(13, "CopyName 1", Tibia.Constants.ContextMenuType.CopyNameContextMenu, false);
            client.ContextMenu.AddContextMenu(14, "CopyName 2", Tibia.Constants.ContextMenuType.CopyNameContextMenu, true);
            client.ContextMenu.AddContextMenu(41, "TradeWith 1", Tibia.Constants.ContextMenuType.TradeWithContextMenu, false);
            client.ContextMenu.AddContextMenu(42, "TradeWith 2", Tibia.Constants.ContextMenuType.TradeWithContextMenu, true);
            client.ContextMenu.AddContextMenu(51, "Look 1", Tibia.Constants.ContextMenuType.LookContextMenu, false);
            client.ContextMenu.AddContextMenu(52, "Look 2", Tibia.Constants.ContextMenuType.LookContextMenu, true);
            s.Console.WriteLine("hit any key after verifying...");
            s.Console.ReadKey();

            s.Console.Write("Adding some skins...");
            Tibia.Packets.Pipes.AddSkinPacket.Send(client, 1, 20, 20, 50, 50, 0x13);
            Tibia.Packets.Pipes.AddSkinPacket.Send(client, 2, 70, 70, 50, 50, 0x14);
            s.Console.WriteLine("hit any key after verifying...");
            s.Console.ReadKey();


            s.Console.Write("Adding some icons...gold coins...crystal coins...blue gem...");
            client.Icon.Click += new Icon.IconEvent(Icon_Click);
            client.Icon.AddIcon(1, 300, 300, 64, 3031, 56, Tibia.Constants.ClientFont.NormalBorder, s.Drawing.Color.Goldenrod);
            client.Icon.AddIcon(2, 500, 300, 32, 3043, 100, Tibia.Constants.ClientFont.Normal, s.Drawing.Color.LightBlue);
            client.Icon.AddIcon(3, 500, 400, 64, 3041, 0, Tibia.Constants.ClientFont.Weird, s.Drawing.Color.Lavender);
            s.Console.WriteLine("hit any key after verifying...");
            s.Console.ReadKey();

        }

        static void Icon_Click(int iconId)
        {
            s.Console.Write("Icon click:  ");
            switch (iconId)
            {
                case 1:
                    s.Console.WriteLine("gold coins");
                    break;
                case 2:
                    s.Console.WriteLine("crystal coins");
                    break;
                case 3:
                    s.Console.WriteLine("blue gem");
                    break;
            }
        }

        
        static void Pipe_OnSocketRecv(Tibia.Packets.NetworkMessage msg)
        {
            s.Console.ForegroundColor = ConsoleColor.Green;
            s.Console.WriteLine("Packet received:");
            s.Console.WriteLine(msg.Data.ToHexString());
            s.Console.ResetColor();
        }
        static void Pipe_OnSocketSend(Tibia.Packets.NetworkMessage msg)
        {
            s.Console.ForegroundColor = ConsoleColor.Red;
            s.Console.WriteLine("Packet sent:");
            s.Console.WriteLine(msg.Data.ToHexString());
            s.Console.ResetColor();
        }
        

        static void ContextMenu_Click(int eventID)
        {
            s.Console.Write("Context Menu click:  ");
            switch (eventID)
            {
                case 11:
                    s.Console.WriteLine("SetOutfit 1");
                    break;
                case 12:
                    s.Console.WriteLine("SetOutfit 2");
                    break;
                case 13:
                    s.Console.WriteLine("CopyName 1");
                    break;
                case 14:
                    s.Console.WriteLine("CopyName 2");
                    break;
                case 41:
                    s.Console.WriteLine("TradeWith 1");
                    break;
                case 42:
                    s.Console.WriteLine("TradeWith 2");
                    break;
                case 51:
                    s.Console.WriteLine("Look 1");
                    break;
                case 52:
                    s.Console.WriteLine("Look 2");
                    break;
            }

        }

    }
}
