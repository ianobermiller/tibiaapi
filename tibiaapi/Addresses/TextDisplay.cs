namespace Tibia.Addresses
{
    public static class TextDisplay
    {
        /// <summary>
        /// PrintName function call to overwrite
        /// </summary>
        public static uint PrintName = 0x4F56A3; // 8.57


        /// <summary>
        /// PrintFPS function call to overwrite
        /// </summary>
        public static uint PrintFPS = 0x45A1F8; // 8.57


        /// <summary>
        /// Offset, if user wants to show FPS
        /// </summary>
        public static uint ShowFPS = 0x63DB34; // 8.57

        /// <summary>
        /// PrintText function address
        /// </summary
        public static uint PrintTextFunc = 0x4B4BB0; // 8.57


        /// <summary>
        /// Conditional check to show FPS
        /// </summary>
        public static uint NopFPS = 0x45A134; // 8.57

    }
}

