using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class UMenuManager : MonoBehaviour {

    /// <summary>
    /// Reference of player level
    /// you can replace with your out system of leveling
    /// </summary>
    public static int PlayerLevel = 5;

    public GameObject[] GO_Menus;
    [Header("Main Menu")]
    public Image LevelPreview;
    public Text LevelName;
    [System.Serializable]
    public class _Levels
    {
        public string m_level;
        public Sprite m_levelreview;
        public int LevelRequired = 0;
        public int Rate = 5;
    }
    public List<_Levels> m_levels = new List<_Levels>();
    [Header("Player Info")]
    public RawImage m_avatarUI;
    public string m_avatarURL = "";
    private float ListLevelScale ;
    public GameObject NewAvatar;
    public InputField m_UrlField;
    public Text m_PlayerNamePerfil;
    [Header("Options")]
    public Slider m_VolumeSlider;
    public Slider m_SensitivitySlider;
    public InputField m_InputName;
    public int m_Quality = 3;
    public int m_ResolutionCurrent = 3;
    [Header("Audio")]
    public bool DownloadAudio = true;
    public string m_AudioURL = ""; //Url of a audio .waw or .oggvorvis
    [Range(0.1f,1f)]
    public float m_Volume = 1.0f;

    /// <summary>
    /// 
    /// </summary>
    void Awake()
    {
       // InstanceUIAwake();
        //if you have saved a url, the load this on start
        if (PlayerPrefs.HasKey(UMKeyOptions.AvatarUrlKey))
        {
            m_avatarURL = PlayerPrefs.GetString(UMKeyOptions.AvatarUrlKey);
            StartCoroutine(DownloadImage());
        }
        ChangeMenu(MenuState.MainMenu);
        GetSettings();

    }
    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        if (m_AudioURL != "" && DownloadAudio)
        {
            StartCoroutine(LoadAudio());
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// 
    IEnumerator LoadAudio()
    {
        WWW www = new WWW(m_AudioURL);
        if (www.error != null) Debug.Log(www.error);
        GetComponent<AudioSource>().clip = www.audioClip;

        while (!www.isDone)
        {

            yield return new WaitForSeconds(0.2f);
            if (www.progress == 1)
            {
                GetComponent<AudioSource>().clip = www.audioClip;
                GetComponent<AudioSource>().Play();
            }
        }
    }

    /// <summary>
    /// Create a custom List with UI 4.6
    /// </summary>
  /*  void InstanceUIAwake()
    {
        if (LevelButtonPrefab != null)
        {
            for (int i = 0; i < m_levels.Count; i++)
            {
                GameObject button = Instantiate(LevelButtonPrefab) as GameObject;
                UMEventHelper helper = button.GetComponent<UMEventHelper>();
                helper.GetInfo(m_levels[i].m_level, m_levels[i].m_levelreview, m_levels[i].Rate, m_levels[i].LevelRequired);
                Text text = button.GetComponentInChildren<Text>();
                if (m_levels[i].LevelRequired <= UMenuManager.PlayerLevel)
                {
                    text.text = m_levels[i].m_level;
                }
                else
                {
                    text.text = m_levels[i].m_level + " <color=red>[Lock]</color> \n <size=9>Required Level: " + m_levels[i].LevelRequired+"</size>";
                }
                button.transform.SetParent(LevelPanel,false);
            }
        }
    }*/
    /// <summary>
    /// 
    /// </summary>
    public void OnMainMenuClick()
    {
        ChangeMenu(MenuState.MainMenu);
    }
    /// <summary>
    /// 
    /// </summary>
    public void OnSecondMenuClick()
    {
        ChangeMenu(MenuState.SecondMenu);
    }
    /// <summary>
    /// 
    /// </summary>
    public void OnOptionsClick()
    {
        ChangeMenu(MenuState.Options);
    }
    /// <summary>
    /// 
    /// </summary>
    public void OnCreditsClick()
    {
        ChangeMenu(MenuState.Credits);
    }
    /// <summary>
    /// 
    /// </summary>
    public void OnQuitClick()
    {
        ChangeMenu(MenuState.Quit);
    }
    /// <summary>
    /// Change Menu 
    /// </summary>
    /// <param name="t_state"></param>
    void ChangeMenu(MenuState t_state)
    {
        switch (t_state)
        {
            case MenuState.MainMenu:
                GO_Menus[0].SetActive(true);
                GO_Menus[1].SetActive(false);
                GO_Menus[2].SetActive(false);
                GO_Menus[3].SetActive(false);
                GO_Menus[4].SetActive(false);
                break;
            case MenuState.SecondMenu:
                GO_Menus[0].SetActive(false);
                GO_Menus[1].SetActive(true);
                GO_Menus[2].SetActive(false);
                GO_Menus[3].SetActive(false);
                GO_Menus[4].SetActive(false);
                break;
            case MenuState.Options:
                GO_Menus[0].SetActive(false);
                GO_Menus[1].SetActive(false);
                GO_Menus[2].SetActive(true);
                GO_Menus[3].SetActive(false);
                GO_Menus[4].SetActive(false);
                break;
            case MenuState.Credits:
                GO_Menus[0].SetActive(false);
                GO_Menus[1].SetActive(false);
                GO_Menus[2].SetActive(false);
                GO_Menus[3].SetActive(true);
                GO_Menus[4].SetActive(false);
                break;
            case MenuState.Quit:
                GO_Menus[0].SetActive(false);
                GO_Menus[1].SetActive(false);
                GO_Menus[2].SetActive(false);
                GO_Menus[3].SetActive(false);
                GO_Menus[4].SetActive(true);
                break;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public void NewAvatarWindow()
    {
        if (NewAvatar != null)
        {
            if (NewAvatar.activeSelf)
            {
                NewAvatar.SetActive(false);
            }
            else
            {
                NewAvatar.SetActive(true);
            }
        }
    }
    /// <summary>
    /// Download a image 
    /// </summary>
    public void SearchAvatar()
    {
        if (m_UrlField == null)
            return;

        if (m_UrlField.text != null)
        {
            m_avatarURL = m_UrlField.text;
            PlayerPrefs.SetString(UMKeyOptions.AvatarUrlKey, m_avatarURL);
            StartCoroutine(DownloadImage());
        }
    }
    /// <summary>
    /// Get image
    /// </summary>
    /// <returns></returns>
    IEnumerator DownloadImage()
    {
        // Start a download of the given URL
        WWW www = new WWW(m_avatarURL);
		// Wait for download to complete
		yield return www;
		// assign texture
        m_avatarUI.texture = www.texture;
    }
    /// <summary>
    /// Open a url function 
    /// </summary>
    /// <param name="m_url"></param>
    public void OpenURL(string m_url)
    {
        Application.OpenURL(m_url);
    }
    /// <summary>
    /// Update and save options Auto
    /// for load save options use ej.  PlayerPrefs.GetFloat(KeySave);
    /// </summary>
    public void UpdateOptions()
    {
        if (m_VolumeSlider != null)
        {
            AudioListener.volume = m_VolumeSlider.value;
        }       
    }

    void FixedUpdate()
    {
        if (!PhotonNetwork.connected)
            return;

        m_PlayerNamePerfil.text = "Player Name: " + PhotonNetwork.player.name;
    }
    public void ChangeQuality(int m_quality)
    {
        m_Quality = m_quality;
    }

    public void ChangeResolution(int re)
    {
        m_ResolutionCurrent = re;
    }
    /// <summary>
    /// Apply ans save all settings 
    /// </summary>
    public void ApplyOptions()
    {
        if (m_VolumeSlider != null)
        {
            PlayerPrefs.SetFloat(UMKeyOptions.VolumeKey, m_VolumeSlider.value);
        }
        if (m_SensitivitySlider != null)
        {
            PlayerPrefs.SetFloat(UMKeyOptions.SensitivityKey, m_SensitivitySlider.value);
        }
        if (m_InputName != null)
        {
            PlayerPrefs.SetString(UMKeyOptions.PlayerNameKey, m_InputName.text);
            if (m_PlayerNamePerfil != null)
            {
                m_PlayerNamePerfil.text = "Player Name: "+PlayerPrefs.GetString(UMKeyOptions.PlayerNameKey);
                PhotonNetwork.player.name = PlayerPrefs.GetString(UMKeyOptions.PlayerNameKey);
            }
        }
        PlayerPrefs.SetInt(UMKeyOptions.ResolutionKey, m_ResolutionCurrent);
        PlayerPrefs.SetInt(UMKeyOptions.QualityKey, m_Quality);
        #if !UNITY_EDITOR
        Screen.SetResolution(Screen.resolutions[m_ResolutionCurrent].width, Screen.resolutions[m_ResolutionCurrent].height, true);
#endif

    }
    /// <summary>
    /// Use this for load the current select level
    /// </summary>
    /// 
    public void LoadScene()
    {
        //enable this line when you have your scene in build settings
        Debug.Log("Enabled this line when you have yours scenes ready.");
       // Application.LoadLevel(currentLevel);
        
    }

    public void QuitApp()
    {
        PhotonNetwork.Disconnect();
        Application.LoadLevel("Login");
    }


    /// <summary>
    /// Get Settings saved in PlayerPrefs
    /// and apply to UI
    /// </summary>
    void GetSettings()
    {
        if (m_VolumeSlider != null && PlayerPrefs.HasKey(UMKeyOptions.VolumeKey))
        {
            m_VolumeSlider.value = PlayerPrefs.GetFloat(UMKeyOptions.VolumeKey);
        }
        if (m_SensitivitySlider != null && PlayerPrefs.HasKey(UMKeyOptions.SensitivityKey))
        {
            m_SensitivitySlider.value = PlayerPrefs.GetFloat(UMKeyOptions.SensitivityKey);
        }
        if (m_InputName != null && PlayerPrefs.HasKey(UMKeyOptions.PlayerNameKey))
        {
            m_InputName.text = PlayerPrefs.GetString(UMKeyOptions.PlayerNameKey);
            if (m_PlayerNamePerfil != null)
            {
                m_PlayerNamePerfil.text = "Player Name: " + PlayerPrefs.GetString(UMKeyOptions.PlayerNameKey);
            }
        }
    }
}