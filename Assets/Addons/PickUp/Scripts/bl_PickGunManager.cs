using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class bl_PickGunManager : bl_PhotonHelper {

    public List<bl_GunPickUp> Guns = new List<bl_GunPickUp>();
    public float ForceImpulse = 350;
    public const string PickUpManagerName = "PickUpManager";


    protected override void Awake()
    {
        this.gameObject.name = PickUpManagerName;
    }

    /// <summary>
    /// 
    /// </summary>
    public void TrownGun(int id,Vector3 point,int[] info)
    {
        int[] i = new int[3];
        i[0] = info[0];
        i[1] = info[1];
        i[2] = bl_GameManager.m_view;
        //prevent the go has an existing name
        int rand = Random.Range(0, 9999);
        //unique go name
        string prefix = PhotonNetwork.player.name +rand;
        photonView.RPC("TrowGunRPC", PhotonTargets.AllBuffered, id,point,prefix,i);
    }

    [PunRPC]
    void TrowGunRPC(int id,Vector3 point,string prefix,int[] info)
    {
        GameObject trow = null;
        foreach (bl_GunPickUp g in Guns)
        {
            if (g.Gun_ID == id)
            {
                trow = g.gameObject;
            }
        }
        if (trow == null)
        {
            Debug.LogError("Please attach the gun into list");
            return;
        }
        GameObject p = FindPlayerRoot(info[2]);
        GameObject gun = Instantiate(trow, point, Quaternion.identity) as GameObject;
        Collider[] c = FindPlayerRoot(info[2]).GetComponentsInChildren<Collider>();
        for (int i = 0; i < c.Length; i++)
        {
            Physics.IgnoreCollision(c[i], gun.GetComponent<Collider>());
        }
        gun.GetComponent<Rigidbody>().AddForce(p.transform.forward * ForceImpulse);     
        gun.GetComponent<bl_GunPickUp>().Info.Clips = info[0];
        gun.GetComponent<bl_GunPickUp>().Info.Bullets = info[1];
        gun.name = gun.name.Replace("(Clone)", "");
        gun.name += prefix;
        gun.transform.parent = this.transform;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="n"></param>
    /// <param name="id"></param>
    public void SendPickUp(string n, int id, bl_GunPickUp.m_Info gp)
    {
        int[] i = new int[2];
        i[0] = gp.Clips;
        i[1] = gp.Bullets;
        photonView.RPC("PickUp", PhotonTargets.AllBuffered, n, id,i);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mName"></param>
    /// <param name="msgInfo"></param>
    [PunRPC]
    void PickUp(string mName,int id,int[] info, PhotonMessageInfo msgInfo)
    {
        // one of the messages might be ours
        // note: you could check "active" first, if you're not interested in your own, failed pickup-attempts.
        GameObject g = GameObject.Find(mName);
        if (g == null)
        {
            Debug.LogError("This Gun does not exist in scene");
            return;
        }
        //is mine
        if (msgInfo.sender == PhotonNetwork.player)
        {
            bl_OnPickUpInfo pi = new bl_OnPickUpInfo();
            pi.ID = id;
            pi.ItemObject = g;
            pi.Clips = info[0];
            pi.Bullets = info[1];
            bl_EventHandler.OnPickUpGun(pi);
        }
                Destroy(g);
    }
}