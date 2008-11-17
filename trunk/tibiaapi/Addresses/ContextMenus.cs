using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Addresses
{
    public static class ContextMenus
    {
        /// <summary>
        /// Add Context Menu Function
        /// 
        public static uint AddContextMenuPtr = 0x450AA0;

        /// <summary>
        /// Menu Events handler
        /// 
        public static uint OnClickContextMenuVf = 0x5AF950;

        /// <summary>
        /// Called only when you right-click on yourself
        /// To be overwritten.
        /// 
        public static uint AddSetOutfitContextMenu = 0x4519D2;

        /// <summary>
        /// Called only when you right-click on other players(invite or revoke invitation)
        /// To be overwritten.
        /// 
        public static uint AddPartyActionContextMenu = 0x451A23;

        /// <summary>
        /// Called only when you right-click on any creature
        /// To be overwritten.
        /// 
        public static uint AddCopyNameContextMenu = 0x451A3A;


    }
}
