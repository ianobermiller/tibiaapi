namespace Tibia.Addresses
{
    public static class TextDisplay
    {
        /// <summary>
        /// PrintName function call to overwrite
        /// </summary>
        public static uint PrintName = 0x4F0753; // 8.61


        /// <summary>
        /// PrintFPS function call to overwrite
        /// </summary>
        public static uint PrintFPS = 0x457C28; // 8.61


        /// <summary>
        /// Offset, if user wants to show FPS
        /// </summary>
        public static uint ShowFPS = 0x63287C; // 8.61

        /// <summary>
        /// PrintText function address
        /// </summary
        public static uint PrintTextFunc = 0x4B02B0; // 8.61


        /// <summary>
        /// Conditional check to show FPS
        /// </summary>
        public static uint NopFPS = 0x457B64; // 8.61

    }
}

