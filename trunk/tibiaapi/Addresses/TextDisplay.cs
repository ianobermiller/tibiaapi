namespace Tibia.Addresses
{
    public static class TextDisplay
    {
        /// <summary>
        /// PrintName function call to overwrite
        /// </summary>
        public static int PrintName = 0x4E228A;

        /// <summary>
        /// PrintFPS function call to overwrite
        /// </summary>
        public static int PrintFPS = 0x44E753;

        /// <summary>
        /// Offset, if user wants to show FPS
        /// </summary>
        public static int ShowFPS = 0x611874; // 0 = Don't display 1 = Display

        /// <summary>
        /// PrintText function address
        /// </summary
        public static int PrintTextFunc = 0x4A3C00;

        /// <summary>
        /// Conditional check to show FPS
        /// </summary>
        public static int NopFPS = 0x44E68F;
    }
}

