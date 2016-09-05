using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class bl_LevelUnlock : MonoBehaviour {

    public List<bl_LevelInfo> Levels = new List<bl_LevelInfo>();
    public string Level = "";


    private int CurrentScore = 0;
    public static int LevelID = 0;
    [HideInInspector]public int OldScore = 0;

    private int OldLevel = 0;
    private bool FirtsTime = true;
    public const string LUName = "LevelSystem";
    private bl_SaveInfo m_Info = null;
    private bl_LevelManager LM;
    private bool AlredyTakeScore = false;
    /// <summary>
    /// 
    /// </summary>
    void Awake()
    {
        this.gameObject.name = LUName;
        DontDestroyOnLoad(gameObject);
        //Take firts time
        if (GameObject.Find(bl_SaveInfo.SaveInfoName) != null)
        {
            m_Info = GameObject.Find(bl_SaveInfo.SaveInfoName).GetComponent<bl_SaveInfo>();
            CurrentScore = m_Info.Score;
            AlredyTakeScore = true;
        }
    }

    public void OnInit()
    {
        if (GameObject.Find("Lobby") != null)
        {
            LM = GameObject.Find("Lobby").GetComponent<bl_LevelManager>();
        }
        if (GameObject.Find(bl_SaveInfo.SaveInfoName) != null)
        {
            m_Info = GameObject.Find(bl_SaveInfo.SaveInfoName).GetComponent<bl_SaveInfo>();
            if (!AlredyTakeScore) { CurrentScore = m_Info.Score; AlredyTakeScore = true; }
            GetCurrentLevel();          
            if (LM != null)
            {
                LM.RefreshInfo();
            }
        }
        else
        {
            Debug.LogWarning("Need Login for level system work");
        }
    }
    /// <summary>
    /// 
    /// </summary>
    void GetCurrentLevel()
    {
        OldScore = CurrentScore;
        OldLevel = LevelID;
        for (int i = 0; i < Levels.Count; i++)
        {
            if (CurrentScore >= Levels[i].ScoreNeeded)
            {
                LevelID = i;
                Level = Levels[i].LevelName;
            }
        }
        if (FirtsTime)
        {
            OldLevel = LevelID;
            FirtsTime = false;
        }
        if (LM != null)
        {
            LM.UpdateProfile(Level, CurrentScore, Levels[LevelID].LevelIcon);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="score"></param>
    public void UpdateScore(int score)
    {
        OldScore = CurrentScore;
        OldLevel = LevelID;
        CurrentScore = score;
        for (int i = 0; i < Levels.Count; i++)
        {
            if (CurrentScore >= Levels[i].ScoreNeeded)
            {
                LevelID = i;
                Level = Levels[i].LevelName;
            }
        }
        if (FirtsTime)
        {
            OldLevel = LevelID;
            FirtsTime = false;
        }
    }
    public void RefresOldLevel() { OldLevel = LevelID; }
    /// <summary>
    /// /
    /// </summary>
    public int GetCurrentScore
    {
        get
        {
            return CurrentScore;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public bool isNewLevel
    {
        get
        {
            bool b = (LevelID > OldLevel);
            return b;
        }
    }
}