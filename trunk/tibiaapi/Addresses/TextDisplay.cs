namespace Tibia.Addresses
{
    public static class TextDisplay
    {
        /// <summary>
        /// PrintName function call to overwrite
        /// </summary>
        public static int PrintName = 0x4EA881;

        /// <summary>
        /// PrintFPS function call to overwrite
        /// </summary>
        public static int PrintFPS = 0x455A38;

        /// <summary>
        /// Offset, if user wants to show FPS
        /// </summary>
        public static int ShowFPS = 0x61F974; // 0 = Don't display 1 = Display

        /// <summary>
        /// PrintText function address
        /// </summary
        public static int PrintTextFunc = 0x4ABAD0;

        /// <summary>
        /// Conditional check to show FPS
        /// </summary>
        public static int NopFPS = 0x455974;
    }
}

