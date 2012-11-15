namespace Tibia.Objects
{
    public partial class AddressesCollection
    {
        public CreatureAddresses Creature = new CreatureAddresses();

        /// <summary>
        /// Distances for creatures.
        /// </summary>
        public class CreatureAddresses
        {
            public uint DistanceId;
            public uint DistanceType;
            public uint DistanceName;

            public uint DistanceX;
            public uint DistanceY;
            public uint DistanceZ;
            public uint DistanceScreenOffsetHoriz;
            public uint DistanceScreenOffsetVert;

            public uint DistanceFaceDirection;
            public uint DistanceIsWalking;
            public uint DistanceWalkDirection;

            public uint DistanceOutfit;
            public uint DistanceColorHead;
            public uint DistanceColorBody;
            public uint DistanceColorLegs;
            public uint DistanceColorFeet;
            public uint DistanceAddon;
            public uint DistanceMountId;

            public uint DistanceLight;
            public uint DistanceLightColor;
            public uint DistanceLightPattern;
            public uint DistanceBlackSquare;
            public uint DistanceHPBar;
            public uint DistanceWalkSpeed;
            public uint DistanceIsVisible;

            public uint DistanceSkull;
            public uint DistanceParty;
            public uint DistanceWarIcon;
            public uint DistanceIsBlocking;
        }
    }
}