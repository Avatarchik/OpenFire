using UnityEngine;
using System.Collections;

public class bl_ClassManager : MonoBehaviour {

    //When if new player and not have data saved
    //take this weapons ID for default
    [Header("Assault")]
    public int DefaulPrimaryAssault = 0;
    public int DefaulSecundaryAssault = 1;
    public int DefaulKnifeAssault = 2;
    public int DefaulGrenadeAssault = 3;
    [Header("Enginner")]
    public int DefaulPrimaryEnginner = 0;
    public int DefaulSecundaryEnginner = 1;
    public int DefaulKnifeEnginner = 2;
    public int DefaulGrenadeEnginner = 3;
    [Header("Support")]
    public int DefaulPrimarySupport = 0;
    public int DefaulSecundarySupport = 1;
    public int DefaulKnifeSupport = 2;
    public int DefaulGrenadeSupport = 3;
    [Header("Recon")]
    public int DefaulPrimaryRecon = 0;
    public int DefaulSecundaryRecon = 1;
    public int DefaulKnifeRecon = 2;
    public int DefaulGrenadeRecon = 3;


    //Global vars
    public static PlayerClass m_Class = PlayerClass.Assault;
    //
    public static int PrimaryAssault = 0;
    public static int SecundaryAssault = 1;
    public static int KnifeAssault = 2;
    public static int GrenadeAssault = 3;
    ///
    public static int PrimaryEnginner = 0;
    public static int SecundaryEnginner = 1;
    public static int KnifeEnginner = 2;
    public static int GrenadeEnginner = 3;
    //
    public static int PrimarySupport = 0;
    public static int SecundarySupport = 1;
    public static int KnifeSupport = 2;
    public static int GrenadeSupport = 3;
    //
    public static int PrimaryRecon = 0;
    public static int SecundaryRecon = 1;
    public static int KnifeRecon = 2;
    public static int GrenadeRecon = 3;

    public const string ClassManagerName = "ClassInformation";

    /// <summary>
    /// 
    /// </summary>
    void Awake()
    {
        this.gameObject.name = ClassManagerName;
        GetID();
        //This script if permanent
        DontDestroyOnLoad(gameObject);
        //avoid a wrong name (for acces)
        this.gameObject.name = ClassManagerName;
    }

    /// <summary>
    /// 
    /// </summary>
    void GetID()
    {
        int c = 0;
        if (PlayerPrefs.HasKey(ClassKey.ClassType)) { c = PlayerPrefs.GetInt(ClassKey.ClassType); }
        switch (c)
        {
            case 0 :
                m_Class = PlayerClass.Assault;
                break;
            case 1:
                m_Class = PlayerClass.Engineer;
                break;
            case 2:
                m_Class = PlayerClass.Support;
                break;
            case 3:
                m_Class = PlayerClass.Recon;
                break;
        }
        if (PlayerPrefs.HasKey(ClassKey.PrimaryAssault)) { PrimaryAssault = PlayerPrefs.GetInt(ClassKey.PrimaryAssault); } else { PrimaryAssault = DefaulPrimaryAssault; }
        if (PlayerPrefs.HasKey(ClassKey.SecundaryAssault)) { SecundaryAssault = PlayerPrefs.GetInt(ClassKey.SecundaryAssault); } else { SecundaryAssault = DefaulSecundaryAssault; }
        if (PlayerPrefs.HasKey(ClassKey.KnifeAssault)) { KnifeAssault = PlayerPrefs.GetInt(ClassKey.KnifeAssault); } else { KnifeAssault = DefaulKnifeAssault; }
        if (PlayerPrefs.HasKey(ClassKey.GrenadeAssault)) { GrenadeAssault = PlayerPrefs.GetInt(ClassKey.GrenadeAssault); } else { GrenadeAssault = DefaulGrenadeAssault; }

        if (PlayerPrefs.HasKey(ClassKey.PrimaryEnginner)) { PrimaryEnginner = PlayerPrefs.GetInt(ClassKey.PrimaryEnginner); } else { PrimaryEnginner = DefaulPrimaryEnginner; }
        if (PlayerPrefs.HasKey(ClassKey.SecundaryEnginner)) { SecundaryEnginner = PlayerPrefs.GetInt(ClassKey.SecundaryEnginner); } else { SecundaryEnginner = DefaulSecundaryEnginner; }
        if (PlayerPrefs.HasKey(ClassKey.KnifeEnginner)) { KnifeEnginner = PlayerPrefs.GetInt(ClassKey.KnifeEnginner); } else { KnifeEnginner = DefaulKnifeEnginner; }
        if (PlayerPrefs.HasKey(ClassKey.GrenadeEnginner)) { GrenadeEnginner = PlayerPrefs.GetInt(ClassKey.GrenadeEnginner); } else { GrenadeEnginner = DefaulGrenadeEnginner; }

        if (PlayerPrefs.HasKey(ClassKey.PrimarySupport)) { PrimarySupport = PlayerPrefs.GetInt(ClassKey.PrimarySupport); } else { PrimarySupport = DefaulPrimarySupport; }
        if (PlayerPrefs.HasKey(ClassKey.SecundarySupport)) { SecundarySupport = PlayerPrefs.GetInt(ClassKey.SecundarySupport); } else { SecundarySupport = DefaulSecundarySupport; }
        if (PlayerPrefs.HasKey(ClassKey.KnifeSupport)) { KnifeSupport = PlayerPrefs.GetInt(ClassKey.KnifeSupport); } else { KnifeSupport = DefaulKnifeSupport; }
        if (PlayerPrefs.HasKey(ClassKey.GrenadeSupport)) { GrenadeSupport = PlayerPrefs.GetInt(ClassKey.GrenadeSupport); } else { GrenadeSupport = DefaulGrenadeSupport; }

        if (PlayerPrefs.HasKey(ClassKey.PrimaryRecon)) { PrimaryRecon = PlayerPrefs.GetInt(ClassKey.PrimaryRecon); } else { PrimaryRecon = DefaulPrimaryRecon; }
        if (PlayerPrefs.HasKey(ClassKey.SecundaryRecon)) { SecundaryRecon = PlayerPrefs.GetInt(ClassKey.SecundaryRecon); } else { SecundaryRecon = DefaulSecundaryRecon; }
        if (PlayerPrefs.HasKey(ClassKey.KnifeRecon)) { KnifeRecon = PlayerPrefs.GetInt(ClassKey.KnifeRecon); } else { KnifeRecon = DefaulKnifeRecon; }
        if (PlayerPrefs.HasKey(ClassKey.GrenadeRecon)) { GrenadeRecon = PlayerPrefs.GetInt(ClassKey.GrenadeRecon); } else { GrenadeRecon = DefaulGrenadeRecon; }
    }
    /// <summary>
    /// 
    /// </summary>
    public static void SaveClass()
    {
        PlayerPrefs.SetInt(ClassKey.PrimaryAssault, PrimaryAssault);
        PlayerPrefs.SetInt(ClassKey.SecundaryAssault, SecundaryAssault);
        PlayerPrefs.SetInt(ClassKey.KnifeAssault, KnifeAssault);
        PlayerPrefs.SetInt(ClassKey.GrenadeAssault, GrenadeAssault);

        PlayerPrefs.SetInt(ClassKey.PrimaryEnginner, PrimaryEnginner);
        PlayerPrefs.SetInt(ClassKey.SecundaryEnginner, SecundaryEnginner);
        PlayerPrefs.SetInt(ClassKey.KnifeEnginner, KnifeEnginner);
        PlayerPrefs.SetInt(ClassKey.GrenadeEnginner, GrenadeEnginner);

        PlayerPrefs.SetInt(ClassKey.PrimarySupport, PrimarySupport);
        PlayerPrefs.SetInt(ClassKey.SecundarySupport, SecundarySupport);
        PlayerPrefs.SetInt(ClassKey.KnifeSupport, KnifeSupport);
        PlayerPrefs.SetInt(ClassKey.GrenadeSupport, GrenadeSupport);

        PlayerPrefs.SetInt(ClassKey.PrimaryRecon, PrimaryRecon);
        PlayerPrefs.SetInt(ClassKey.SecundaryRecon, SecundaryRecon);
        PlayerPrefs.SetInt(ClassKey.KnifeRecon, KnifeRecon);
        PlayerPrefs.SetInt(ClassKey.GrenadeRecon, GrenadeRecon);

        Debug.Log("Save Class");
    }
}