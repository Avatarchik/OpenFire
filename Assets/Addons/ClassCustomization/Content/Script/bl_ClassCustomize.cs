using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class bl_ClassCustomize : MonoBehaviour {

    public PlayerClass m_Class = PlayerClass.Assault;
    public GameObject ClassManagerPrefab;
    public string SceneToReturn = "";
    [Space(5)]
    [Header("Weapons Info")]
    public List<ClassInfo> AssaultClass = new List<ClassInfo>();
    public List<ClassInfo> EngineerClass = new List<ClassInfo>();
    public List<ClassInfo> SupportClass = new List<ClassInfo>();
    public List<ClassInfo> ReconClass = new List<ClassInfo>();
    [Space(5)]
    [Header("HUD")]
    public HudClassContent PrimaryHUD;
    public HudClassContent SecundaryHUD;
    public HudClassContent KnifeHUD;
    public HudClassContent GrenadeHUD;
    [Space(7)]
    public RectTransform AssaultButton = null;
    public RectTransform EnginnerButton = null;
    public RectTransform SupportButton = null;
    public RectTransform ReconButton = null;
    public Text ClassText = null;
    [Space(7)]
    public GameObject GunSelectPrefabs = null;
    public Transform PanelWeaponList = null;


    private int CurrentSlot = 0;

    void Awake()
    {
        //Fix inssue
        if (PhotonNetwork.connected)
        {
            PhotonNetwork.Disconnect();
        }
        CheckForInstance();
    }
    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        TakeCurrentClass(bl_ClassManager.m_Class);

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    public void ChangeSlotClass(int id,int slot,int listid)
    {
        switch (CurrentSlot)
        {
            case 0:
                switch (bl_ClassManager.m_Class)
                {
                    case PlayerClass.Assault:
                        bl_ClassManager.PrimaryAssault = id;
                        PlayerPrefs.SetInt(ClassKey.PrimaryAssault, id);
                        break;
                    case PlayerClass.Engineer:
                        bl_ClassManager.PrimaryEnginner = id;
                        PlayerPrefs.SetInt(ClassKey.PrimaryEnginner, id);
                        break;
                    case PlayerClass.Support:
                        bl_ClassManager.PrimarySupport = id;
                        PlayerPrefs.SetInt(ClassKey.PrimarySupport, id);
                        break;
                    case PlayerClass.Recon:
                        bl_ClassManager.PrimaryRecon = id;
                        PlayerPrefs.SetInt(ClassKey.PrimaryRecon, id);
                        break;
                }
                
                break;
            case 1:
                switch (bl_ClassManager.m_Class)
                {
                    case PlayerClass.Assault:
                        bl_ClassManager.SecundaryAssault = id;
                        PlayerPrefs.SetInt(ClassKey.SecundaryAssault, id);
                        break;
                    case PlayerClass.Engineer:
                        bl_ClassManager.SecundaryEnginner = id;
                        PlayerPrefs.SetInt(ClassKey.SecundaryEnginner, id);
                        break;
                    case PlayerClass.Support:
                        bl_ClassManager.SecundarySupport = id;
                        PlayerPrefs.SetInt(ClassKey.SecundarySupport, id);
                        break;
                    case PlayerClass.Recon:
                        bl_ClassManager.SecundaryRecon = id;
                        PlayerPrefs.SetInt(ClassKey.SecundaryRecon, id);
                        break;
                }
                break;
            case 2:
                switch (bl_ClassManager.m_Class)
                {
                    case PlayerClass.Assault:
                        bl_ClassManager.KnifeAssault = id;
                        PlayerPrefs.SetInt(ClassKey.KnifeAssault, id);
                        break;
                    case PlayerClass.Engineer:
                        bl_ClassManager.KnifeEnginner = id;
                        PlayerPrefs.SetInt(ClassKey.KnifeEnginner, id);
                        break;
                    case PlayerClass.Support:
                        bl_ClassManager.KnifeSupport = id;
                        PlayerPrefs.SetInt(ClassKey.KnifeSupport, id);
                        break;
                    case PlayerClass.Recon:
                        bl_ClassManager.KnifeRecon = id;
                        PlayerPrefs.SetInt(ClassKey.KnifeRecon, id);
                        break;
                }
                break;
            case 3:
                switch (bl_ClassManager.m_Class)
                {
                    case PlayerClass.Assault:
                        bl_ClassManager.GrenadeAssault = id;
                        PlayerPrefs.SetInt(ClassKey.GrenadeAssault, id);
                        break;
                    case PlayerClass.Engineer:
                        bl_ClassManager.GrenadeEnginner = id;
                        PlayerPrefs.SetInt(ClassKey.GrenadeEnginner, id);
                        break;
                    case PlayerClass.Support:
                        bl_ClassManager.GrenadeSupport = id;
                        PlayerPrefs.SetInt(ClassKey.GrenadeSupport, id);
                        break;
                    case PlayerClass.Recon:
                        bl_ClassManager.GrenadeRecon = id;
                        PlayerPrefs.SetInt(ClassKey.GrenadeRecon, id);
                        break;
                }
                break;

        }
        UpdateClassUI(listid,slot);
    }
    /// <summary>
    /// 
    /// </summary>
    void CheckForInstance()
    {
        if (GameObject.Find(bl_ClassManager.ClassManagerName) == null)
        {
            Instantiate(ClassManagerPrefab);
        }
    }

    void UpdateClassUI(int id,int slot)
    {
        switch (bl_ClassManager.m_Class)
        {
            case PlayerClass.Assault:
                switch (slot)
                {
                    case 0:
                        PrimaryHUD.Icon.sprite = AssaultClass[id].WeaponIcon;
                        PrimaryHUD.WeaponNameText.text = AssaultClass[id].WeaponName;
                        PrimaryHUD.AccuracySlider.value = AssaultClass[id].Accuracy;
                        PrimaryHUD.DamageSlider.value = AssaultClass[id].Damage;
                        PrimaryHUD.RateSlider.value = AssaultClass[id].Rate;
                        break;
                    case 1:
                        SecundaryHUD.Icon.sprite = AssaultClass[id].WeaponIcon;
                        SecundaryHUD.WeaponNameText.text = AssaultClass[id].WeaponName;
                        SecundaryHUD.AccuracySlider.value = AssaultClass[id].Accuracy;
                        SecundaryHUD.DamageSlider.value = AssaultClass[id].Damage;
                        SecundaryHUD.RateSlider.value = AssaultClass[id].Rate;
                        break;
                    case 2:
                        KnifeHUD.Icon.sprite = AssaultClass[id].WeaponIcon;
                        KnifeHUD.WeaponNameText.text = AssaultClass[id].WeaponName;
                        KnifeHUD.AccuracySlider.value = AssaultClass[id].Accuracy;
                        KnifeHUD.DamageSlider.value = AssaultClass[id].Damage;
                        KnifeHUD.RateSlider.value = AssaultClass[id].Rate;
                        break;
                    case 3:
                        GrenadeHUD.Icon.sprite = AssaultClass[id].WeaponIcon;
                        GrenadeHUD.WeaponNameText.text = AssaultClass[id].WeaponName;
                        GrenadeHUD.AccuracySlider.value = AssaultClass[id].Accuracy;
                        GrenadeHUD.DamageSlider.value = AssaultClass[id].Damage;
                        GrenadeHUD.RateSlider.value = AssaultClass[id].Rate;
                        break;
                }
                break;
                //------------------------------------------------------------------
            case PlayerClass.Engineer :
                switch (slot)
                {
                    case 0:
                        PrimaryHUD.Icon.sprite = EngineerClass[id].WeaponIcon;
                        PrimaryHUD.WeaponNameText.text = EngineerClass[id].WeaponName;
                        PrimaryHUD.AccuracySlider.value = EngineerClass[id].Accuracy;
                        PrimaryHUD.DamageSlider.value = EngineerClass[id].Damage;
                        PrimaryHUD.RateSlider.value = EngineerClass[id].Rate;
                        break;
                    case 1:
                        SecundaryHUD.Icon.sprite = EngineerClass[id].WeaponIcon;
                        SecundaryHUD.WeaponNameText.text = EngineerClass[id].WeaponName;
                        SecundaryHUD.AccuracySlider.value = EngineerClass[id].Accuracy;
                        SecundaryHUD.DamageSlider.value = EngineerClass[id].Damage;
                        SecundaryHUD.RateSlider.value = EngineerClass[id].Rate;
                        break;
                    case 2:
                        KnifeHUD.Icon.sprite = EngineerClass[id].WeaponIcon;
                        KnifeHUD.WeaponNameText.text = EngineerClass[id].WeaponName;
                        KnifeHUD.AccuracySlider.value = EngineerClass[id].Accuracy;
                        KnifeHUD.DamageSlider.value = EngineerClass[id].Damage;
                        KnifeHUD.RateSlider.value = EngineerClass[id].Rate;
                        break;
                    case 3:
                        GrenadeHUD.Icon.sprite = EngineerClass[id].WeaponIcon;
                        GrenadeHUD.WeaponNameText.text = EngineerClass[id].WeaponName;
                        GrenadeHUD.AccuracySlider.value = EngineerClass[id].Accuracy;
                        GrenadeHUD.DamageSlider.value = EngineerClass[id].Damage;
                        GrenadeHUD.RateSlider.value = EngineerClass[id].Rate;
                        break;
                }
                break;
            case PlayerClass.Support:
                switch (slot)
                {
                    case 0:
                        PrimaryHUD.Icon.sprite = SupportClass[id].WeaponIcon;
                        PrimaryHUD.WeaponNameText.text = SupportClass[id].WeaponName;
                        PrimaryHUD.AccuracySlider.value = SupportClass[id].Accuracy;
                        PrimaryHUD.DamageSlider.value = SupportClass[id].Damage;
                        PrimaryHUD.RateSlider.value = SupportClass[id].Rate;
                        break;
                    case 1:
                        SecundaryHUD.Icon.sprite = SupportClass[id].WeaponIcon;
                        SecundaryHUD.WeaponNameText.text = SupportClass[id].WeaponName;
                        SecundaryHUD.AccuracySlider.value = SupportClass[id].Accuracy;
                        SecundaryHUD.DamageSlider.value = SupportClass[id].Damage;
                        SecundaryHUD.RateSlider.value = SupportClass[id].Rate;
                        break;
                    case 2:
                        KnifeHUD.Icon.sprite = SupportClass[id].WeaponIcon;
                        KnifeHUD.WeaponNameText.text = SupportClass[id].WeaponName;
                        KnifeHUD.AccuracySlider.value = SupportClass[id].Accuracy;
                        KnifeHUD.DamageSlider.value = SupportClass[id].Damage;
                        KnifeHUD.RateSlider.value = SupportClass[id].Rate;
                        break;
                    case 3:
                        GrenadeHUD.Icon.sprite = SupportClass[id].WeaponIcon;
                        GrenadeHUD.WeaponNameText.text = SupportClass[id].WeaponName;
                        GrenadeHUD.AccuracySlider.value = SupportClass[id].Accuracy;
                        GrenadeHUD.DamageSlider.value = SupportClass[id].Damage;
                        GrenadeHUD.RateSlider.value = SupportClass[id].Rate;
                        break;
                }
                break;
            case PlayerClass.Recon:
                switch (slot)
                {
                    case 0:
                        PrimaryHUD.Icon.sprite = ReconClass[id].WeaponIcon;
                        PrimaryHUD.WeaponNameText.text = ReconClass[id].WeaponName;
                        PrimaryHUD.AccuracySlider.value = ReconClass[id].Accuracy;
                        PrimaryHUD.DamageSlider.value = ReconClass[id].Damage;
                        PrimaryHUD.RateSlider.value = ReconClass[id].Rate;
                        break;
                    case 1:
                        SecundaryHUD.Icon.sprite = ReconClass[id].WeaponIcon;
                        SecundaryHUD.WeaponNameText.text = ReconClass[id].WeaponName;
                        SecundaryHUD.AccuracySlider.value = ReconClass[id].Accuracy;
                        SecundaryHUD.DamageSlider.value = ReconClass[id].Damage;
                        SecundaryHUD.RateSlider.value = ReconClass[id].Rate;
                        break;
                    case 2:
                        KnifeHUD.Icon.sprite = ReconClass[id].WeaponIcon;
                        KnifeHUD.Icon.sprite = ReconClass[id].WeaponIcon;
                        KnifeHUD.WeaponNameText.text = ReconClass[id].WeaponName;
                        KnifeHUD.AccuracySlider.value = ReconClass[id].Accuracy;
                        KnifeHUD.DamageSlider.value = ReconClass[id].Damage;
                        KnifeHUD.RateSlider.value = ReconClass[id].Rate;
                        break;
                    case 3:
                        GrenadeHUD.Icon.sprite = ReconClass[id].WeaponIcon;
                        GrenadeHUD.WeaponNameText.text = ReconClass[id].WeaponName;
                        GrenadeHUD.AccuracySlider.value = ReconClass[id].Accuracy;
                        GrenadeHUD.DamageSlider.value = ReconClass[id].Damage;
                        GrenadeHUD.RateSlider.value = ReconClass[id].Rate;
                        break;
                }
                break;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public void SaveClass() { bl_ClassManager.SaveClass(); }
    public void ReturnToScene() { Application.LoadLevel(SceneToReturn); }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="mclass"></param>
    public void ShowList(int slot)
    {
        CurrentSlot = slot;
        foreach (Transform t in PanelWeaponList.GetComponentsInChildren<Transform>())
        {
            if (t.GetComponent<bl_ClassInfoUI>() != null)
            {
                Destroy(t.gameObject);
            }
        }

        switch (bl_ClassManager.m_Class)
        {
            case PlayerClass.Assault:
                for (int i = 0; i < AssaultClass.Count; i++)
                {
                    switch (slot)
                    {
                        case 0://Primary
                            if (AssaultClass[i].Type != bl_Gun.weaponType.Knife && AssaultClass[i].Type != bl_Gun.weaponType.Launcher)
                            {
                                GameObject b = Instantiate(GunSelectPrefabs) as GameObject;
                                bl_ClassInfoUI iui = b.GetComponent<bl_ClassInfoUI>();
                                iui.GetInfo(AssaultClass[i].WeaponName, AssaultClass[i].WeaponIcon, AssaultClass[i].ID,0,i);
                                b.transform.SetParent(PanelWeaponList, false);
                            }
                            break;
                        case 1://Secundary
                            if (AssaultClass[i].Type != bl_Gun.weaponType.Knife && AssaultClass[i].Type != bl_Gun.weaponType.Launcher)
                            {
                                GameObject b = Instantiate(GunSelectPrefabs) as GameObject;
                                bl_ClassInfoUI iui = b.GetComponent<bl_ClassInfoUI>();
                                iui.GetInfo(AssaultClass[i].WeaponName, AssaultClass[i].WeaponIcon, AssaultClass[i].ID,1,i);
                                b.transform.SetParent(PanelWeaponList, false);
                            }
                            break;
                        case 2://Knife
                            if (AssaultClass[i].Type == bl_Gun.weaponType.Knife)
                            {
                                GameObject b = Instantiate(GunSelectPrefabs) as GameObject;
                                bl_ClassInfoUI iui = b.GetComponent<bl_ClassInfoUI>();
                                iui.GetInfo(AssaultClass[i].WeaponName, AssaultClass[i].WeaponIcon, AssaultClass[i].ID,2,i);
                                b.transform.SetParent(PanelWeaponList, false);
                            }
                            break;
                        case 3://Grenade
                            if ( AssaultClass[i].Type == bl_Gun.weaponType.Launcher)
                            {
                                GameObject b = Instantiate(GunSelectPrefabs) as GameObject;
                                bl_ClassInfoUI iui = b.GetComponent<bl_ClassInfoUI>();
                                iui.GetInfo(AssaultClass[i].WeaponName, AssaultClass[i].WeaponIcon, AssaultClass[i].ID,3,i);
                                b.transform.SetParent(PanelWeaponList, false);
                            }
                            break;
                    }                      
                }
                break;
            case PlayerClass.Engineer://----------------------------------------------------------------------------------------------------
                for (int i = 0; i < EngineerClass.Count; i++)
                {
                    switch (slot)
                    {
                        case 0://Primary
                            if (EngineerClass[i].Type != bl_Gun.weaponType.Knife && EngineerClass[i].Type != bl_Gun.weaponType.Launcher)
                            {
                                GameObject b = Instantiate(GunSelectPrefabs) as GameObject;
                                bl_ClassInfoUI iui = b.GetComponent<bl_ClassInfoUI>();
                                iui.GetInfo(EngineerClass[i].WeaponName, EngineerClass[i].WeaponIcon, EngineerClass[i].ID,0,i);
                                b.transform.SetParent(PanelWeaponList, false);
                            }
                            break;
                        case 1://Secundary
                            if (EngineerClass[i].Type != bl_Gun.weaponType.Knife && EngineerClass[i].Type != bl_Gun.weaponType.Launcher)
                            {
                                GameObject b = Instantiate(GunSelectPrefabs) as GameObject;
                                bl_ClassInfoUI iui = b.GetComponent<bl_ClassInfoUI>();
                                iui.GetInfo(EngineerClass[i].WeaponName, EngineerClass[i].WeaponIcon, EngineerClass[i].ID,1,i);
                                b.transform.SetParent(PanelWeaponList, false);
                            }
                            break;
                        case 2://Knife
                            if (EngineerClass[i].Type == bl_Gun.weaponType.Knife)
                            {
                                GameObject b = Instantiate(GunSelectPrefabs) as GameObject;
                                bl_ClassInfoUI iui = b.GetComponent<bl_ClassInfoUI>();
                                iui.GetInfo(EngineerClass[i].WeaponName, EngineerClass[i].WeaponIcon, EngineerClass[i].ID,2,i);
                                b.transform.SetParent(PanelWeaponList, false);
                            }
                            break;
                        case 3://Primary
                            if (EngineerClass[i].Type == bl_Gun.weaponType.Launcher)
                            {
                                GameObject b = Instantiate(GunSelectPrefabs) as GameObject;
                                bl_ClassInfoUI iui = b.GetComponent<bl_ClassInfoUI>();
                                iui.GetInfo(EngineerClass[i].WeaponName, EngineerClass[i].WeaponIcon, EngineerClass[i].ID,3,i);
                                b.transform.SetParent(PanelWeaponList, false);
                            }
                            break;
                    }                      
                }
                break;
            case PlayerClass.Support://-----------------------------------------------------------------------------------------------------
                for (int i = 0; i < SupportClass.Count; i++)
                {
                    switch (slot)
                    {
                        case 0://Primary
                            if (SupportClass[i].Type != bl_Gun.weaponType.Knife && SupportClass[i].Type != bl_Gun.weaponType.Launcher)
                            {
                                GameObject b = Instantiate(GunSelectPrefabs) as GameObject;
                                bl_ClassInfoUI iui = b.GetComponent<bl_ClassInfoUI>();
                                iui.GetInfo(SupportClass[i].WeaponName, SupportClass[i].WeaponIcon, SupportClass[i].ID,0,i);
                                b.transform.SetParent(PanelWeaponList, false);
                            }
                            break;
                        case 1://Secundary
                            if (SupportClass[i].Type != bl_Gun.weaponType.Knife && SupportClass[i].Type != bl_Gun.weaponType.Launcher)
                            {
                                GameObject b = Instantiate(GunSelectPrefabs) as GameObject;
                                bl_ClassInfoUI iui = b.GetComponent<bl_ClassInfoUI>();
                                iui.GetInfo(SupportClass[i].WeaponName, SupportClass[i].WeaponIcon, SupportClass[i].ID,1,i);
                                b.transform.SetParent(PanelWeaponList, false);
                            }
                            break;
                        case 2://Knife
                            if (SupportClass[i].Type == bl_Gun.weaponType.Knife)
                            {
                                GameObject b = Instantiate(GunSelectPrefabs) as GameObject;
                                bl_ClassInfoUI iui = b.GetComponent<bl_ClassInfoUI>();
                                iui.GetInfo(SupportClass[i].WeaponName, SupportClass[i].WeaponIcon, SupportClass[i].ID,2,i);
                                b.transform.SetParent(PanelWeaponList, false);
                            }
                            break;
                        case 3://Primary
                            if (SupportClass[i].Type == bl_Gun.weaponType.Launcher)
                            {
                                GameObject b = Instantiate(GunSelectPrefabs) as GameObject;
                                bl_ClassInfoUI iui = b.GetComponent<bl_ClassInfoUI>();
                                iui.GetInfo(SupportClass[i].WeaponName, SupportClass[i].WeaponIcon, SupportClass[i].ID,3,i);
                                b.transform.SetParent(PanelWeaponList, false);
                            }
                            break;
                    }                      
                }
                break;
            case PlayerClass.Recon://-----------------------------------------------------------------------------
                for (int i = 0; i < ReconClass.Count; i++)
                {
                    switch (slot)
                    {
                        case 0://Primary
                            if (ReconClass[i].Type != bl_Gun.weaponType.Knife && ReconClass[i].Type != bl_Gun.weaponType.Launcher)
                            {
                                GameObject b = Instantiate(GunSelectPrefabs) as GameObject;
                                bl_ClassInfoUI iui = b.GetComponent<bl_ClassInfoUI>();
                                iui.GetInfo(ReconClass[i].WeaponName, ReconClass[i].WeaponIcon, ReconClass[i].ID,0,i);
                                b.transform.SetParent(PanelWeaponList, false);
                            }
                            break;
                        case 1://Secundary
                            if (ReconClass[i].Type != bl_Gun.weaponType.Knife && ReconClass[i].Type != bl_Gun.weaponType.Launcher)
                            {
                                GameObject b = Instantiate(GunSelectPrefabs) as GameObject;
                                bl_ClassInfoUI iui = b.GetComponent<bl_ClassInfoUI>();
                                iui.GetInfo(ReconClass[i].WeaponName, ReconClass[i].WeaponIcon, ReconClass[i].ID,1,i);
                                b.transform.SetParent(PanelWeaponList, false);
                            }
                            break;
                        case 2://Knife
                            if (ReconClass[i].Type == bl_Gun.weaponType.Knife)
                            {
                                GameObject b = Instantiate(GunSelectPrefabs) as GameObject;
                                bl_ClassInfoUI iui = b.GetComponent<bl_ClassInfoUI>();
                                iui.GetInfo(ReconClass[i].WeaponName, ReconClass[i].WeaponIcon, ReconClass[i].ID,2,i);
                                b.transform.SetParent(PanelWeaponList, false);
                            }
                            break;
                        case 3://Primary
                            if (ReconClass[i].Type == bl_Gun.weaponType.Launcher)
                            {
                                GameObject b = Instantiate(GunSelectPrefabs) as GameObject;
                                bl_ClassInfoUI iui = b.GetComponent<bl_ClassInfoUI>();
                                iui.GetInfo(ReconClass[i].WeaponName, ReconClass[i].WeaponIcon, ReconClass[i].ID,3,i);
                                b.transform.SetParent(PanelWeaponList, false);
                            }
                            break;
                    }                      
                }
                break;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="newclass"></param>
    private float DSDC = 0;
    public void ChangeClass(PlayerClass newclass)
    {
        if (m_Class == newclass && bl_ClassManager.m_Class == newclass)
            return;

        m_Class = newclass; bl_ClassManager.m_Class = newclass;
        ClassText.text = newclass.ToString() + " Class";
        ResetClassHUD();    
        int i = 0;
        switch (newclass)
        {
            case PlayerClass.Assault:
                i = 0;
                Vector2 v = AssaultButton.sizeDelta;
                DSDC = v.y;
                EnginnerButton.sizeDelta = v;
                SupportButton.sizeDelta = v;
                ReconButton.sizeDelta = v;
                v.y = DSDC + 10;
                AssaultButton.sizeDelta = v;
                UpdateClassUI(GetListId(PlayerClass.Assault, bl_ClassManager.PrimaryAssault), 0);
                UpdateClassUI(GetListId(PlayerClass.Assault, bl_ClassManager.SecundaryAssault), 1);
                UpdateClassUI(GetListId(PlayerClass.Assault, bl_ClassManager.KnifeAssault), 2);
                UpdateClassUI(GetListId(PlayerClass.Assault, bl_ClassManager.GrenadeAssault), 3);
                break;
            case PlayerClass.Engineer:
                i = 1;
                Vector2 va = EnginnerButton.sizeDelta;
                DSDC = va.y;
                AssaultButton.sizeDelta = va;
                SupportButton.sizeDelta = va;
                ReconButton.sizeDelta = va;
                va.y = DSDC + 10;
                EnginnerButton.sizeDelta = va;
                UpdateClassUI(GetListId(PlayerClass.Engineer, bl_ClassManager.PrimaryEnginner), 0);
                UpdateClassUI(GetListId(PlayerClass.Engineer, bl_ClassManager.SecundaryEnginner), 1);
                UpdateClassUI(GetListId(PlayerClass.Engineer, bl_ClassManager.KnifeEnginner), 2);
                UpdateClassUI(GetListId(PlayerClass.Engineer, bl_ClassManager.GrenadeEnginner), 3);
                break;
            case PlayerClass.Support:
                i = 2;
                Vector2 ve = SupportButton.sizeDelta;
                DSDC = ve.y;
                AssaultButton.sizeDelta = ve;
                EnginnerButton.sizeDelta = ve;
                ReconButton.sizeDelta = ve;
                ve.y = DSDC + 10;
                SupportButton.sizeDelta = ve;
                UpdateClassUI(GetListId(PlayerClass.Support, bl_ClassManager.PrimarySupport), 0);
                UpdateClassUI(GetListId(PlayerClass.Support, bl_ClassManager.SecundarySupport), 1);
                UpdateClassUI(GetListId(PlayerClass.Support, bl_ClassManager.KnifeSupport), 2);
                UpdateClassUI(GetListId(PlayerClass.Support, bl_ClassManager.GrenadeSupport), 3);
                break;
            case PlayerClass.Recon:
                i = 3;
                Vector2 vr = ReconButton.sizeDelta;
                DSDC = vr.y;
                AssaultButton.sizeDelta = vr;
                EnginnerButton.sizeDelta = vr;
                SupportButton.sizeDelta = vr;
                vr.y = DSDC + 10;
                ReconButton.sizeDelta = vr;
                UpdateClassUI(GetListId(PlayerClass.Recon, bl_ClassManager.PrimaryRecon), 0);
                UpdateClassUI(GetListId(PlayerClass.Recon, bl_ClassManager.SecundaryRecon), 1);
                UpdateClassUI(GetListId(PlayerClass.Recon, bl_ClassManager.KnifeRecon), 2);
                UpdateClassUI(GetListId(PlayerClass.Recon, bl_ClassManager.GrenadeRecon), 3);
                break;
        }
   
        PlayerPrefs.SetInt(ClassKey.ClassType, i);

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="clas"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    private int GetListId(PlayerClass clas,int id)
    {
        switch (clas)
        {
            case PlayerClass.Assault:
                for (int i = 0; i < AssaultClass.Count; i++)
                {
                    if (AssaultClass[i].ID == id)
                    {
                        return i;
                    }
                }
                break;
            case PlayerClass.Engineer:
                for (int i = 0; i < EngineerClass.Count; i++)
                {
                    if (EngineerClass[i].ID == id)
                    {
                        return i;
                    }
                }
                break;
            case PlayerClass.Support:
                for (int i = 0; i < SupportClass.Count; i++)
                {
                    if (SupportClass[i].ID == id)
                    {
                        return i;
                    }
                }
                break;
            case PlayerClass.Recon:
                for (int i = 0; i < ReconClass.Count; i++)
                {
                    if (ReconClass[i].ID == id)
                    {
                        return i;
                    }
                }
                break;
        }

        return 0;
    }
    void ResetClassHUD()
    {
        PrimaryHUD.WeaponNameText.text = "None";
        PrimaryHUD.Icon.sprite = null;
        PrimaryHUD.DamageSlider.value = 0;
        PrimaryHUD.AccuracySlider.value = 0;
        PrimaryHUD.RateSlider.value = 0;
        //
        SecundaryHUD.WeaponNameText.text = "None";
        SecundaryHUD.Icon.sprite = null;
        SecundaryHUD.DamageSlider.value = 0;
        SecundaryHUD.AccuracySlider.value = 0;
        SecundaryHUD.RateSlider.value = 0;
        //
        KnifeHUD.WeaponNameText.text = "None";
        KnifeHUD.Icon.sprite = null;
        KnifeHUD.DamageSlider.value = 0;
        KnifeHUD.AccuracySlider.value = 0;
        KnifeHUD.RateSlider.value = 0;
        //
        GrenadeHUD.WeaponNameText.text = "None";
        GrenadeHUD.Icon.sprite = null;
        GrenadeHUD.DamageSlider.value = 0;
        GrenadeHUD.AccuracySlider.value = 0;
        GrenadeHUD.RateSlider.value = 0;
    }
    /// <summary>
    /// Take the current class
    /// </summary>
    void TakeCurrentClass(PlayerClass mclass)
    {
        switch (mclass)
        {
            case PlayerClass.Assault:
                foreach (ClassInfo ci in AssaultClass)
                {
                    if (ci.ID == bl_ClassManager.PrimaryAssault)
                    {
                        PrimaryHUD.WeaponNameText.text = ci.WeaponName;
                        PrimaryHUD.Icon.sprite = ci.WeaponIcon;
                        PrimaryHUD.DamageSlider.value = ci.Damage;
                        PrimaryHUD.AccuracySlider.value = ci.Accuracy;
                        PrimaryHUD.RateSlider.value = ci.Rate;
                    }
                }
                foreach (ClassInfo ci in AssaultClass)
                {
                    if (ci.ID == bl_ClassManager.SecundaryAssault)
                    {
                        SecundaryHUD.WeaponNameText.text = ci.WeaponName;
                        SecundaryHUD.Icon.sprite = ci.WeaponIcon;
                        SecundaryHUD.DamageSlider.value = ci.Damage;
                        SecundaryHUD.AccuracySlider.value = ci.Accuracy;
                        SecundaryHUD.RateSlider.value = ci.Rate;
                    }
                }
                foreach (ClassInfo ci in AssaultClass)
                {
                    if (ci.ID == bl_ClassManager.KnifeAssault)
                    {
                        KnifeHUD.WeaponNameText.text = ci.WeaponName;
                        KnifeHUD.Icon.sprite = ci.WeaponIcon;
                        KnifeHUD.DamageSlider.value = ci.Damage;
                        KnifeHUD.AccuracySlider.value = ci.Accuracy;
                        KnifeHUD.RateSlider.value = ci.Rate;
                    }
                }
                foreach (ClassInfo ci in EngineerClass)
                {
                    if (ci.ID == bl_ClassManager.GrenadeAssault)
                    {
                        GrenadeHUD.WeaponNameText.text = ci.WeaponName;
                        GrenadeHUD.Icon.sprite = ci.WeaponIcon;
                        GrenadeHUD.DamageSlider.value = ci.Damage;
                        GrenadeHUD.AccuracySlider.value = ci.Accuracy;
                        GrenadeHUD.RateSlider.value = ci.Rate;
                    }
                }
                break;
            case PlayerClass.Engineer://---------------------------------------------------------------------------------
                foreach (ClassInfo ci in EngineerClass)
                {
                    if (ci.ID == bl_ClassManager.PrimaryEnginner)
                    {
                        PrimaryHUD.WeaponNameText.text = ci.WeaponName;
                        PrimaryHUD.Icon.sprite = ci.WeaponIcon;
                        PrimaryHUD.DamageSlider.value = ci.Damage;
                        PrimaryHUD.AccuracySlider.value = ci.Accuracy;
                        PrimaryHUD.RateSlider.value = ci.Rate;
                    }
                }
                foreach (ClassInfo ci in EngineerClass)
                {
                    if (ci.ID == bl_ClassManager.SecundaryEnginner)
                    {
                        SecundaryHUD.WeaponNameText.text = ci.WeaponName;
                        SecundaryHUD.Icon.sprite = ci.WeaponIcon;
                        SecundaryHUD.DamageSlider.value = ci.Damage;
                        SecundaryHUD.AccuracySlider.value = ci.Accuracy;
                        SecundaryHUD.RateSlider.value = ci.Rate;
                    }
                }
                foreach (ClassInfo ci in EngineerClass)
                {
                    if (ci.ID == bl_ClassManager.KnifeEnginner)
                    {
                        KnifeHUD.WeaponNameText.text = ci.WeaponName;
                        KnifeHUD.Icon.sprite = ci.WeaponIcon;
                        KnifeHUD.DamageSlider.value = ci.Damage;
                        KnifeHUD.AccuracySlider.value = ci.Accuracy;
                        KnifeHUD.RateSlider.value = ci.Rate;
                    }
                }
                foreach (ClassInfo ci in EngineerClass)
                {
                    if (ci.ID == bl_ClassManager.GrenadeEnginner)
                    {
                        GrenadeHUD.WeaponNameText.text = ci.WeaponName;
                        GrenadeHUD.Icon.sprite = ci.WeaponIcon;
                        GrenadeHUD.DamageSlider.value = ci.Damage;
                        GrenadeHUD.AccuracySlider.value = ci.Accuracy;
                        GrenadeHUD.RateSlider.value = ci.Rate;
                    }
                }
                break;
            case PlayerClass.Recon://-------------------------------------------------------------------------------------
                foreach (ClassInfo ci in ReconClass)
                {
                    if (ci.ID == bl_ClassManager.PrimaryRecon)
                    {
                        PrimaryHUD.WeaponNameText.text = ci.WeaponName;
                        PrimaryHUD.Icon.sprite = ci.WeaponIcon;
                        PrimaryHUD.DamageSlider.value = ci.Damage;
                        PrimaryHUD.AccuracySlider.value = ci.Accuracy;
                        PrimaryHUD.RateSlider.value = ci.Rate;
                    }
                }
                foreach (ClassInfo ci in ReconClass)
                {
                    if (ci.ID == bl_ClassManager.SecundaryRecon)
                    {
                        SecundaryHUD.WeaponNameText.text = ci.WeaponName;
                        SecundaryHUD.Icon.sprite = ci.WeaponIcon;
                        SecundaryHUD.DamageSlider.value = ci.Damage;
                        SecundaryHUD.AccuracySlider.value = ci.Accuracy;
                        SecundaryHUD.RateSlider.value = ci.Rate;
                    }
                }
                foreach (ClassInfo ci in ReconClass)
                {
                    if (ci.ID == bl_ClassManager.KnifeRecon)
                    {
                        KnifeHUD.WeaponNameText.text = ci.WeaponName;
                        KnifeHUD.Icon.sprite = ci.WeaponIcon;
                        KnifeHUD.DamageSlider.value = ci.Damage;
                        KnifeHUD.AccuracySlider.value = ci.Accuracy;
                        KnifeHUD.RateSlider.value = ci.Rate;
                    }
                }
                foreach (ClassInfo ci in ReconClass)
                {
                    if (ci.ID == bl_ClassManager.GrenadeRecon)
                    {
                        GrenadeHUD.WeaponNameText.text = ci.WeaponName;
                        GrenadeHUD.Icon.sprite = ci.WeaponIcon;
                        GrenadeHUD.DamageSlider.value = ci.Damage;
                        GrenadeHUD.AccuracySlider.value = ci.Accuracy;
                        GrenadeHUD.RateSlider.value = ci.Rate;
                    }
                }
                break;
            case PlayerClass.Support://--------------------------------------------------------------------------------------
                foreach (ClassInfo ci in SupportClass)
                {
                    if (ci.ID == bl_ClassManager.PrimarySupport)
                    {
                        PrimaryHUD.WeaponNameText.text = ci.WeaponName;
                        PrimaryHUD.Icon.sprite = ci.WeaponIcon;
                        PrimaryHUD.DamageSlider.value = ci.Damage;
                        PrimaryHUD.AccuracySlider.value = ci.Accuracy;
                        PrimaryHUD.RateSlider.value = ci.Rate;
                    }
                }
                foreach (ClassInfo ci in SupportClass)
                {
                    if (ci.ID == bl_ClassManager.SecundarySupport)
                    {
                        SecundaryHUD.WeaponNameText.text = ci.WeaponName;
                        SecundaryHUD.Icon.sprite = ci.WeaponIcon;
                        SecundaryHUD.DamageSlider.value = ci.Damage;
                        SecundaryHUD.AccuracySlider.value = ci.Accuracy;
                        SecundaryHUD.RateSlider.value = ci.Rate;
                    }
                }
                foreach (ClassInfo ci in SupportClass)
                {
                    if (ci.ID == bl_ClassManager.KnifeSupport)
                    {
                        KnifeHUD.WeaponNameText.text = ci.WeaponName;
                        KnifeHUD.Icon.sprite = ci.WeaponIcon;
                        KnifeHUD.DamageSlider.value = ci.Damage;
                        KnifeHUD.AccuracySlider.value = ci.Accuracy;
                        KnifeHUD.RateSlider.value = ci.Rate;
                    }
                }
                foreach (ClassInfo ci in SupportClass)
                {
                    if (ci.ID == bl_ClassManager.GrenadeSupport)
                    {
                        GrenadeHUD.WeaponNameText.text = ci.WeaponName;
                        GrenadeHUD.Icon.sprite = ci.WeaponIcon;
                        GrenadeHUD.DamageSlider.value = ci.Damage;
                        GrenadeHUD.AccuracySlider.value = ci.Accuracy;
                        GrenadeHUD.RateSlider.value = ci.Rate;
                    }
                }
                break;
        }
    }
   
}