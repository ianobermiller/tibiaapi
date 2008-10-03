namespace Tibia.Addresses
{
    public static class TextDisplay
    {
        /// <summary>
        /// PrintName function call to overwrite
        /// </summary>
        public static int PrintName = 0x4EF0F1; // 8.31


        /// <summary>
        /// PrintFPS function call to overwrite
        /// </summary>
        public static int PrintFPS = 0x4585E8; // 8.31


        /// <summary>
        /// Offset, if user wants to show FPS
        /// </summary>
        public static int ShowFPS = 0x6289F4; // 8.31

        /// <summary>
        /// PrintText function address
        /// </summary
        public static int PrintTextFunc = 0x4AEB90; // 8.31


        /// <summary>
        /// Conditional check to show FPS
        /// </summary>
        public static int NopFPS = 0x458524; // 8.31

    }
}

