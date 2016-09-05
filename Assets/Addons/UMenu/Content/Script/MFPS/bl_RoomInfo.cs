using UnityEngine;
using UnityEngine.UI;

public class bl_RoomInfo : MonoBehaviour  {

    public Text RoomNameUI;
    public Text MapNameUI;
    public Text MaxPlayerUI;
    public Text TypeUI;

    [HideInInspector]
    public RoomInfo m_Room;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="rn"></param>
    /// <param name="mn"></param>
    /// <param name="p"></param>
    /// <param name="mType"></param>
    public void GetInfo(RoomInfo r)
    {
        m_Room = r;
        RoomNameUI.text = r.name;
        MapNameUI.text = (string)r.customProperties[PropiertiesKeys.SceneNameKey];
        MaxPlayerUI.text = r.playerCount + " / " + r.maxPlayers;
        TypeUI.text = (string)r.customProperties[PropiertiesKeys.GameModeKey];
    }
    /// <summary>
    /// 
    /// </summary>
    public void EnterRoom()
    {
        if (m_Room.playerCount < m_Room.maxPlayers)
        {
            PhotonNetwork.JoinRoom(m_Room.name);
        }
        else
        {
            Debug.Log("This Room is Full");
        }
    }
}