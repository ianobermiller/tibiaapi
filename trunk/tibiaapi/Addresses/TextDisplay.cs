namespace Tibia.Addresses
{
    public static class TextDisplay
    {
        /// <summary>
        /// PrintName function call to overwrite
        /// </summary>
        public static uint PrintName = 0x4EF9F1; // 8.41


        /// <summary>
        /// PrintFPS function call to overwrite
        /// </summary>
        public static uint PrintFPS = 0x458BD8; // 8.41


        /// <summary>
        /// Offset, if user wants to show FPS
        /// </summary>
        public static uint ShowFPS = 0x62AA30; // 8.40

        /// <summary>
        /// PrintText function address
        /// </summary
        public static uint PrintTextFunc = 0x4AF480; // 8.41


        /// <summary>
        /// Conditional check to show FPS
        /// </summary>
        public static uint NopFPS = 0x458B14; // 8.41

    }
}

