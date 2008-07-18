namespace Tibia.Addresses
{
    public static class TextDisplay
    {
        /// <summary>
        /// PrintName function call to overwrite
        /// </summary>
        public static int PrintName = 0x4EC521; // 8.21


        /// <summary>
        /// PrintFPS function call to overwrite
        /// </summary>
        public static int PrintFPS = 0x456468; // 8.21


        /// <summary>
        /// Offset, if user wants to show FPS
        /// </summary>
        public static int ShowFPS = 0x622974; // 8.21

        /// <summary>
        /// PrintText function address
        /// </summary
        public static int PrintTextFunc = 0x4AC500; // 8.21


        /// <summary>
        /// Conditional check to show FPS
        /// </summary>
        public static int NopFPS = 0x4563A4; // 8.21

    }
}

