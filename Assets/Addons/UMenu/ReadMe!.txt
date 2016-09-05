Thanks for buying UMenu.

UMENU is an advanced System Menu with the new Unity UI 4.6, with a minimalist design and modern flat style,
 plus multiple transition effects and sound effects.

Requires Unity 4.6+

Features:

- Unity 4.6 UI
- Lock / UnLock Levels
- Rated Levels
- 4 different color styles.
- Flag modern style
- Effects of transitions
- Sound effects
- Custom Water animations
- Custom UIAtlas
- Different designs of buttons
- Save and load settings
- Load from URL Avatar
- Social Buttons

Get Started:

- Import UMenu package to your project.
- Open the scena "UMenu" or others scenes (UMenuTheme1,UMenuTheme2 or UMenuTheme3).
- In the scena find GO called "UMenu", in this find the list called "levels".
- Place there levels of your game (if any), placing this:

level = scena name / level name.
levelreview = sprite image preview of scene.
LevelRequired = the player level necesary for play in this level.
Rate = score / difficulty of this map (max 5)

-then, open the script "UMenuManager.cs" and enable the line number 328.

- Then change the rest to your liking.


MFPS--------------------------------------------------------------------
- Add UMenu MFPS Scene as firts in build setting and remove the old main menu.
-In bl_GameManager find thits: "OnDisconnectedFromPhoton()" and change this variable "Application.LoadLevel("MainMenu");" for Application.LoadLevel("UMenuMFPS");

-------------------------------------------------------------------------
Tips
-------------------------------------------------------------------------

-for loading the settings saved in any other scena, use the sample script called "UMLoadSettings.cs"

-to add or remove any IU of UIAtlas, use a graphics program such as PhotoShop, AdobeIlustrator, Gimb, etc ...
and configure it with the "Sprite Editor" of unity.

---Replace the Player Level with its  your own system of grading or score, ej: XP,Score,Kills,etc...



Change Log:

Version 1.0----------------------------------------------------------------
Initial version.

Version 1.2----------------------------------------------------------------
Impromoved: ScrollView and panel selection of levels.
Added: ScrollView improved selection of levels
Fix: ScrollView "Resolution" position.
Added: Download Audio for Background option, need a url of a file audio .waw or .oggvorvis

Version 1.3---------------------------------------------------------------
Impromoved: ScrollView and panel selection of levels.
Fix: Level Button Prefabs size
Added: ScrollBar in selection of levels
Add: Rated levels
Add: UnLock / Lock levels
Add: 3 diferents color styles.
Fix: Select level.

Contact: brinerjhonson.lc@gmail.com
Forum: http://lovattostudio.com/Forum/index.php
