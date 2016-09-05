using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class bl_LoginManager : MonoBehaviour {

    //Call all script when Login
    public delegate void LoginEvent(string name,int kills,int deaths,int score);
    public static LoginEvent OnLogin;

    public GameObject PlayerInfo;
    public Animation LoginAnim;
    public Animation RegisterAnim;
    public Animation InfoAnim;
    [Space(5)]
    public Text Description = null;
    static Text mDescrip = null;
    public Image BlackScreen = null;

    public const string SavedUser = "UserName";
    private Color alpha = new Color(0, 0, 0, 0);
    protected bool InLogin = true;
    protected bool m_ShowInfo = false;
    /// <summary>
    /// 
    /// </summary>
    void Awake()
    {
        mDescrip = Description;
        OnLogin += onLogin;
        StartCoroutine(FadeOut());
        if (GameObject.Find("PlayerInfo") == null)
        {
            GameObject p = Instantiate(PlayerInfo, Vector3.zero, Quaternion.identity) as GameObject;
            p.name = p.name.Replace("(Clone)", "");
        }
    }
    /// <summary>
    /// 
    /// </summary>
    void OnDisable()
    {
        OnLogin -= onLogin;
    }
    /// <summary>
    /// 
    /// </summary>
    public void ShowLogin()
    {
        if (!InLogin)
        {
            InLogin = true;
            LoginAnim.Play("Login_Show");
            RegisterAnim.Play("Register_Hide");
            UpdateDescription("");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void ShowRegister()
    {
        if (InLogin)
        {
            InLogin = false;
            LoginAnim.Play("Login_Hide");
            RegisterAnim.Play("Register_Show");
            UpdateDescription("");
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public void ShowInfo()
    {
        m_ShowInfo = !m_ShowInfo;
        if (m_ShowInfo)
        {
            InfoAnim["Info_Show"].time = 0;
            InfoAnim["Info_Show"].speed = 1.0f;
            InfoAnim.Play("Info_Show");
        }
        else
        {
            InfoAnim["Info_Show"].time = InfoAnim["Info_Show"].length;
            InfoAnim["Info_Show"].speed = -1.0f;
            InfoAnim.Play("Info_Show");
        }
    }
    /// <summary>
    /// Update Text description UI
    /// </summary>
    /// <param name="t"></param>
    public static void UpdateDescription(string t)
    {
        mDescrip.text = t;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="kills"></param>
    /// <param name="deaths"></param>
    /// <param name="score"></param>
    public static void OnLoginEvent(string name, int kills, int deaths, int score)
    {
        if (OnLogin != null)
            OnLogin(name,kills,deaths,score);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="n"></param>
    /// <param name="k"></param>
    /// <param name="d"></param>
    /// <param name="s"></param>
    void onLogin(string n,int k,int d,int s)
    {
        BlackScreen.gameObject.SetActive(true);
        StartCoroutine(FadeIn());
    }

    /// <summary>
    /// Effect of Fade In
    /// </summary>
    /// <returns></returns>
    IEnumerator FadeIn()
    {
        alpha = BlackScreen.color;

        while (alpha.a < 1.0f)
        {
            alpha.a += Time.deltaTime ;
            BlackScreen.color = alpha;
            yield return null;
        }
    }
    /// <summary>
    /// Effect of Effect Fade Out
    /// </summary>
    /// <returns></returns>
    IEnumerator FadeOut()
    {
        alpha.a = 1f;
        while (alpha.a > 0.0f)
        {
            alpha.a -= Time.deltaTime;
            BlackScreen.color = alpha;
            yield return null;
        }
        BlackScreen.gameObject.SetActive(false);
    }
}