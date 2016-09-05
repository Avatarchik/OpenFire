//--------------------------------UMLoadSettings-------------------------
//use this example to load saved settings.
//
//------------------------------Lovatto Studio---------------------------

using UnityEngine;
using System.Collections;

public class UMLoadSettings : MonoBehaviour {

    public float m_volume;
    public float m_Sensitivity;
    public int m_Resolution;
    public string m_playername;
    public int m_Quality;

	// Use this for initialization
    void Start()
    {
        if (PlayerPrefs.HasKey(UMKeyOptions.VolumeKey))
        {
            m_volume = PlayerPrefs.GetFloat(UMKeyOptions.VolumeKey);
        }
        if (PlayerPrefs.HasKey(UMKeyOptions.SensitivityKey))
        {
            m_Sensitivity = PlayerPrefs.GetFloat(UMKeyOptions.SensitivityKey);
        }
        if (PlayerPrefs.HasKey(UMKeyOptions.ResolutionKey))
        {
            #if !UNITY_EDITOR
            m_Resolution = PlayerPrefs.GetInt(UMKeyOptions.ResolutionKey);
            Screen.SetResolution(Screen.resolutions[PlayerPrefs.GetInt(UMKeyOptions.ResolutionKey)].width,
            Screen.resolutions[PlayerPrefs.GetInt(UMKeyOptions.ResolutionKey)].height, true);
            #endif
        }
        if (PlayerPrefs.HasKey(UMKeyOptions.PlayerNameKey))
        {
            m_playername = PlayerPrefs.GetString(UMKeyOptions.PlayerNameKey);
        }
        if (PlayerPrefs.HasKey(UMKeyOptions.QualityKey))
        {
            m_Quality = PlayerPrefs.GetInt(UMKeyOptions.QualityKey);
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt(UMKeyOptions.QualityKey),true);
        }
    }

    void OnGUI()
    {
        GUILayout.Label(m_playername);
        GUILayout.Label("Volume: " + m_volume);
        GUILayout.Label("Sensitivity: " + m_Sensitivity);
        GUILayout.Label("Quality: " + m_Quality);
         #if !UNITY_EDITOR
        GUILayout.Label("Resolution: " + Screen.resolutions[m_Resolution]);
         #endif
    }
}