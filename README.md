# PEAKTrails

- Client-sided mod that adds real-time trails for all players!  
- Trail settings and other options are provided via a configuration file.  
- Configurations can be updated while in-game using [ModConfig](https://thunderstore.io/c/peak/p/PEAKModding/ModConfig/)  

![cinematic down](https://github.com/darmuh/PEAKTrails/blob/main/images/trails_cinema_down_cropped.webp?raw=true)

## Trails For All!
- Whether your whole lobby has this mod or just you, all players (from your perspective) will have trails!  
- Your own trail can be enabled or disabled (locally) via the ``Show Self Trail`` configuration item.  
- Trail Width & Length can be configured via the ``Trail Width`` and ``Trail Length`` configuration items.  
- Each trail will match it's player's color as shown below  

![colors image](https://github.com/darmuh/PEAKTrails/blob/main/images/colors.png?raw=true)  

## Visibility Controls
- Set your desired visibility behavior using the ``Visbility Setting`` configuration item.  
- This setting provides 6 different options:  
	- ``AlwaysOn`` means the trail will always be visible.  
	- ``ToggleKey`` means the trail can be toggled on/off with a custom keybind.  
	- ``OnActionShow`` means the trail will be visible when a player is performing a specific action.  
	- ``OnActionHide`` means the trail will always be visible except when a player is performing a specific action.  
	- ``OnHoldItem`` means the trail will be visible while holding a specific item.  
	- ``OnItemUseButton`` means the trail will be visible when a player is pressing the primary or secondary use buttons while holding a specific item.  

### Visbility Setting - ToggleKey  
- When the ``Visibility Toggle Key`` is pressed, trails will be shown or hidden depending on their current status.  
- The ``Visibility Press Delay`` configuration item determines how long after a key press the game will wait until listening for the next key press.  

<details> <summary>ToggleKey Example</summary>  
   <img src="https://github.com/darmuh/PEAKTrails/blob/main/images/toggle_trail_NEW.webp?raw=true" alt="toggle trail key">
![toggle trail keybind](https://github.com/darmuh/PEAKTrails/blob/main/images/toggle_trail_NEW.webp?raw=true)  
</details>

### Visibility Setting - OnActionShow & OnActionHide  
- This setting is dependent on the ``Visibility Toggle Action`` configuration item.  
- Currently the options for this configuration item are ``Reach, Crouch, SaluteEmote, ThumbsUpEmote, NoNoEmote, ThinkEmote, PlayDeadEmote, ShrugEmote, CrossedArmsEmote, and DanceEmote``  
- When ``Visibility Setting`` is set to ``OnActionShow``, performing the action will **SHOW** the trail.  
- When ``Visibility Setting`` is set to ``OnActionHide``, performing the action will **HIDE** the trail.  

<details> <summary>Toggle Action - Reach</summary>  
  <img src="https://github.com/darmuh/PEAKTrails/blob/main/images/toggle_trail_reach.webp?raw=true" alt="toggle trail reach">
</details>
<details> <summary>Toggle Action - ThinkEmote</summary> 
  <img src="https://github.com/darmuh/PEAKTrails/blob/main/images/toggle_trail_emote.webp?raw=true" alt="toggle trail ThinkEmote">
</details>

### Visibility Setting - OnHoldItem & OnItemUseButton  
- The ``Visibility Toggle Item`` configuration item determines what item the player must hold in order to show the trail.  
- Valid items for this setting are ``Binoculars, BingBong, Bugle, Coconut, Conch, Compass, Flare, Guidebook, and Lantern``  
- When ``Visibility Setting`` is set to ``OnItemUseButton``, the player must also press the primary or secondary use button while holding this item in order to show the trail.  
	- Please note the item does not actually need to be used to show the trail.  

<details> <summary>Toggle Item - Bingbong</summary> 
  <img src="https://github.com/darmuh/PEAKTrails/blob/main/images/toggle_trail_bingbong.webp?raw=true" alt="toggle trail BingBong">
</details>

### Compatibility:  
- This mod is fully compatible with all additional colors mods.  
- When using [SkinColorSliders](https://thunderstore.io/c/peak/p/Snosz/SkinColorSliders/), your trail will match the color of your HEAD.  