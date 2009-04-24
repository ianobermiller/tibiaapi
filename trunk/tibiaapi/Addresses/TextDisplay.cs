namespace Tibia.Addresses
{
    public static class TextDisplay
    {
        /// <summary>
        /// PrintName function call to overwrite
        /// </summary>
        public static uint PrintName = 0x4EFA71; // 8.42


        /// <summary>
        /// PrintFPS function call to overwrite
        /// </summary>
        public static uint PrintFPS = 0x459048; // 8.42


        /// <summary>
        /// Offset, if user wants to show FPS
        /// </summary>
        public static uint ShowFPS = 0x62FA34; // 8.42

        /// <summary>
        /// PrintText function address
        /// </summary
        public static uint PrintTextFunc = 0x4AF8D0; // 8.42


        /// <summary>
        /// Conditional check to show FPS
        /// </summary>
        public static uint NopFPS = 0x458F84; // 8.42

    }
}

