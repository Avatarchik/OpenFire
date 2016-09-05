using UnityEngine;
using System.Collections;

public static class bl_KillNotifierUtils {

    public static bl_KillNotifierManager GetManager
    {
        get
        {
            bl_KillNotifierManager m = GameObject.Find("KillNotifierManager").GetComponent<bl_KillNotifierManager>();
            return m;
        }
    }
}