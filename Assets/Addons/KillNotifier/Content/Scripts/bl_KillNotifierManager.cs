using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class bl_KillNotifierManager : MonoBehaviour {

    public bl_KillNotifier Notifier = null;
    /// <summary>
    /// Create all default kills types
    /// you call the id of kill from this list
    /// </summary>
    public List<bl_KillInfo> KillsStreaks = new List<bl_KillInfo>();
    public float TimeToReset = 7f;//time for losed the streak
    [Range(0.01f,3f)]
    public float Delay = 1.0f;
    [HideInInspector]
    public List<bl_KillInfo> Kills = new List<bl_KillInfo>();
    [Space(5)]
    public int HeatShotID = 8;
    //Privates
    protected int Current = 0;
    protected bool Avaible = true;
    protected int Streak = -1;
    protected float CacheTFL;

    /// <summary>
    /// 
    /// </summary>
    void Awake()
    {
        CacheTFL = TimeToReset;
    }
    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        if (Notifier == null)
            return;
        if (Kills.Count <= 0)
        {
            Current = 0;
            return;
        }
        if (!Kills[Current].AlredyShow && Avaible)// Sure of show only the current notifier
        {
            Kills[Current].AlredyShow = true;
            Notifier.NewKill(Kills[Current].KillLogo, Kills[Current].KillName);
            if (Kills[Current].KillClip != null)
            {
                GetComponent<AudioSource>().clip = Kills[Current].KillClip;
                GetComponent<AudioSource>().Play();
            }
        }
        if (Kills[Current].TimeToShow > 0.0f)
        {
            Kills[Current].TimeToShow -= Time.deltaTime;
        }
        else
        {
            Closet();
        }
    }
    /// <summary>
    /// Hide the current notifier
    /// </summary>
    public void Closet()
    {
       Notifier.Hide();
       Kills.Remove(Kills[Current]);
       StartCoroutine(GetAvaible());
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IEnumerator GetAvaible()
    {
        Avaible = false;
        yield return new WaitForSeconds(Delay);
        Avaible = true;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="heatshot"></param>
    public void NewKill( bool heatshot = false)
    {
        if (Streak <= -1)//if start the streak
        {
            InvokeRepeating("StreakCountDown", 1, 1);//start countdown
        }
        else
        {
            TimeToReset = CacheTFL;//Reset time again
        }
        if (Streak < KillsStreaks.Count - 2)// - 2 because the ultime in list is for heat shot only.
        {
            Streak++;
        }
        else
        {
            Streak = KillsStreaks.Count - 2;
        }

        bl_KillInfo n = new bl_KillInfo();
        if (!heatshot)
        {
            n.KillName = KillsStreaks[Streak].KillName;
            n.KillLogo = KillsStreaks[Streak].KillLogo;
            n.KillClip = KillsStreaks[Streak].KillClip;
            n.TimeToShow = 5;
        }
        else
        {
            n.KillName = KillsStreaks[HeatShotID].KillName;
            n.KillLogo = KillsStreaks[HeatShotID].KillLogo;
            n.KillClip = KillsStreaks[HeatShotID].KillClip;
            n.TimeToShow = 5;
        }
        Kills.Add(n);
    }
    /// <summary>
    /// 
    /// </summary>
    void StreakCountDown()
    {
        TimeToReset--;
        if (TimeToReset <= 0)
        {
            CancelInvoke("StreakCountDown");
            TimeToReset = CacheTFL;
            Streak = -1;//restar streak
        }
    }
}