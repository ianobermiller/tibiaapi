using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Tibia.Constants;

namespace Tibia.Objects
{
    public class Screen
    {
        Client client;

        #region Constructor

        public Screen(Client c)
        {
            client = c;
        }

        #endregion

        #region Text Display

        public bool DrawScreenText(string TextName, Location loc, Color color, ClientFont font, string text)
        {
            //Testing that user has given valid values
            if (TextName == string.Empty || loc.X <= 0 || loc.Y <= 0 || text == string.Empty)
                return false;

            if (client.Dll.Pipe == null)
            {
                client.Dll.InitializePipe();
                client.Dll.PipeIsReady.WaitOne();
            }

            return Packets.Pipes.DisplayTextPacket.Send(client, TextName, loc, color, font, text);
        }

        public bool RemoveScreenText(string textName)
        {
            //Testing that user has given valid values
            if (textName == string.Empty)
                return false;

            if (client.Dll.Pipe == null)
            {
                client.Dll.InitializePipe();
                client.Dll.PipeIsReady.WaitOne();
            }

            return Packets.Pipes.RemoveTextPacket.Send(client, textName);
        }

        public bool RemoveAllScreenText()
        {
            if (client.Dll.Pipe == null)
            {
                client.Dll.InitializePipe();
                client.Dll.PipeIsReady.WaitOne();
            }

            return Packets.Pipes.RemoveAllTextPacket.Send(client);
        }

        public bool DrawCreatureText(string creatureName, Location loc, Color color, ClientFont font, string text)
        {
            //Testing that user has given valid values
            if (creatureName == string.Empty || text == string.Empty)
                return false;

            if (client.Dll.Pipe == null)
            {
                client.Dll.InitializePipe();
                client.Dll.PipeIsReady.WaitOne();
            }

            return Packets.Pipes.DisplayCreatureTextPacket.Send(client, 0, creatureName, loc, color, font, text);
        }

        public bool DrawCreatureText(int CreatureID, Location loc, Color color, ClientFont font, string Text)
        {
            //Testing that user has given valid values
            if (CreatureID == 0 || Text == string.Empty)
                return false;

            if (client.Dll.Pipe == null)
            {
                client.Dll.InitializePipe();
                client.Dll.PipeIsReady.WaitOne();
            }

            return Packets.Pipes.DisplayCreatureTextPacket.Send(client, CreatureID, "MyChar", loc, color, font, Text);
        }

        public bool UpdateCreatureText(string CreatureName, Location loc, string NewText)
        {
            //Testing that user has given valid values
            if (CreatureName == string.Empty || NewText == string.Empty)
                return false;

            if (client.Dll.Pipe == null)
            {
                client.Dll.InitializePipe();
                client.Dll.PipeIsReady.WaitOne();
            }

            return Packets.Pipes.UpdateCreatureTextPacket.Send(client, 0, CreatureName, loc, NewText);
        }

        public bool UpdateCreatureText(int CreatureID, Location loc, string NewText)
        {
            //Testing that user has given valid values
            if (CreatureID == 0 || NewText == string.Empty)
                return false;

            if (client.Dll.Pipe == null)
            {
                client.Dll.InitializePipe();
                client.Dll.PipeIsReady.WaitOne();
            }

            return Packets.Pipes.UpdateCreatureTextPacket.Send(client, CreatureID, "", loc, NewText);
        }

        public bool RemoveCreatureText(string CreatureName)
        {
            //Testing that user has given valid values
            if (CreatureName == string.Empty)
                return false;

            if (client.Dll.Pipe == null)
            {
                client.Dll.InitializePipe();
                client.Dll.PipeIsReady.WaitOne();
            }

            return Packets.Pipes.RemoveCreatureTextPacket.Send(client, 0, CreatureName);
        }

        public bool RemoveCreatureText(int CreatureID)
        {
            //Testing that user has given valid values
            if (CreatureID == 0)
                return false;

            if (client.Dll.Pipe == null)
            {
                client.Dll.InitializePipe();
                client.Dll.PipeIsReady.WaitOne();
            }

            return Packets.Pipes.RemoveCreatureTextPacket.Send(client, CreatureID, "");
        }
        #endregion
    }
}
