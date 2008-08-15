namespace Tibia.Addresses
{
    public static class TextDisplay
    {
        /// <summary>
        /// PrintName function call to overwrite
        /// </summary>
        public static int PrintName = 0x4EC5E1; // 8.22


        /// <summary>
        /// PrintFPS function call to overwrite
        /// </summary>
        public static int PrintFPS = 0x4563C8; // 8.22


        /// <summary>
        /// Offset, if user wants to show FPS
        /// </summary>
        public static int ShowFPS = 0x624974; // 8.22

        /// <summary>
        /// PrintText function address
        /// </summary
        public static int PrintTextFunc = 0x4AC490; // 8.22


        /// <summary>
        /// Conditional check to show FPS
        /// </summary>
        public static int NopFPS = 0x456304; // 8.22

    }
}

