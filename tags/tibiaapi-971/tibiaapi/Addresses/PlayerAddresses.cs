namespace Tibia.Objects
{
    public partial class AddressesCollection
    {
        public PlayerAddresses Player = new PlayerAddresses();

        /// <summary>
        /// Player memory addresses.
        /// </summary>
        public class PlayerAddresses
        {
            public uint Experience;
            public uint Flags;

            public uint GoToX;
            public uint GoToY;
            public uint GoToZ;

            public uint Id;
            public uint Health;
            public uint HealthMax;

            public uint Level;
            public uint MagicLevel;
            public uint LevelPercent;
            public uint MagicLevelPercent;

            public uint Mana;
            public uint ManaMax;

            public uint Soul;
            public uint Stamina;
            public uint OfflineTraining;
            public uint Capacity;

            public uint Fist;
            public uint Club;
            public uint Sword;
            public uint Axe;
            public uint Distance;
            public uint Shielding;
            public uint Fishing;

            public uint FishingPercent;
            public uint ShieldingPercent;
            public uint DistancePercent;
            public uint AxePercent;
            public uint SwordPercent;
            public uint ClubPercent;
            public uint FistPercent;


            /// <summary>
            /// Total number of equipment slots (accessed 0-9)
            /// </summary>
            public int MaxSlots;
            public uint SlotBegin;
            public uint SlotStep;
            public uint SlotHead;
            public uint SlotNeck;
            public uint SlotBackpack;
            public uint SlotArmor;
            public uint SlotRight;
            public uint SlotLeft;
            public uint SlotLegs;
            public uint SlotFeet;
            public uint SlotRing;
            public uint SlotAmmo;

            public uint DistanceSlotCount;
            public uint DistanceSlotId;

            [System.Obsolete]
            public uint CurrentTileToGo;
            [System.Obsolete]
            public uint TilesToGo;

            /// <summary>
            /// The number of times the player has attacked
            /// </summary>
            public uint AttackCount;

            /// <summary>
            /// The number of times the player has followed
            /// </summary>
            public uint FollowCount;

            public uint RedSquare;
            public uint GreenSquare;
            [System.Obsolete("Not updated for 9.71")]
            public uint WhiteSquare;

            public uint AccessN;
            public uint AccessS;

            public uint TargetId;
            public uint TargetBattlelistId;
            public uint TargetBattlelistType;
            public uint TargetType;

            /// <summary>
            /// address for player Z, used for level spy
            /// </summary>
            public uint Z;
            public uint Y;
            public uint X;


            /// <summary>
            /// XOR key address. It is applied to values such as :
            /// Player.HP, Player.Mana, Player.Capacity
            /// </summary>
            public uint XORKey;
        }
    }
}