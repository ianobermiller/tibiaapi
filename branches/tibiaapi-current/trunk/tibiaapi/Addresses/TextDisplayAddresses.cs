namespace Tibia.Objects
{
    public partial class AddressesCollection
    {
        public TextDisplayAddresses TextDisplay = new TextDisplayAddresses();

        public class TextDisplayAddresses
        {
            /// <summary>
            /// PrintName function call to overwrite
            /// </summary>
            public uint PrintName;


            /// <summary>
            /// PrintFPS function call to overwrite
            /// </summary>
            public uint PrintFPS;


            /// <summary>
            /// Offset, if user wants to show FPS
            /// </summary>
            public uint ShowFPS;

            /// <summary>
            /// PrintText function address
            /// </summary
            public uint PrintTextFunc;


            /// <summary>
            /// Conditional check to show FPS
            /// </summary>
            public uint NopFPS;

        }
    }
}