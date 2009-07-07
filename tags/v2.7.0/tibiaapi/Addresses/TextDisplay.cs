namespace Tibia.Addresses
{
    public static class TextDisplay
    {
        /// <summary>
        /// PrintName function call to overwrite
        /// </summary>
        public static uint PrintName = 0x4F0221; // 8.50


        /// <summary>
        /// PrintFPS function call to overwrite
        /// </summary>
        public static uint PrintFPS = 0x459728; // 8.50


        /// <summary>
        /// Offset, if user wants to show FPS
        /// </summary>
        public static uint ShowFPS = 0x630B74; // 8.50

        /// <summary>
        /// PrintText function address
        /// </summary
        public static uint PrintTextFunc = 0x4B0000; // 8.50


        /// <summary>
        /// Conditional check to show FPS
        /// </summary>
        public static uint NopFPS = 0x459664; // 8.50

    }
}

