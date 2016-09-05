using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class bl_KillInfo
{
    public string KillName = KillType.Firts.ToString();
    public KillType m_Type = KillType.Firts;    
    public Sprite KillLogo = null;
    public AudioClip KillClip = null;
    public float TimeToShow = 5;
    [HideInInspector]
    public bool AlredyShow = false;
}
    /// <summary>
    /// Add more Types here if you want
    /// </summary>
    [System.Serializable]
    public enum KillType
    {
        Firts,
        Double,
        Multi,
        Rambo,
        Fantastic,
        Unbelievable,
        Boss,
        HeatShot,
    }
