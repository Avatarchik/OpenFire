using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class bl_PhotonConnection_UMenu : Photon.MonoBehaviour
{


    private LobbyState m_state = LobbyState.PlayerName;
    public List<GameObject> MenusUI = new List<GameObject>();
    private string playerName;
    private string hostName; //Name of room
    [Header("Photon")]
    public string AppID = string.Empty;
    public string AppVersion = "1.0";
    public int Port = 5055;
    [Space(5)]
    public GUISkin Skin;
    private float alpha = 2.0f;

    //OPTIONS
    [HideInInspector]
    public bool GamePerRounds = false;
    private bool AutoTeamSelection = false;

    public bool ShowPhotonStatus = false;
    public bool ShowPhotonStatics = true;
    [HideInInspector]
    public bool loading = false;
    [Space(5)]
    public string[] GameModes;
    private int CurrentGameMode = 0;

    //Max players in game
    public int[] maxPlayers;
    private int players;
    //Room Time in seconds
    public int[] RoomTime;
    private int r_Time;
    //SERVERLIST
    private RoomInfo[] roomList;
    private Vector2 scroll;
    [Space(5)]
    [Header("Effects")]
    public AudioClip a_Click;
    public AudioClip backSound;
    [System.Serializable]
    public class AllScenes
    {
        public string m_name;
        public string m_SceneName;
        public Sprite m_Preview;
    }
    public List<AllScenes> m_scenes = new List<AllScenes>();
    private int CurrentScene = 0;
    [Space(5)]
    [Header("UI")]
    public GameObject Menus;
    public GameObject Buttons;
    public GameObject Loading;
    public GameObject PlayerNameUI;
    [Space(5)]
    public Transform PanelList;
    public GameObject RoomInfoPrefab;
    public InputField HostNameI;
    [Space(5)]
    public Image MapPreview;
    public Text GameModeText;
    public Text PlayersText;
    public Text TimeText;
    public Text PhotonStatus;
    public Text RoomListStatus;
    public Text PhotonInfo;

    private List<GameObject> CacheRoomList = new List<GameObject>();
    private bl_SaveInfo m_SaveInfo;

    void Awake()
    {
        if (!PhotonNetwork.connected)
        {
            Menus.SetActive(false);
            Buttons.SetActive(false);
            PlayerNameUI.SetActive(false);
            Loading.SetActive(true);
        }
        // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
        PhotonNetwork.automaticallySyncScene = true;

        // the following line checks if this client was just created (and not yet online). if so, we connect
        if (!PhotonNetwork.connected || PhotonNetwork.connectionStateDetailed == PeerState.PeerCreated)
        {
            PhotonNetwork.ConnectUsingSettings(AppVersion);
        }
        hostName = "myRoom" + Random.Range(10, 999);
        HostNameI.text = hostName;

        InvokeRepeating("UpdateRoomList", 1, 3);
        MapPreview.sprite = m_scenes[CurrentScene].m_Preview;
        GameModeText.text = GameModes[CurrentGameMode];
        PlayersText.text = maxPlayers[players].ToString();
        TimeText.text = (RoomTime[r_Time] / 60) + " <size=12>Min</size>";

        //Get Save Script
        if (GameObject.Find("PlayerInfo") != null)
        {
            m_SaveInfo = GameObject.Find("PlayerInfo").GetComponent<bl_SaveInfo>();
        }
        else
        {
            Debug.LogError("Need Login firts for all work correctly");
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="t_state"></param>
    /// <returns></returns>
    IEnumerator Fade(LobbyState t_state)
    {
        alpha = 0.0f;
        m_state = t_state;
        loading = true;
        while (alpha < 2.0f)
        {
            alpha += Time.deltaTime / 0.5f;
            yield return 0;
        }

        loading = false;
    }

    /// <summary>
    /// 
    /// </summary>
    public void UpdateRoomList()
    {
        if (!PhotonNetwork.connected)
            return;

        RoomListStatus.text = "";
        //Removed old list
        if (CacheRoomList.Count > 0)
        {
            foreach (GameObject g in CacheRoomList)
            {
                Destroy(g);
            }
        }
        //Update List
        RoomInfo[] ri = PhotonNetwork.GetRoomList();
        if (ri.Length > 0)
        {
            for (int i = 0; i < ri.Length; i++)
            {
                GameObject r = Instantiate(RoomInfoPrefab) as GameObject;
                CacheRoomList.Add(r);
                r.GetComponent<bl_RoomInfo>().GetInfo(ri[i]);
                r.transform.SetParent(PanelList, false);
            }
        }
        else
        {
            RoomListStatus.text = "There is no room created.";
        }

    }

    public void LoadRanking() { PhotonNetwork.Disconnect(); Application.LoadLevel("Ranking"); }
    public void LoadCustomizer() { PhotonNetwork.Disconnect(); Application.LoadLevel("Customizer"); }
    public void LoadCustomClass() { PhotonNetwork.Disconnect(); Application.LoadLevel("ClassCustomizer"); }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="i"></param>
    public void GetPlayerName(InputField i)
    {
        if (i != null && i.text != string.Empty)
        {
            PhotonNetwork.player.name = i.text;
            PlayerNameUI.SetActive(false);
            Menus.SetActive(true);
            Buttons.SetActive(true);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="b"></param>
    public void ChangeMap(bool b)
    {
        if (b)
        {
            if (CurrentScene < m_scenes.Count)
            {
                CurrentScene++;
                if (CurrentScene > (m_scenes.Count - 1))
                {
                    CurrentScene = 0;
                }
            }
        }
        else
        {
            if (CurrentScene < m_scenes.Count)
            {
                CurrentScene--;
                if (CurrentScene < 0)
                {
                    CurrentScene = m_scenes.Count - 1;

                }
            }
        }

        MapPreview.sprite = m_scenes[CurrentScene].m_Preview;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="b"></param>
    public void ChangerGameMode(bool b)
    {
        if (b)
        {
            if (CurrentGameMode < GameModes.Length)
            {
                CurrentGameMode--;
                if (CurrentGameMode < 0)
                {
                    CurrentGameMode = GameModes.Length - 1;

                }
            }
        }
        else
        {
            if (CurrentGameMode < GameModes.Length)
            {
                CurrentGameMode++;
                if (CurrentGameMode > (GameModes.Length - 1))
                {
                    CurrentGameMode = 0;
                }
            }
        }
        GameModeText.text = GameModes[CurrentGameMode];
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="b"></param>
    public void ChangerTime(bool b)
    {
        if (b)
        {
            if (r_Time < RoomTime.Length)
            {
                r_Time++;
                if (r_Time > (RoomTime.Length - 1))
                {
                    r_Time = 0;

                }

            }
        }
        else
        {
            if (r_Time < RoomTime.Length)
            {
                r_Time--;
                if (r_Time < 0)
                {
                    r_Time = RoomTime.Length - 1;

                }
            }
        }
        TimeText.text = (RoomTime[r_Time] / 60) + " <size=12>Min</size>";
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="b"></param>
    public void ChangePlayers(bool b)
    {
        if (b)
        {
            if (players < maxPlayers.Length)
            {
                players++;
                if (players > (maxPlayers.Length - 1)) players = 0;

            }
        }
        else
        {
            if (players < maxPlayers.Length)
            {
                players--;
                if (players < 0) players = maxPlayers.Length - 1;
            }
        }
        PlayersText.text = maxPlayers[players].ToString();
    }
    public void AutoTeam(bool b) { AutoTeamSelection = b; }
    public void GamePerRound(bool b) { GamePerRounds = b; }
    /// <summary>
    /// 
    /// </summary>
    public void CreateRoom(InputField i)
    {
        // PhotonNetwork.player.name = playerName;
        //Save Room properties for load in room
        ExitGames.Client.Photon.Hashtable roomOption = new ExitGames.Client.Photon.Hashtable();
        roomOption[PropiertiesKeys.TimeRoomKey] = RoomTime[r_Time];
        roomOption[PropiertiesKeys.GameModeKey] = GameModes[CurrentGameMode];
        roomOption[PropiertiesKeys.SceneNameKey] = m_scenes[CurrentScene].m_SceneName;
        roomOption[PropiertiesKeys.RoomRoundKey] = GamePerRounds ? "1" : "0";
        roomOption[PropiertiesKeys.TeamSelectionKey] = AutoTeamSelection ? "1" : "0";

        string[] properties = new string[5];
        properties[0] = PropiertiesKeys.TimeRoomKey;
        properties[1] = PropiertiesKeys.GameModeKey;
        properties[2] = PropiertiesKeys.SceneNameKey;
        properties[3] = PropiertiesKeys.RoomRoundKey;
        properties[4] = PropiertiesKeys.TeamSelectionKey;

        PhotonNetwork.CreateRoom(i.text, new RoomOptions()
        {
            maxPlayers = (byte)maxPlayers[players],
            isVisible = true,
            isOpen = true,
            customRoomProperties = roomOption,
            cleanupCacheOnLeave = true,
            customRoomPropertiesForLobby = properties
        }, null);
    }

    void Update()
    {
        if (PhotonStatus != null)
        {
            PhotonStatus.text = "Connection State: <color=#549BFF>" + PhotonNetwork.connectionStateDetailed.ToString() + "</color>";
        }
        if (PhotonInfo != null)
        {
            PhotonInfo.text = "<color=orange>" + PhotonNetwork.countOfPlayersInRooms + "</color> Players in Rooms   " +
           "<color=orange>" + PhotonNetwork.countOfPlayersOnMaster + "</color> Players in Lobby   " +
            "<color=orange>" + PhotonNetwork.countOfPlayers + "</color> Players in Server   " +
            "<color=orange>" + PhotonNetwork.countOfRooms + "</color> Room are Create";
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="position"></param>
    /// <param name="volume"></param>
    /// <returns></returns>
    AudioSource PlayAudioClip(AudioClip clip, Vector3 position, float volume)
    {
        GameObject go = new GameObject("One shot audio");
        go.transform.position = position;
        AudioSource source = go.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = volume;
        source.Play();
        Destroy(go, clip.length);
        return source;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private IEnumerator MoveToGameScene()
    {
        //Wait for check
        while (PhotonNetwork.room == null)
        {
            yield return 0;
        }
        PhotonNetwork.isMessageQueueRunning = false;
        Application.LoadLevel((string)PhotonNetwork.room.customProperties[PropiertiesKeys.SceneNameKey]);
    }
    // LOBBY EVENTS

    void OnJoinedLobby()
    {
        Debug.Log("We joined the lobby.");
        //Active this is you need integrate with Login System
        
        if (m_SaveInfo != null)
        {
            //if save info get in script then skip playername menu
            if (m_SaveInfo.m_UserName != string.Empty && m_SaveInfo.m_UserName != null)
            {
                //Get Info
                playerName = m_SaveInfo.m_UserName;
                PhotonNetwork.player.name = playerName;

                Loading.SetActive(false);
                PlayerNameUI.SetActive(false);
                Menus.SetActive(true);
                Buttons.SetActive(true);

            }
        }
        else
        {
            Debug.LogWarning("can not get the information from the SaveInfo Script");
            Loading.SetActive(false);
            PlayerNameUI.SetActive(true);
        }
    }

    void OnLeftLobby()
    {
        Debug.Log("We left the lobby.");
    }

    // ROOMLIST

    void OnReceivedRoomList()
    {
        Debug.Log("We received a new room list, total rooms: " + PhotonNetwork.GetRoomList().Length);
    }

    void OnReceivedRoomListUpdate()
    {
        Debug.Log("We received a room list update, total rooms now: " + PhotonNetwork.GetRoomList().Length);
    }

    void OnJoinedRoom()
    {
        Debug.Log("We have joined a room.");
        StartCoroutine(MoveToGameScene());
    }
    void OnFailedToConnectToPhoton(DisconnectCause cause)
    {
        Debug.LogWarning("OnFailedToConnectToPhoton: " + cause);
        loading = false;
    }
    void OnConnectionFail(DisconnectCause cause)
    {
        Debug.LogWarning("OnConnectionFail: " + cause);
    }


    private int GetButtonSize(LobbyState t_state)
    {

        if (m_state == t_state)
        {
            return 55;
        }
        else
        {
            return 40;
        }
    }
}
