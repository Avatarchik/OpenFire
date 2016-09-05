/////////////////////////////////////////////////////////////////////////////////
///////////////////////////bl_GunManager.cs//////////////////////////////////////
/////////////Use this to manage all weapons Player///////////////////////////////
/////////////////////////////////////////////////////////////////////////////////
////////////////////////////////Briner Games/////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class bl_GunManager : MonoBehaviour
{
    /// <summary>
    /// all the Guns of game
    /// </summary>
    public List<bl_Gun> AllGuns = new List<bl_Gun>();
    /// <summary>
    /// weapons that the player take equipped
    /// </summary>
    public List<bl_Gun> PlayerEquip = new List<bl_Gun>();

    /// <summary>
    /// ID the weapon to take to start
    /// </summary>
    public int m_Current = 0;
    /// <summary>
    /// weapon used by the player currently
    /// </summary>
    public bl_Gun CurrentGun;
    /// <summary>
    /// Point where guns instantiate when trow
    /// </summary>
    public Transform TrowPoint = null;
    /// <summary>
    /// time it takes to switch weapons
    /// </summary>
    public float SwichTime = 1;

    public float PickUpTime = 0.4f;
    /// <summary>
    /// Can Swich Now?
    /// </summary>
    public bool CanSwich;
    [Space(5)]
    public Animator m_HeatAnimator;
    private bl_PickGunManager PUM;

    /// <summary>
    /// 
    /// </summary>
    void Awake()
    {

        //Desactive all weapons in children and take the firts
        GetClass();
        foreach (bl_Gun guns in AllGuns)
        {
            guns.gameObject.SetActive(false);
        }
        TakeWeapon(PlayerEquip[m_Current].gameObject);
        PUM = GameObject.Find(bl_PickGunManager.PickUpManagerName).GetComponent<bl_PickGunManager>();

    }
    /// <summary>
    /// 
    /// </summary>
    void GetClass()
    {
        if (GameObject.Find(bl_ClassManager.ClassManagerName) != null)
        {
            //Get info for class
            bl_RoomMenu.m_playerclass = bl_ClassManager.m_Class;

            m_AssaultClass.primary = bl_ClassManager.PrimaryAssault;
            m_AssaultClass.secondary = bl_ClassManager.SecundaryAssault;
            m_AssaultClass.Knife = bl_ClassManager.KnifeAssault;
            m_AssaultClass.Special = bl_ClassManager.GrenadeAssault;

            m_EngineerClass.primary = bl_ClassManager.PrimaryEnginner;
            m_EngineerClass.secondary = bl_ClassManager.SecundaryEnginner;
            m_EngineerClass.Knife = bl_ClassManager.KnifeEnginner;
            m_EngineerClass.Special = bl_ClassManager.GrenadeEnginner;

            m_SupportClass.primary = bl_ClassManager.PrimarySupport;
            m_SupportClass.secondary = bl_ClassManager.SecundarySupport;
            m_SupportClass.Knife = bl_ClassManager.KnifeSupport;
            m_SupportClass.Special = bl_ClassManager.GrenadeSupport;

            m_ReconClass.primary = bl_ClassManager.PrimaryRecon;
            m_ReconClass.secondary = bl_ClassManager.SecundaryRecon;
            m_ReconClass.Knife = bl_ClassManager.KnifeRecon;
            m_ReconClass.Special = bl_ClassManager.GrenadeRecon;

            switch (bl_RoomMenu.m_playerclass)
            {
                case PlayerClass.Assault:
                    PlayerEquip[0] = AllGuns[m_AssaultClass.primary];
                    PlayerEquip[1] = AllGuns[m_AssaultClass.secondary];
                    PlayerEquip[2] = AllGuns[m_AssaultClass.Special];
                    PlayerEquip[3] = AllGuns[m_AssaultClass.Knife];
                    break;
                case PlayerClass.Recon:
                    PlayerEquip[0] = AllGuns[m_ReconClass.primary];
                    PlayerEquip[1] = AllGuns[m_ReconClass.secondary];
                    PlayerEquip[2] = AllGuns[m_ReconClass.Special];
                    PlayerEquip[3] = AllGuns[m_ReconClass.Knife];
                    break;
                case PlayerClass.Engineer:
                    PlayerEquip[0] = AllGuns[m_EngineerClass.primary];
                    PlayerEquip[1] = AllGuns[m_EngineerClass.secondary];
                    PlayerEquip[2] = AllGuns[m_EngineerClass.Special];
                    PlayerEquip[3] = AllGuns[m_EngineerClass.Knife];
                    break;
                case PlayerClass.Support:
                    PlayerEquip[0] = AllGuns[m_SupportClass.primary];
                    PlayerEquip[1] = AllGuns[m_SupportClass.secondary];
                    PlayerEquip[2] = AllGuns[m_SupportClass.Special];
                    PlayerEquip[3] = AllGuns[m_SupportClass.Knife];
                    break;
            }
        }
        else
        {
            Debug.Log("Cant found class manager");
            //when player instance select player class select in bl_RoomMenu
            //when player instance select player class select in bl_RoomMenu
            switch (bl_RoomMenu.m_playerclass)
            {
                case PlayerClass.Assault:
                    PlayerEquip[0] = AllGuns[m_AssaultClass.primary];
                    PlayerEquip[1] = AllGuns[m_AssaultClass.secondary];
                    PlayerEquip[2] = AllGuns[m_AssaultClass.Special];
                    PlayerEquip[3] = AllGuns[m_AssaultClass.Knife];
                    break;
                case PlayerClass.Recon:
                    PlayerEquip[0] = AllGuns[m_ReconClass.primary];
                    PlayerEquip[1] = AllGuns[m_ReconClass.secondary];
                    PlayerEquip[2] = AllGuns[m_ReconClass.Special];
                    PlayerEquip[3] = AllGuns[m_ReconClass.Knife];
                    break;
                case PlayerClass.Engineer:
                    PlayerEquip[0] = AllGuns[m_EngineerClass.primary];
                    PlayerEquip[1] = AllGuns[m_EngineerClass.secondary];
                    PlayerEquip[2] = AllGuns[m_EngineerClass.Special];
                    PlayerEquip[3] = AllGuns[m_EngineerClass.Knife];
                    break;
                case PlayerClass.Support:
                    PlayerEquip[0] = AllGuns[m_SupportClass.primary];
                    PlayerEquip[1] = AllGuns[m_SupportClass.secondary];
                    PlayerEquip[2] = AllGuns[m_SupportClass.Special];
                    PlayerEquip[3] = AllGuns[m_SupportClass.Knife];
                    break;
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    void OnEnable()
    {
        bl_EventHandler.OnPickUpGun += this.PickUpGun;
    }
    /// <summary>
    /// 
    /// </summary>
    void OnDisable()
    {
        bl_EventHandler.OnPickUpGun += this.PickUpGun;
    }

    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && CanSwich && m_Current != 0)
        {

            StartCoroutine(ChangeGun(PlayerEquip[m_Current].gameObject, PlayerEquip[0].gameObject));
            m_Current = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && CanSwich && m_Current != 1)
        {

            StartCoroutine(ChangeGun((PlayerEquip[m_Current].gameObject), PlayerEquip[1].gameObject));
            m_Current = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && CanSwich && m_Current != 2)
        {
            StartCoroutine(ChangeGun((PlayerEquip[m_Current].gameObject), PlayerEquip[2].gameObject));
            m_Current = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && CanSwich && m_Current != 3)
        {
            StartCoroutine(ChangeGun((PlayerEquip[m_Current].gameObject), PlayerEquip[3].gameObject));
            m_Current = 3;
        }
        //cahnge gun with Scroll mouse
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            StartCoroutine(ChangeGun((PlayerEquip[m_Current].gameObject), PlayerEquip[(this.m_Current + 1) % this.PlayerEquip.Count].gameObject));
            m_Current = (this.m_Current + 1) % this.PlayerEquip.Count;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (this.m_Current != 0)
            {
                StartCoroutine(ChangeGun((PlayerEquip[m_Current].gameObject), PlayerEquip[(this.m_Current - 1) % this.PlayerEquip.Count].gameObject));
                this.m_Current = (this.m_Current - 1) % this.PlayerEquip.Count;
            }
            else
            {
                StartCoroutine(ChangeGun((PlayerEquip[m_Current].gameObject), PlayerEquip[this.PlayerEquip.Count - 1].gameObject));
                this.m_Current = this.PlayerEquip.Count - 1;
            }

        }
        CurrentGun = PlayerEquip[m_Current];
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="t_weapon"></param>
    void TakeWeapon(GameObject t_weapon)
    {
        t_weapon.SetActive(true);
        CanSwich = true;

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    public void PickUpGun(bl_OnPickUpInfo e)
    {
        //If not alredy equip
        if (!PlayerEquip.Contains(AllGuns[e.ID]))
        {
            int c = 0;

            for (int i = 0; i < AllGuns.Count; i++)
            {
                if (AllGuns[i] == PlayerEquip[m_Current])
                {
                    c = i;
                }
            }
            //Get Info
            int[] info = new int[2];
            info[0] = PlayerEquip[m_Current].numberOfClips;
            info[1] = PlayerEquip[m_Current].bulletsLeft;
            PlayerEquip[m_Current] = AllGuns[e.ID];
            //Send Info
            AllGuns[e.ID].numberOfClips = e.Clips;
            AllGuns[e.ID].bulletsLeft = e.Bullets;
            StartCoroutine(PickUpGun((PlayerEquip[m_Current].gameObject), AllGuns[e.ID].gameObject, c, info));
        }
        else
        {
            foreach (bl_Gun g in PlayerEquip)
            {
                if (g == AllGuns[e.ID])
                {
                    bl_EventHandler.OnAmmo(3);//Add 3 clips
                }
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bl_Gun GetCurrentWeapon()
    {
        if (CurrentGun == null)
        {
            return PlayerEquip[m_Current];
        }
        else
        {
            return CurrentGun;
        }
    }
    /// <summary>
    /// Corrutine to Change of Gun
    /// </summary>
    /// <param name="t_current"></param>
    /// <param name="t_next"></param>
    /// <returns></returns>
    public IEnumerator ChangeGun(GameObject t_current, GameObject t_next)
    {
        CanSwich = false;
        if (m_HeatAnimator != null)
        {
            m_HeatAnimator.SetBool("Swicht", true);
        }
        t_current.GetComponent<bl_Gun>().DisableWeapon();
        yield return new WaitForSeconds(SwichTime);
        foreach (bl_Gun guns in AllGuns)
        {
            if (guns.gameObject.activeSelf == true)
            {
                guns.gameObject.SetActive(false);
            }
        }
        TakeWeapon(t_next);
        if (m_HeatAnimator != null)
        {
            m_HeatAnimator.SetBool("Swicht", false);
        }
    }
    /// <summary>
    /// Corrutine to Change of Gun
    /// </summary>
    /// <param name="t_current"></param>
    /// <param name="t_next"></param>
    /// <returns></returns>
    public IEnumerator PickUpGun(GameObject t_current, GameObject t_next, int id, int[] info)
    {
        CanSwich = false;
        if (m_HeatAnimator != null)
        {
            m_HeatAnimator.SetBool("Swicht", true);
        }
        t_current.GetComponent<bl_Gun>().DisableWeapon();
        yield return new WaitForSeconds(PickUpTime);
        foreach (bl_Gun guns in AllGuns)
        {
            if (guns.gameObject.activeSelf == true)
            {
                guns.gameObject.SetActive(false);
            }
        }
        TakeWeapon(t_next);
        if (m_HeatAnimator != null)
        {
            m_HeatAnimator.SetBool("Swicht", false);
        }
        PUM.TrownGun(id, TrowPoint.position, info);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="m_state"></param>
    public void heatReloadAnim(int m_state)
    {
        if (m_HeatAnimator == null)
            return;

        switch (m_state)
        {
            case 0:
                m_HeatAnimator.SetInteger("Reload", 0);
                break;
            case 1:
                m_HeatAnimator.SetInteger("Reload", 1);
                break;
            case 2:
                m_HeatAnimator.SetInteger("Reload", 2);
                break;
        }
    }


    [System.Serializable]
    public class AssaultClass
    {
        //ID = the number of Gun in the list AllGuns
        /// <summary>
        /// the ID of the first gun Equipped
        /// </summary>
        public int primary = 0;
        /// <summary>
        /// the ID of the secondary Gun Equipped
        /// </summary>
        public int secondary = 1;
        /// <summary>
        /// the ID the a special weapon
        /// </summary>
        public int Special = 2;
        /// <summary>
        /// 
        /// </summary>
        public int Knife = 3;
    }
    public AssaultClass m_AssaultClass;

    [System.Serializable]
    public class EngineerClass
    {
        //ID = the number of Gun in the list AllGuns
        /// <summary>
        /// the ID of the first gun Equipped
        /// </summary>
        public int primary = 0;
        /// <summary>
        /// the ID of the secondary Gun Equipped
        /// </summary>
        public int secondary = 1;
        /// <summary>
        /// the ID the a special weapon
        /// </summary>
        public int Special = 2;
        /// <summary>
        /// 
        /// </summary>
        public int Knife = 3;
    }
    public EngineerClass m_EngineerClass;
    //
    [System.Serializable]
    public class ReconClass
    {
        //ID = the number of Gun in the list AllGuns
        /// <summary>
        /// the ID of the first gun Equipped
        /// </summary>
        public int primary = 0;
        /// <summary>
        /// the ID of the secondary Gun Equipped
        /// </summary>
        public int secondary = 1;
        /// <summary>
        /// the ID the a special weapon
        /// </summary>
        public int Special = 2;
        /// <summary>
        /// 
        /// </summary>
        public int Knife = 3;
    }
    public ReconClass m_ReconClass;
    //
    [System.Serializable]
    public class SupportClass
    {
        //ID = the number of Gun in the list AllGuns
        /// <summary>
        /// the ID of the first gun Equipped
        /// </summary>
        public int primary = 0;
        /// <summary>
        /// the ID of the secondary Gun Equipped
        /// </summary>
        public int secondary = 1;
        /// <summary>
        /// the ID the a special weapon
        /// </summary>
        public int Special = 2;
        /// <summary>
        /// 
        /// </summary>
        public int Knife = 3;
    }
    public SupportClass m_SupportClass;
}
