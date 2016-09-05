using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class bl_NetworkGun : MonoBehaviour
{

    public Transform FirePoint;
    public GameObject m_bullet;
    public GameObject m_muzzleFlash;
    /// <summary>
    /// This asigne auto not need asigne in inspector
    /// </summary>
    public int WeaponID;
    /// <summary>
    /// Local Gun reference (the same Gun but local)
    /// </summary>
    public bl_Gun IsGun;
    /// <summary>
    /// The type of gun wath is this (signe auto)
    /// </summary>
    public bl_Gun.weaponType m_weaponType = bl_Gun.weaponType.Machinegun;
    //private
    private bl_UpperAnimations m_upper;
    //Customizer Part
    [Space(5)]
    public bool recive = false;
    public ListCustomizer m_Customizer;
    protected int BarrelID;
    protected int OpticID;
    protected int CylinderID;
    protected int FeederID;

    void Awake()
    {
        GetComponent<AudioSource>().playOnAwake = false;
        m_upper = this.transform.root.GetComponentInChildren<bl_UpperAnimations>();
        if (IsGun != null)
        {
            WeaponID = IsGun.GunID;
            m_weaponType = IsGun.typeOfGun;
        }
        else
        {
            Debug.LogError("This NetworkGun No have reference of GUN");
        }
    }
    /// <summary>
    /// Update type each is enable 
    /// </summary>
    void OnEnable()
    {
        if (m_upper != null)
        {
            m_upper.m_WeaponType = this.m_weaponType;
        }
        else
        {
            Debug.LogError("No have bl_UpperAnimations.cs attach in this player");
        }
    }

    /// <summary>
    /// Fire Sync in network player
    /// </summary>
    public void Fire(float m_spread)
    {
        if (IsGun != null)
        {
            Vector3 position = FirePoint.position; // position to spawn bullet is at the muzzle point of the gun       
            //bullet info is set up in start function
            GameObject newBullet = Instantiate(m_bullet, position, FirePoint.rotation) as GameObject; // create a bullet
            // set the gun's info into an array to send to the bullet
            bl_BulletInitSettings t_info = new bl_BulletInitSettings();
            t_info.m_damage = 0;
            t_info.m_ImpactForce = 0;
            t_info.m_MaxPenetration = 0;
            t_info.m_maxspread = IsGun.maxSpread;
            t_info.m_spread = m_spread;
            t_info.m_speed = IsGun.bulletSpeed;
            t_info.m_weaponname = "";
            t_info.m_position = this.transform.root.position;
            t_info.isNetwork = true;

            newBullet.GetComponent<bl_Bullet>().SetUp(t_info);
            newBullet.GetComponent<bl_Bullet>().isTracer = true;
            GetComponent<AudioSource>().clip = IsGun.FireSound;
            GetComponent<AudioSource>().spread = Random.Range(1.0f, 1.5f);
            GetComponent<AudioSource>().Play();
        }
        if (m_muzzleFlash)
        {
            StartCoroutine(MuzzleFlash());
        }
    }

    /// <summary>
    /// When is knife only reply sounds
    /// </summary>
    public void KnifeFire()
    {
        if (IsGun != null)
        {
            GetComponent<AudioSource>().clip = IsGun.FireSound;
            GetComponent<AudioSource>().spread = Random.Range(1.0f, 1.5f);
            GetComponent<AudioSource>().Play();
        }
    }
    /// <summary>
    /// if grenade 
    /// </summary>
    /// <param name="s"></param>
    public void GrenadeFire(float s)
    {
        if (IsGun != null)
        {
            Vector3 position = FirePoint.position; // position to spawn bullet is at the muzzle point of the gun       
            //bullet info is set up in start function
            GameObject newBullet = Instantiate(m_bullet, position, FirePoint.rotation) as GameObject; // create a bullet
            // set the gun's info into an array to send to the bullet
            bl_BulletInitSettings t_info = new bl_BulletInitSettings();
            t_info.m_damage = 0;
            t_info.m_ImpactForce = 0;
            t_info.m_MaxPenetration = 0;
            t_info.m_maxspread = IsGun.maxSpread;
            t_info.m_spread = s;
            t_info.m_speed = IsGun.bulletSpeed;
            t_info.m_weaponname = "";
            t_info.m_position = this.transform.root.position;
            t_info.isNetwork = true;

            newBullet.GetComponent<bl_Grenade>().SetUp(t_info);
            GetComponent<AudioSource>().clip = IsGun.FireSound;
            GetComponent<AudioSource>().spread = Random.Range(1.0f, 1.5f);
            GetComponent<AudioSource>().Play();
        }
    }
    IEnumerator MuzzleFlash()
    {
        m_muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(0.04f);
        m_muzzleFlash.SetActive(false);

    }

    /// <summary>
    /// Receive info from RPC
    /// </summary>
    /// <param name="info"></param>
    public void ReadCustomizer(string info)
    {
        string m_Read = info;
        string[] descompile = m_Read.Split("-"[0]);
        BarrelID = int.Parse(descompile[0]);
        OpticID = int.Parse(descompile[1]);
        CylinderID = int.Parse(descompile[2]);
        FeederID = int.Parse(descompile[3]);
        RendererModule();
        recive = true;
    }

    /// <summary>
    /// 
    /// </summary>
    void RendererModule()
    {

        Debug.Log("RenderBuffer");
        foreach (infomodule IDBarrel in m_Customizer.Barrel)
        {
            if (IDBarrel.ID == BarrelID)
            {
                IDBarrel.model.SetActive(true);
            }
            else
            {
                IDBarrel.model.SetActive(false);
            }
        }

        foreach (infomodule IDOptics in m_Customizer.Optics)
        {
            if (IDOptics.ID == OpticID)
            {
                IDOptics.model.SetActive(true);
            }
            else
            {
                IDOptics.model.SetActive(false);
            }
        }

        foreach (infomodule IDFeeder in m_Customizer.Feeder)
        {
            if (IDFeeder.ID == FeederID)
            {
                IDFeeder.model.SetActive(true);
            }
            else
            {
                IDFeeder.model.SetActive(false);
            }
        }

        foreach (infomodule IDCylinder in m_Customizer.Cylinder)
        {
            if (IDCylinder.ID == CylinderID)
            {
                IDCylinder.model.SetActive(true);
            }
            else
            {
                IDCylinder.model.SetActive(false);
            }
        }
    }
}