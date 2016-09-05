using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UMEventHelper : MonoBehaviour {

    public string m_string = "";
    public int m_int = 0;
    public float m_float = 0f;
    [HideInInspector]
    public Sprite m_sprite;
    [Space(5)]
    public int Rate = 5;
    public int LevelRequiered = 5;
    [HideInInspector]
    public bool Unlock = false;
    public Image[] Stars;
    public Image SmallPreview = null;

    /// <summary>
    /// 
    /// </summary>
    public void OnLevelChange()
    {
        if (!Unlock)//only if unlock can load level
            return;

        UMEvents.LevelChangeEvent(m_string, m_sprite);
    }

    /// <summary>
    /// 
    /// </summary>
    public void GetInfo(string str1, Sprite spr, int int1, int int2)
    {
        m_string = str1;
        m_sprite = spr;
        Rate = int1;
        LevelRequiered = int2;
        if (SmallPreview != null)
        {
            SmallPreview.sprite = spr;
        }
        if (Stars.Length >= Rate)
        {
            for (int i = 0; i < Rate; i++)
            {
                Stars[i].color = Color.white;
            }
        }
        if (UMenuManager.PlayerLevel >= LevelRequiered)
        {
            Unlock = true;
        }
    }
}