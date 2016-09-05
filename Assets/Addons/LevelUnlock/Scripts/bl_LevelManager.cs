////Put this in lobby scene UMenu

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class bl_LevelManager : MonoBehaviour {

    public string MessageNewLevel = "";

    public GameObject NewLevelNotifier = null;
    public Text LevelText;
    public Image LevelIcon;
    [Space(5)]
    public Text LevelTextProfile = null;
    public Text ScoreTextProfile = null;
    public Image LevelIconProfile = null;
    public GameObject ManagerPrefab;

    private bl_LevelUnlock LU;

    /// <summary>
    /// 
    /// </summary>
    void Awake()
    {
        if (GameObject.Find(bl_LevelUnlock.LUName) == null)
        {
            GameObject g = Instantiate(ManagerPrefab) as GameObject;
            g.GetComponent<bl_LevelUnlock>().OnInit();
        }
        else
        {
            GameObject.Find(bl_LevelUnlock.LUName).GetComponent<bl_LevelUnlock>().OnInit();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void RefreshInfo()
    {
        if (GameObject.Find(bl_LevelUnlock.LUName) != null)
        {
            LU = GameObject.Find(bl_LevelUnlock.LUName).GetComponent<bl_LevelUnlock>();
            Calculate();
        }
        else
        {           
            Debug.Log("No have Level Unlock in scene");
        }
        Debug.Log("2");
    }

    /// <summary>
    /// 
    /// </summary>
    void Calculate()
    {
            if (LU.isNewLevel)
            {
                NewLevelNotifier.SetActive(true);
                LevelText.text = MessageNewLevel+ " <color=orange>"+ LU.Levels[bl_LevelUnlock.LevelID].LevelName + "</color>";
                LevelIcon.sprite = LU.Levels[bl_LevelUnlock.LevelID].LevelIcon;
            }
            Debug.Log("3");
    }
    /// <summary>
    /// 
    /// </summary>
    public void UpdateProfile(string l,int s,Sprite spri)
    {
        LevelTextProfile.text = "Level: <color=orange>" + l + "</color>";
        ScoreTextProfile.text = "Score: <color=orange>" + s + "</color>";
        LevelIconProfile.sprite = spri;
    }
    /// <summary>
    /// 
    /// </summary>
    public void Closet() { NewLevelNotifier.SetActive(false); LU.RefresOldLevel(); }
}