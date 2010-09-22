namespace Tibia.Addresses
{
    public static class TextDisplay
    {
        /// <summary>
        /// PrintName function call to overwrite
        /// </summary>
        public static uint PrintName = 0x4F14F3; // 8.62


        /// <summary>
        /// PrintFPS function call to overwrite
        /// </summary>
        public static uint PrintFPS = 0x458778; // 8.62


        /// <summary>
        /// Offset, if user wants to show FPS
        /// </summary>
        public static uint ShowFPS = 0x6358FC; // 8.62

        /// <summary>
        /// PrintText function address
        /// </summary
        public static uint PrintTextFunc = 0x4B0F70; // 8.62


        /// <summary>
        /// Conditional check to show FPS
        /// </summary>
        public static uint NopFPS = 0x4586B4; // 8.62

    }
}

