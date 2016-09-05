using UnityEngine;
using System.Collections;

public class UMEvents
{
    public delegate void LevelChange(string m_level,Sprite m_preview);
    public static LevelChange OnLevelChange;

    public static void LevelChangeEvent(string t_level, Sprite t_preview)
    {
        if (OnLevelChange != null)
        {
            OnLevelChange(t_level, t_preview);
        }
    }

    


}
