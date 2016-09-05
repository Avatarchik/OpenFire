using UnityEngine;
using System.Collections;

public class bl_Hud : MonoBehaviour {

    public bl_HudInfo HudInfo;

    /// <summary>
    /// Instantiate a new Hud
    /// add hud to hud manager in start
    /// </summary>
    void Start()
    {
        if (bl_HudManager.instance != null)
        {
            bl_HudManager.instance.CreateHud(this.HudInfo);
        }
        else
        {
            Debug.LogError("Need have a Hud Manager in scene");
        }
    }
	
}