namespace Tibia.Objects
{
    public partial class AddressesCollection
    {
        public HotkeyAddresses Hotkey = new HotkeyAddresses();

        public class HotkeyAddresses
        {
            public uint SendAutomaticallyStart;
            public uint SendAutomaticallyStep;

            public uint TextStart;
            public uint TextStep;

            public uint ObjectStart;
            public uint ObjectStep;

            public uint ObjectUseTypeStart;
            public uint ObjectUseTypeStep;

            public uint MaxHotkeys;
        }
    }
}