using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class bl_HudManager : MonoBehaviour {

    /// <summary>
    /// All huds in scene
    /// </summary>
    public List<bl_HudInfo> Huds = new List<bl_HudInfo>();
    /// <summary>
    /// Player transform
    /// </summary>
    public Transform LocalPlayer = null;
    [Space(5)]
    public bool DecalMode = true;
    public float clampBorder = 12;
    [Space(5)]
    public GUIStyle TextStyle;
    [Space(5)]
    public bl_GameManager GManager;

    private static bl_HudManager _instance;

    //Get singleton
    public static bl_HudManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<bl_HudManager>();
            }
            return _instance;
        }
    }

    /// <summary>
    /// Draw a icon foreach in list
    /// </summary>
    void OnGUI()
    {
        if (bl_HudUtility.mCamera == null)
            return;
        if (LocalPlayer == null)
        {
            GetTarget();
            return;
        }
        //pass test :)

        //Create a Hud for each in list
        for (int i = 0; i < Huds.Count; i++)
        {           
            OnScreen(i);
            OffScreen(i);
        }
            
    }
    /// <summary>
    /// 
    /// </summary>
    void GetTarget()
    {
        if (GManager != null && GManager.OurPlayer != null)
        {
            LocalPlayer = GManager.OurPlayer.transform;

        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="i"></param>
    void OnScreen(int i)
    {
        //if transform destroy, then remove from list
        if (Huds[i].m_Target == null)
        {
            Huds.Remove(Huds[i]);
            return;
        }
        //Calculate Position of target
        Vector3 RelativePosition = Huds[i].m_Target.position + Huds[i].Offset;
        if ((Vector3.Dot(this.LocalPlayer.forward, RelativePosition - this.LocalPlayer.position) > 0f))
        {
            //Calculate the 2D position of the position where the icon should be drawn
            Vector3 point = bl_HudUtility.mCamera.WorldToViewportPoint(RelativePosition);

            //The viewportPoint coordinates are between 0 and 1, so we have to convert them into screen space here
            Vector2 drawPosition = new Vector2(point.x * Screen.width, Screen.height * (1 - point.y));

            if (!Huds[i].Arrow.ShowArrow)
            {
                //Clamp the position to the edge of the screen in case the icon would be drawn outside the screen
                drawPosition.x = Mathf.Clamp(drawPosition.x, clampBorder, Screen.width - clampBorder);
                drawPosition.y = Mathf.Clamp(drawPosition.y, clampBorder, Screen.height - clampBorder);
            }
            //Caculate distance from player to waypoint
            float Distance = Vector3.Distance(this.LocalPlayer.position, RelativePosition);
            //Cache distance
            float CompleteDistance = Distance;
            //Max Hud Increment 
            if (Distance > 50) // if more than "50" no increase more
            {
                Distance = 50;
            }
            float n;
            //Calculate depend of type 
            if (Huds[i].m_TypeHud == TypeHud.Decreasing)
            {
                n = (((50 + Distance) / (50 * 2f)) * 0.9f) + 0.1f;
            }
            else
            {
                n = (((50 - Distance) / (50 * 2f)) * 0.9f) + 0.1f;
            }
            //Calculate Size of Hud
            float sizeX = Huds[i].m_Icon.width * n;
            if (sizeX >= Huds[i].m_MaxSize)
            {
                sizeX = Huds[i].m_MaxSize;
            }
            float sizeY = Huds[i].m_Icon.height * n;
            if (sizeY >= Huds[i].m_MaxSize)
            {
                sizeY = Huds[i].m_MaxSize;
            }
            //palpiting effect
            if (Huds[i].isPalpitin)
            {
                if (Huds[i].m_Color.a <= 0)
                {
                    Huds[i].tip = false;
                }
                else if (Huds[i].m_Color.a >= 1)
                {
                    Huds[i].tip = true;
                }
                //Create a loop
                if (Huds[i].tip == false)
                {
                    Huds[i].m_Color.a += Time.deltaTime * 0.5f;
                }
                else
                {
                    Huds[i].m_Color.a -= Time.deltaTime * 0.5f;
                }
            }
            //Draw Huds
            GUI.color = Huds[i].m_Color;
            GUI.DrawTexture(new Rect(drawPosition.x - ((sizeX) / 2), drawPosition.y - ((sizeY) / 2), sizeX, sizeY), Huds[i].m_Icon);
            if (!Huds[i].ShowDistance)
            {
                GUI.Label(new Rect((drawPosition.x - Huds[i].m_Text.Length), (drawPosition.y - n) - 35f, 300, 50), Huds[i].m_Text, TextStyle);
            }
            else
            {
                GUI.Label(new Rect((drawPosition.x - Huds[i].m_Text.Length), (drawPosition.y - n) - 35f, 300, 50), Huds[i].m_Text + "\n <color=whitte>[" + CompleteDistance.ToString("000") + "m]</color>", TextStyle);
            }
        }
    }
    /// <summary>
    /// When the target if not in Player sight 
    /// </summary>
    /// <param name="i"></param>
    void OffScreen(int i)
    {
        if (Huds[i].Arrow.ArrowIcon != null)
            {
                //Get the relative position of arrow
                Vector3 ArrowPosition = Huds[i].m_Target.position + Huds[i].Arrow.ArrowOffset;
                Vector3 pointArrow = bl_HudUtility.mCamera.WorldToScreenPoint(ArrowPosition);
            
                pointArrow.x = pointArrow.x / bl_HudUtility.mCamera.pixelWidth;
                pointArrow.y = pointArrow.y / bl_HudUtility.mCamera.pixelHeight;

                Vector3 mForward = Huds[i].m_Target.position - bl_HudUtility.mCamera.transform.position;
                Vector3 mDir = bl_HudUtility.mCamera.transform.InverseTransformDirection(mForward);
                mDir = mDir.normalized / 5;
                pointArrow.x = 0.5f + mDir.x * 20f / bl_HudUtility.mCamera.aspect;
                pointArrow.y = 0.5f + mDir.y * 20f; 

                if (pointArrow.z < 0)
                {
                    pointArrow *= -1f;
                    pointArrow *= -1f;
                }
                //Arrow
                GUI.color = Huds[i].m_Color;

                    float Xpos = bl_HudUtility.mCamera.pixelWidth * pointArrow.x;
                    float Ypos = bl_HudUtility.mCamera.pixelHeight * (1f - pointArrow.y);

                    if (!bl_HudUtility.isOnScreen(bl_HudUtility.ScreenPosition(i, Huds[i].m_Target), i, Huds[i].m_Target))
                    {
                        float mRot = bl_HudUtility.GetRotation(bl_HudUtility.mCamera.pixelWidth / (2), bl_HudUtility.mCamera.pixelHeight / (2), Xpos, Ypos);
                        Vector2 mPivot = bl_HudUtility.GetPivot(Xpos, Ypos, Huds[i].Arrow.ArrowSize);
                       
                        Matrix4x4 matrix = GUI.matrix;
                        GUIUtility.RotateAroundPivot(mRot, mPivot);
                        GUI.DrawTexture(new Rect(mPivot.x - bl_HudUtility.HalfSize(Huds[i].Arrow.ArrowSize), mPivot.y - bl_HudUtility.HalfSize(Huds[i].Arrow.ArrowSize), Huds[i].Arrow.ArrowSize, Huds[i].Arrow.ArrowSize), Huds[i].Arrow.ArrowIcon);
                        GUIUtility.RotateAroundPivot(-mRot, mPivot);
                        GUI.matrix = matrix;
                        float df = 0;
                        if (!Huds[i].ShowDistance)
                        {
                            df = 25;
                            GUI.Label(new Rect(bl_HudUtility.FixedPositionX(mPivot.x, Huds[i].Arrow.ArrowSize, Huds[i].m_Text.Length), bl_HudUtility.FixedPositionY(mPivot.y, Huds[i].Arrow.ArrowSize), 300, 75), Huds[i].m_Text, TextStyle);
                        }
                        else
                        {
                            df = 50;
                            float Distance = Vector3.Distance(this.LocalPlayer.position, Huds[i].m_Target.position);
                            GUI.Label(new Rect(bl_HudUtility.FixedPositionX(mPivot.x, Huds[i].Arrow.ArrowSize, Huds[i].m_Text.Length), bl_HudUtility.FixedPositionY(mPivot.y, Huds[i].Arrow.ArrowSize), 300, 75), Huds[i].m_Text + "\n <color=whitte>[" + Distance.ToString("000") + "m]</color>", TextStyle);
                        }
                        GUI.DrawTexture(new Rect(bl_HudUtility.FixedPositionX(mPivot.x,25,0), bl_HudUtility.FixedPositionY( mPivot.y,df), 25, 25), Huds[i].m_Icon);
                    }
                    GUI.color = Color.white;
                }                   
    }

    //Add a new Huds from instance to the list
    public void CreateHud(bl_HudInfo info)
    {
        Huds.Add(info);
    }
}