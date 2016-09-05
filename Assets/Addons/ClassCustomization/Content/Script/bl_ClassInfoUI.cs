using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class bl_ClassInfoUI : MonoBehaviour {

    public Image Icon;
    public Text NameText;
    public int ID;
    [HideInInspector] public int ClassId = 0;
    [HideInInspector] public int ListID = 0;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="n"></param>
    /// <param name="s"></param>
    /// <param name="id"></param>
    /// <param name="slot"></param>
    /// <param name="lID"></param>
    public void GetInfo(string n,Sprite s, int id,int slot,int lID)
    {
        Icon.sprite = s;
        NameText.text = n;
        ID = id;
        ClassId = slot;
        ListID = lID;
    }
    /// <summary>
    /// 
    /// </summary>
    public void ChangeSlot()
    {
        bl_ClassCustomize c = GameObject.Find("ClassManager").GetComponent<bl_ClassCustomize>();
        c.ChangeSlotClass(ID,ClassId,ListID);
    }
}