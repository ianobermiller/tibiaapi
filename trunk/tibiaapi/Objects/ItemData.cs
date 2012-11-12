using System;
using System.Collections.Generic;
using Tibia.Packets;

namespace Tibia.Objects
{
    public class ItemData
    {
        public string Name;
        public uint Id;
        public bool Enchantable;
        public double Weight;
        public int LootValue;
        public List<string> DroppedBy;

        public ItemData(string name, uint id, bool enchantable, double weight, int lootValue, List<string> droppedBy)
        {
            Name = name;
            Id = id;
            Enchantable = enchantable;
            Weight = weight;
            LootValue = lootValue;
            DroppedBy = droppedBy;
        }

        public double ValueRatio
        {
            get
            {
                return LootValue / Weight;
            }
        }
    }
}
