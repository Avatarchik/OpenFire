Thanks for Buying Hud System.

Get Starting-----------------------------------------------------

1- Import Hud System package into the project.
2- Drap the HudSystem Prefabs from Prefab folder, into scene room.
3- Add your huds in list of HudManager.
4- Ready, enjoy!

HudManager:
Huds = List with all huds in scene.
LocalPlayer = reference position player (add the camera of Player)
DecalMode = If true, icons will show off-screem.
ClampBoder = limited distance before the screen end (When not show arrow)

HudInfo:
Target = the GameObject that will follow the gui.
Icon = Icon to draw in target.
Color = Gui color.
TypeHud = Effect type when the player moves toward or away from the target.
offset = Should the icon be drawn at a different position relative to the GameObject. Expected coordinates are in world space.
Text = text to be will show in Hud.
isPalpitin = Bliking effect?


How Create a new hud in prefabs?
- add the script bl_Hud to the prefabs and complete the HudInfo.
when the prefab is instantiated, it will be added automatically to Hud Manager.



Support: http://lovattostudio.com/Forum/index.php
Email: brinerjhonson.lc@gmail.com
Lovatto Studio 2015.