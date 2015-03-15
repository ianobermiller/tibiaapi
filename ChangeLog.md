# v2.3 #

## Renamed ##
  * None!

## Added ##
  * Displaying text directly in the client area using an injected DLL (see the new Smart Team Marker Project and the Screen object for details)
  * All the features needed for auto-login (which will be coming soon), including client dimensions, dialog info, and username/password setting
  * Multithreaded timer in Util.Timer
  * WPF Client Chooser
  * ToStringDeep debug helper (try it on a complex object like Player)
  * Tags for all the old versions
  * NPCChannel chat type
  * TradeBoxOpenPacket (untested)
  * Demon hunter outfits

## Fixed ##
  * Updated to Tibia version 8.21, older versions can be found in Version.cs
  * Clients with different executable names (ie. not named Tibia.exe) are now recognized
  * Updated all of the spells
  * Refactored the repository structure to be more typical
  * Pipes are now asynchronous
  * Inventory.Stack() has a short delay
  * Fixed bugs in the Website class relating to spaces and multiple requests
  * Bug in map object

# v2.2 #

## Renamed ##
  * LoggedIn -> OnLogin
  * LoggedOut -> OnLogOut
  * Constants.ItemList -> Constants.ItemLists
  * ChatMessage fields: type->Type, text->Text, recipient->Recipient, channel->Channel
  * ChannelGM -> ChannelRedAnonymous
  * Many Client.Something() functions were converted to properties, so drop the ()
  * All spells are now in Constants/Spells.cs
  * Several packet related conversion functions are now extensions
  * All the item constants are now item objects, so to get the ID, you call item.ID. However, most functions have been modified to accept an Item as well as an id, so that is not usually necessary.

## Added ##
  * Many New Packets
  * Tons of new items
  * More Tile ID's for holes and stairs
  * More outfits
  * Website.GuildMembers function gets a list of guild members
  * Enhanced ClientChooser that allows you to open a client from the default location or choose a location, and also easily handles Open Tibia servers
  * A list of all the items, so you can get information about an item by getting the properties by Constants.AllItems[id](id.md).Property
  * DatReader now works and reads from memory
  * Inventory.UseItem(Id, Location) and Item.Use(Location) that automatically get the tile info for you
  * Support for any kind of TransformingItem, like enchanted spears
  * Util.Pipe for communicating with injected DLLs
  * WhoIsOnline lookup
  * PacketBuilder for working with packets
  * Crash notification and protection to the proxy
  * OnExit event when client is closed
  * Simple IRC client capabilities
  * Battlelist.GetCreaturesAttacking
  * Container.Rename
  * Client.InjectDLL
  * Client.Open can take parameters
  * Client.IsTopMost
  * Client.Close()
  * Client.IsVisible
  * Creature.Approach()
  * Creature.IsSelf()
  * Guild details to LookupPlayer
  * Visual Basic code samples
  * Util.Timer, a timer whose executions don't overlap (uses thread locks)

## Fixed ##
  * The proxy can handle combined and split packets
  * The proxy plays well with OT servers
  * The proxy doesn't crash if you enter a wrong password and try again
  * OnLogIn is called 250ms after the packet is recieved to allow the client to initialize
  * Character encoding for packets can handle international characters
  * Client.Fish()
  * Client.MakeRune()
  * The MapViewer is now a control you can drag into your project, and you add markers by adding them to MapViewer.Markers
  * Fixed Lifefluid and Manafluid items
  * Cleaned up the Map class and added some new GetTile/Square functions

# v2.1 #

## Renamed ##
  * Proxy.ServerRecieve -> Proxy.ReceiveFromServer
  * Proxy.ClientRecieve -> Proxy.ReceiveFromClient
  * Tibia.ClientChooser -> Tibia.Util.ClientChooser
  * Tibia.MapViewer -> Tibia.Util.MapViewer
  * Constants.SpeechChannel -> Packets.ChatChannel
  * Constants.SpeechType -> Packets.ChatType
  * Constants.MouseCursor -> Constants.ActionState (thanks adnimo)
  * Player.GoTo(x) -> Player.GoTo = x

## Added ##
  * Added the following packet objects and their corresponding listeners in Proxy
    * CharListPacket (no listener, for proxy connections)
    * StatusMessagePacket
    * ChatMessagePacket
    * AnimatedTextPacket
    * ProjectilePacket
    * CreatureHealthPacket
    * VipLoginPacket
    * PlayerSpeechPacket
  * Added Website class with function LookupPlayer for accessing the Tibia website (incomplete)
  * Added StartProxy and SetOT overloads to Client for connecting to OT's.
  * Added Proxy.Connected, Proxy.SendToClient, Proxy.SendToServer
  * Added functions to Location: Parse, ToString, Equals, IsValid, GetInvalid, and ToBytes.
  * Added GetCreatureOnLoc to BattleList.
  * Added Smart Packet Analyzer, Smart IPChanger, and Smart Runemaker to the svn in the apps/ directory.
  * Added Client.Fish() function.
  * Added potions to the item lists.
  * Added functions to Packet for converting bytes to ASCII.
  * Added GetRelativeLocation and GetTileInfo to Map.

## Fixed ##
  * Fixed Client to use the proxy if it is enabled.
  * Fixed the Proxy to handle multiple packets in one receive call.
  * Fixed the Proxy to be thread safe and use queues for processing and sending packets.
  * Fixed the Proxy to handle logging out correctly and also relogging in.
  * Fixed the Proxy to use the first open port, allows for using multiple clients.
  * Fixed FPS.
  * Fixed Player.Stop() to stop even when map walking.
  * Fixed rune list with the latest spells.
  * Fixed GreatManaPotion id.

# v2.0 #

  * Added Proxy with sending, recieving, and blocking of packets. It is completely asynchronous and multi-client compatible.(see example code below)
  * Added MapViewer
  * Added GetTiles function to the Map object. Has the option to scan only the current floor. Useful for a fisher.
  * Added getting/setting of client FPS
  * Added a mouse hook
  * Added GetTilesWithinView method to Map
  * Added Map Merger utility
  * Fixed: MapViewer no longer flickers
  * Fixed: item.OpenContainer
  * Fixed: Changed several Client methods to properties (LoggedIn, FPS, RSA, IsActive, Title, see documentation for all of them)
  * Fixed: inventory.FindItem now has the option to check slots.
  * Fixed: Merged Slot object with Inventory, moved GetSlot from client to inventory
  * Fixed: Moved hotkey functions to inventory.UseItem, UseItemOnSelf, and UseItem(id, creatureId)
  * Fixed: Renamed item.Id to Item.Number, item.Id is now the tile id
  * Fixed using an Item on the ground (ex. fishing)
  * Fixed: ClientChooser smart option was not being recognized.

# v1.4 #

  * Added name and floor spy functions to Map (ShowNames and ShowFloor)
  * Added UseOnSelf function to Item
  * Added GetPartyMembers to Battlelist, including a function that gets all party members with HP bar less than or equal to the specified percentage
  * Added ExpLeft function to Player
  * Fixed Use functions in Item to work with fluids
  * Added SetTitle to Client
  * Fixed the RSA and LoginCharList values to 8.1
  * Added OutfitTypes from the update (thanks TTB)
  * Added a Calculate class to utility, containing methods for calculating statistics. At the moment it calculates the exp needed for a level
  * Fixed ability to read long strings
  * Fixed read/write of IsWalking and IsVisible (they were read/writing integers when it should be bytes

# v1.3 #

  * Finished updating addresses to 8.10 (thanks TibiaAuto!)
  * Added IsActive method to Client.
  * Added IsAttacking() method to Objects/Creature.cs.
  * Added Stack method to Inventory.
  * Added option to change the ClientChooser title.
  * Added some tiles to Constants/Tiles.cs.
  * Fixed GetContainers and GetItems to reverse the list so it is read in the correct direction; (0,0) is first.
  * Fixed KeyboardHook and made it user friendly (see example code below).
  * Fixed read/write function names in Objects/Client.cs to PascalCase.
  * Fixed using/moving items (client wasn't getting set).

# v1.2 #

  * Added support for Tibia 8.10
  * Added GoTo a location (similar to clicking a square)
  * Added VipList objects and methods
  * Added functions to Objects.Battlelist for finding creatures by name while ignoring spaces
  * Added getVersion to Objects.Client
  * Added support for multiple Tibia versions (currently 8.10 and 8.00) via Tibia.Version.Set(string version)
  * Added support for the Target CreatureType
  * Fixed SetServer and SetOT
  * Fixed makeRune to check hands better and move around items, as welll as making the soul point check optional.
  * Fixed memory read/write for client to only open the handle once (when the client object is created)
  * Fixed use item on ground packet structure

# v1.1 #

  * Added server/port changer to connect to OT servers
  * Added ability to change the RSA key
  * Fixed outfit addon and type to use enum
  * Added function map.replaceTrees();

# v1.0 #

  * Initial release