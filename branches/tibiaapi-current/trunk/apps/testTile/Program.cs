using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using s = System;
using Tibia;
using Tibia.Objects;

namespace testTile
{
    class Program
    {
        static void Main()
        {
            Client c = Client.GetClients().FirstOrDefault();
            if (c != null)
            {
                begin(c);
            }
        }

        static void begin(Client client)
        {
            while (true)
            {
                var playerTile = client.Map.GetTileWithPlayer();
                s.Console.WriteLine("Player Location: " + client.PlayerLocation.ToString());
                PrintTile(playerTile);


                var nextLocation = playerTile.Location;
                nextLocation.X += 2;
                var nextTile = client.Map.GetTile(nextLocation);
                s.Console.WriteLine("Next Location: " + nextLocation.ToString());
                PrintTile(nextTile);
                s.Console.WriteLine("......read again......");
                s.Console.ReadLine();
                //client.Console.Say("ahmed");
            }
        }

        static void PrintTile(Tile tile)
        {
            s.Console.WriteLine("Location by Map: " + tile.Location.ToString());
            s.Console.WriteLine("Items in Stack:");
            for (int i = 0; i < tile.ObjectCount; i++)
            {
                var obj = tile.Objects[i];
                s.Console.WriteLine("{0}\t{1}\t{2}\t{3}", obj.Id, obj.Data, obj.DataEx, obj.StackOrder);
            }
        }
    }
}
