namespace Tibia.Addresses
{
    public static class ContextMenus
    {
        /// <summary>
        /// The function used to add a context menu item.
        /// </summary>
        public static uint AddContextMenuPtr = 0x452320; //8.60

        /// <summary>
        /// The function used to process Tibia's "OnClick" events,
        /// that is, the events issued when you click a context
        /// menu item.
        /// </summary>
        public static uint OnClickContextMenuPtr = 0x44E960; //8.60

        /// <summary>
        /// The address in the virtual function table where the
        /// "OnClick" event for context menu items is.
        /// This address should be overwritten with a raw address,
        /// that is, the unmodified address of the function to be
        /// called when a "OnClick" event is issued.
        /// Moreover, it is HIGHLY (by highly I mean DO IT if you
        /// want to avoid sure trouble) recommended that you call
        /// OnClickContextMenuPtr from your hooked function to
        /// process standard Tibia events.
        /// </summary>
        public static uint OnClickContextMenuVf = 0x5BD9C0; //8.60

        /// <summary>
        /// The "Set Outfit" context menu item function call.
        /// Overwrite it if you want to add a context menu item
        /// that is specific to your character
        /// </summary>
        public static uint AddSetOutfitContextMenu = 0x453283; //8.60

        /// <summary>
        /// The "Invite to Party" / "Leave Party" context menu
        /// item function call.
        /// Overwrite it if you want to add a context menu item
        /// that is specific to other players.
        /// </summary>
        public static uint AddPartyActionContextMenu = 0x4536AC; //8.60

        /// <summary>
        /// The "Copy Name" context menu item function call.
        /// Overwrite it if you want to add a context menu item
        /// that is specific to creatures (including you and monsters).
        /// </summary>
        public static uint AddCopyNameContextMenu = 0x453790; //8.60

        public static uint AddTradeWithContextMenu = 0x452EC9; //8.60

        /// <summary>
        /// The "Look" context menu item function call.
        /// Overwrite it if you want to add a context menu item
        /// that always appears on game window or inventory item clicks.
        /// </summary>
        public static uint AddLookContextMenu = 0x452D7F; //8.60

    }
}
