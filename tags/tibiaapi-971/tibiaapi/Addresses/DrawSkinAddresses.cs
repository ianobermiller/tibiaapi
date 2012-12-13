namespace Tibia.Objects
{
    public partial class AddressesCollection
    {
        public DrawSkinAddresses DrawSkin = new DrawSkinAddresses();

        public class DrawSkinAddresses
        {
            public uint DrawSkinFunc;
        }
    }
}