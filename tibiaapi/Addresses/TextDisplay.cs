namespace Tibia.Addresses
{
    public static class TextDisplay
    {
        /// <summary>
        /// PrintName function call to overwrite
        /// </summary>
        public static uint PrintName = 0x4F57E3; // 8.71


        /// <summary>
        /// PrintFPS function call to overwrite
        /// </summary>
        public static uint PrintFPS = 0x45A6C8; // 8.71


        /// <summary>
        /// Offset, if user wants to show FPS
        /// </summary>
        public static uint ShowFPS = 0x63D9FC; // 8.71

        /// <summary>
        /// PrintText function address
        /// </summary
        public static uint PrintTextFunc = 0x4B4D70; // 8.71


        /// <summary>
        /// Conditional check to show FPS
        /// </summary>
        public static uint NopFPS = 0x45A604; // 8.71

    }
}

