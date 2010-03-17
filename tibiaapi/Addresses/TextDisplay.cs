namespace Tibia.Addresses
{
    public static class TextDisplay
    {
        /// <summary>
        /// PrintName function call to overwrite
        /// </summary>
        public static uint PrintName = 0x4F5133; // 8.55


        /// <summary>
        /// PrintFPS function call to overwrite
        /// </summary>
        public static uint PrintFPS = 0x45A058; // 8.55


        /// <summary>
        /// Offset, if user wants to show FPS
        /// </summary>
        public static uint ShowFPS = 0x63AF94; // 8.55

        /// <summary>
        /// PrintText function address
        /// </summary
        public static uint PrintTextFunc = 0x44D49C; // 8.55


        /// <summary>
        /// Conditional check to show FPS
        /// </summary>
        public static uint NopFPS = 0x459F94; // 8.55

    }
}

