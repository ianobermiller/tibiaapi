### Table of Contents ###
  * [Get a client object to work with](CodeSamples#Get_a_client_object_to_work_with.md)
  * [Using the hook proxy](CodeSamples#Using_the_hook_proxy.md)
  * [Getting player information from the Tibia website](CodeSamples#Getting_player_information_from_the_Tibia_website.md)
  * [Using the proxy](CodeSamples#Using_the_proxy.md)
  * [Parsing commands from the player](CodeSamples#Parsing_commands_from_the_player.md)
  * [Perform name spy or level spy](CodeSamples#Perform_name_spy_or_level_spy.md)
  * [Setup a global keyboard hook](CodeSamples#Setup_a_global_keyboard_hook.md)
  * [Connect to an OT Server](CodeSamples#Connect_to_an_OT_Server.md)
  * [Eat a mushroom in your inventory](CodeSamples#Eat_a_mushroom_in_your_inventory.md)
  * [Attack the first rat found in the battelist](CodeSamples#Attack_the_first_rat_found_in_the_battelist.md)
  * [Change your players outfit type and addon](CodeSamples#Change_your_players_outfit_type_and_addon.md)
  * [Replace all the trees with small fir trees](CodeSamples#Replace_all_the_trees_with_small_fir_trees.md)
  * [Make a simple lighthack](CodeSamples#Make_a_simple_lighthack.md)
  * [Draw players hitpoints in percents above his name](CodeSamples#Draw_players_hitpoints_in_percents_above_his_name.md)
  * [Using Util.Timer](CodeSamples#Using_Util.Timer.md)

### Note ###
All code samples are in C#. You can convert the code samples to VB.NET using this tool: http://www.developerfusion.com/tools/convert/csharp-to-vb/

Most of these samples assume you are using Tibia and Tibia.Objects:

```
using Tibia;
using Tibia.Objects;
```

## Get a client object to work with ##
```
using Tibia.Util;

Client client = ClientChooser.ShowBox();
if (client == null)
{
	System.Console.WriteLine("No active client.");
}
```

## Using the hook proxy ##
```
using Tibia;
using Tibia.Objects;
using Tibia.Packets;

class Program
{
    static void Main(string[] args)
    {
        Client client = Client.GetClients()[0];
        HookProxy hookProxy = new HookProxy(client);

        hookProxy.SplitPacketFromServer += delegate(byte type, byte[] data)
        {
            System.Console.WriteLine("SERVER: " + data.ToHexString());
        };

        hookProxy.SplitPacketFromClient += delegate(byte type, byte[] data)
        {
            System.Console.WriteLine("CLIENT: " + data.ToHexString());
        };

        System.Console.ReadLine();
    }
}
```

## Getting player information from the Tibia website ##
```
using Tibia.Util;

// Print the profession of the player Bubble
// This method is asynchronous, that is why we have to supply a 
// callback to get the data. It does not block your GUI if the 
// request take a while.
Website.LookupPlayer("Bubble", delegate(Website.CharInfo i)
{
    System.Console.WriteLine(i.Profession);
});
```

## Using the proxy ##
```
using Tibia;
using Tibia.Objects;

class Program
{
    static void Main(string[] args)
    {
        Client client = Client.GetClients()[0];

        client.IO.StartProxy();

        client.IO.Proxy.SplitPacketFromServer += delegate(byte type, byte[] data)
        {
            System.Console.WriteLine("SERVER: " + data.ToHexString());
        };

        client.IO.Proxy.SplitPacketFromClient += delegate(byte type, byte[] data)
        {
            System.Console.WriteLine("CLIENT: " + data.ToHexString());
        };

        System.Console.ReadLine();
    }
}
```

## Parsing commands from the player ##
```
using Tibia.Objects;
using Tibia.Packets;
using Tibia.Packets.Outgoing;

class Program
{
    static void Main(string[] args)
    {
        Client client = Client.GetClients()[0];

        client.IO.StartProxy();

        client.IO.Proxy.ReceivedPlayerSpeechOutgoingPacket += delegate(OutgoingPacket packet)
        {
            PlayerSpeechPacket p = (PlayerSpeechPacket)packet;
            if (p.Message.StartsWith("/"))
            {
                System.Console.WriteLine("Command: " + p.Message.Substring(1));
                return false; // Don't send to the server
            }
            return true;
        };

        System.Console.ReadLine();
    }
}
```

## Perform name spy or level spy ##
```
// Show names on other floors
client.Map.NameSpyOn();

// Move the screen to the floor below
client.Map.LevelSpyOn(-1);
```

## Setup a global mouse hook ##
The following code will show you how to set up a global mouse hook and capture when the ButtonDown event is raised

```
// Enable mouse hook and set the button down event to a function

if (!MouseHook.Enabled)
{
    MouseHook.Enable();
    MouseHook.ButtonDown = GlobalMouseDown;
}

// The function to handle the button down event
public bool GlobalMouseDown(System.Windows.Forms.MouseButtons button)
{
    if (button == Windows.Forms.MouseButtons.Left) {
        MessageBox.Show("Left mouse button was clicked down.");
    }
    
    return true;
}
```

## Setup a global keyboard hook ##
Put the following in a button's click event. This example captures the hotkey Ctrl + Alt + A in the Tibia client only (doesn't let it go to the client). All other keypresses, client or otherwise, are ignored.

```
if (!KeyboardHook.Enabled)
{
    KeyboardHook.Enable();
    KeyboardHook.Add(Keys.A, new KeyboardHook.KeyPressed(delegate()
    {
        if (client.IsActive)
        {
            if (KeyboardHook.Control && KeyboardHook.Alt)
            {
                string text = "You want to capture the hotkey " +
                Tibia.KeyboardHook.KeyToString(Keys.A) + " in Tibia!";
                MessageBox.Show(text);
            	return false;
      	     }
        }
      	return true;
    }));
}
else
{
    KeyboardHook.Disable();
}
```

## Connect to an OT Server ##
```
client.Login.SetOT("radonia.net", 7171);
```

## Eat a mushroom in your inventory ##
```
client.Inventory.UseItem(Tibia.Constants.Items.Food.WhiteMushroom.Id);
```

## Attack the first rat found in the battelist ##
```
var rat = client.BattleList.GetCreatures().FirstOrDefault(c => c.Name.Equals("Rat"));
if (rat != null)
    rat.Attack();
```

## Change your players outfit type and addon ##
```
Player player = client.GetPlayer();
player.OutfitType = Tibia.Constants.OutfitType.BeggarMale;
player.Addon = Tibia.Constants.OutfitAddon.Both;
```

## Replace all the trees with small fir trees ##
**C#**
```
client.Map.ReplaceTrees();
```

## Make a simple lighthack ##
```
Player player=client.GetPlayer();
player.Light = Tibia.Constants.LightSize.Full;
player.LightColor = Tibia.Constants.LightColor.White;
```

## Draw players hitpoints in percents above his name ##
```
Player player = client.GetPlayer();
client.Screen.DrawCreatureText(
    player.Id,
    new Location(0, -10, 0),
    Color.Green, 
    Tibia.Constants.ClientFont.NormalBorder,
    player.HPBar.ToString() + "%"
);
```

## Using Util.Timer ##
```
using Tibia.Util;

// This code should go in MainForm_Load, for instance
Timer timer = new Timer();
timer.Interval = 1000; // 1 second
timer.Execute += DoSomething;
timer.Start();
 
void DoSomething()
{
    // Make sure you use form.Invoke if you need to modify a form's controls in here
}
```