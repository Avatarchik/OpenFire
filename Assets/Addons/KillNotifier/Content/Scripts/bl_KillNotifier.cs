using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class bl_KillNotifier : MonoBehaviour {

    public Animation Anim;
    public Image KillLogo;
    public Text KillText;
    [Space(5)]
    public string ShowAnimation = "StartKillN";
    public string HideAnimation = "HideKillN";

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Logo"></param>
    /// <param name="text"></param>
    public void NewKill(Sprite Logo, string text)
    {
        KillLogo.sprite = Logo;
        KillText.text = text;
        Anim.Play(ShowAnimation);
    }
    /// <summary>
    /// 
    /// </summary>
    public void Hide()
    {
        Anim.Play(HideAnimation);
    }
}