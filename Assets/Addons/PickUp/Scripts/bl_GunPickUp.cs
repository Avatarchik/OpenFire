using UnityEngine;
using System.Collections;

public class bl_GunPickUp : bl_PhotonHelper
{

    public int Gun_ID = 0;
    public string Gun_Name = "";
    [HideInInspector]
    public bool PickupOnCollide = true;
    [HideInInspector]
    public bool SentPickup = false;
    [Space(5)]
    public bool AutoDestroy = false;
    public float DestroyIn = 15f;
    //
    private bool Into = false;
    private bl_PickGunManager PUM;

    //Cache info
    [System.Serializable]
    public class m_Info
    {
        public int Clips = 0;
        public int Bullets = 0;
    }
    public m_Info Info;
    /// <summary>
    /// 
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        if (AutoDestroy)
        {
            Destroy(gameObject, DestroyIn);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IEnumerator Start()
    {
        PickupOnCollide = false;
        yield return new WaitForSeconds(3f);
        PickupOnCollide = true;
        PUM = GameObject.Find(bl_PickGunManager.PickUpManagerName).GetComponent<bl_PickGunManager>();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="c"></param>
    void OnTriggerEnter(Collider c)
    {
        // we only call Pickup() if "our" character collides with this PickupItem.
        // note: if you "position" remote characters by setting their translation, triggers won't be hit.

        PhotonView v = c.GetComponent<PhotonView>();
        if (this.PickupOnCollide && v != null && v.isMine)
        {
            Into = true;
        }

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="c"></param>
    void OnTriggerExit(Collider c)
    {
        if (c.transform.tag == bl_PlayerSettings.LocalTag)
        {
            Into = false;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        if (!Into)
            return;

        if ( Input.GetKeyDown(KeyCode.E))
        {
            this.Pickup();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void Pickup()
    {
        if (this.SentPickup)
        {
            // skip sending more pickups until the original pickup-RPC got back to this client
            return;
        }
        this.SentPickup = true;
       PUM.SendPickUp(this.gameObject.name, Gun_ID,Info);
        this.SentPickup = false;
    }

    void OnGUI()
    {
        if (Into)
        {
            bl_UtilityHelper.ShadowLabel(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 100, 200, 50), "Press [E] for pick up <color=#70ABFF>" + Gun_Name + "</color>");
        }
    }
}