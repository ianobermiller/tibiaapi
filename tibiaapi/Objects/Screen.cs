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
        #region Private Variables
        Client client;
        Util.Pipe pipe = null;
        #endregion

        #region Constructor
        public Screen(Client c)
        {
            client = c;
            pipe = client.Pipe;
        }
        #endregion

        #region Text Display
        public bool DrawScreenText(string TextName, Location loc, Color color, ClientFont font, string text)
        {
            //Testing that user has given valid values
            if (TextName == string.Empty || loc.X <= 0 || loc.Y <= 0 || text == string.Empty)
                return false;

            pipe.Send(Tibia.Packets.Pipes.DisplayTextPacket.Create(client, TextName, loc, color, font, text));
            return true;
        }

        public bool RemoveScreenText(string TextName)
        {
            //Testing that user has given valid values
            if (TextName == string.Empty)
                return false;

            pipe.Send(Tibia.Packets.Pipes.RemoveTextPacket.Create(client, TextName));
            return true;
        }

        public bool RemoveAllScreenText()
        {
            pipe.Send(Tibia.Packets.Pipes.RemoveAllTextPacket.Create(client));
            return true;
        }

        public bool DrawCreatureText(string creatureName, Location loc, Color color, ClientFont font, string text)
        {
            //Testing that user has given valid values
            if (creatureName == string.Empty || text == string.Empty)
                return false;

            pipe.Send(Tibia.Packets.Pipes.DisplayCreatureTextPacket.Create(client, 0, creatureName, loc, color, font, text));
            return true;
        }

        public bool DrawCreatureText(int CreatureID, Location loc, Color color, ClientFont font, string Text)
        {
            //Testing that user has given valid values
            if (CreatureID == 0 || Text == string.Empty)
                return false;

            pipe.Send(Tibia.Packets.Pipes.DisplayCreatureTextPacket.Create(client, CreatureID, "MyChar", loc, color, font, Text));
            return true;
        }

        public bool UpdateCreatureText(string CreatureName, Location loc, string NewText)
        {
            //Testing that user has given valid values
            if (CreatureName == string.Empty || NewText == string.Empty)
                return false;

            pipe.Send(Tibia.Packets.Pipes.UpdateCreatureTextPacket.Create(client, 0, CreatureName, loc, NewText));
            return true;
        }

        public bool UpdateCreatureText(int CreatureID, Location loc, string NewText)
        {
            //Testing that user has given valid values
            if (CreatureID == 0 || NewText == string.Empty)
                return false;

            pipe.Send(Tibia.Packets.Pipes.UpdateCreatureTextPacket.Create(client, CreatureID, "", loc, NewText));
            return true;
        }

        public bool RemoveCreatureText(string CreatureName)
        {
            //Testing that user has given valid values
            if (CreatureName == string.Empty)
                return false;

            pipe.Send(Tibia.Packets.Pipes.RemoveCreatureTextPacket.Create(client, 0, CreatureName));
            return true;
        }

        public bool RemoveCreatureText(int CreatureID)
        {
            //Testing that user has given valid values
            if (CreatureID == 0)
                return false;

            pipe.Send(Tibia.Packets.Pipes.RemoveCreatureTextPacket.Create(client, CreatureID, ""));
            return true;
        }
        #endregion
    }
}
