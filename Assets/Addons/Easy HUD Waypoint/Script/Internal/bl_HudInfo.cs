using UnityEngine;

[System.Serializable]
public class bl_HudInfo  {

    public Transform m_Target = null;
    public Texture2D m_Icon = null;
    public Color m_Color = new Color(1, 1, 1, 1);
    public Vector3 Offset;
    public string m_Text = null;
    public TypeHud m_TypeHud = TypeHud.Decreasing;
    public float m_MaxSize = 50f;
    public bool ShowDistance = true;
    public bool isPalpitin = true;
    [System.Serializable]
    public class m_Arrow
    {
        public bool ShowArrow = true;
        public Texture ArrowIcon = null;
        public Vector3 ArrowOffset = Vector3.zero;
        public float ArrowSize = 30;
    }
    [Space(5)]
    [Header("Arrow")]
    public m_Arrow Arrow;
    [HideInInspector]
    public bool tip = false;
}
