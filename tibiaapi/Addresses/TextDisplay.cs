namespace Tibia.Addresses
{
    public static class TextDisplay
    {
        /// <summary>
        /// PrintName function call to overwrite
        /// </summary>
        public static int PrintName = 0x4EF161; // 8.40


        /// <summary>
        /// PrintFPS function call to overwrite
        /// </summary>
        public static int PrintFPS = 0x458648; // 8.40


        /// <summary>
        /// Offset, if user wants to show FPS
        /// </summary>
        public static int ShowFPS = 0x629A34; // 8.40

        /// <summary>
        /// PrintText function address
        /// </summary
        public static int PrintTextFunc = 0x4AEC00; // 8.40


        /// <summary>
        /// Conditional check to show FPS
        /// </summary>
        public static int NopFPS = 0x458584; // 8.40

    }
}

