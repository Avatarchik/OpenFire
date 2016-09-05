//-------------------OnEnableAnimation.cs--------------------------
//Use This for Play a animation when the Go is Enabled
//
//
//------------------------Lovatto Studio---------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("UMenu/Utils/UMPlay Animation")]
public class UMPlayAnimation : MonoBehaviour {

    public OnType m_onType = OnType.OnEnable;
    [System.Serializable]
    public class t_Animations
    {
        public Animation m_Animation;
        public string StartAnimation = "Start";
        public float m_speed = 1.0f;

    }
    public List<t_Animations> m_animations = new List<t_Animations>();

    public Animation m_animation;
    public string OnHoverAnim = "OnButtonHover";
    public string OnHoverOutAnim = "OnButtonHoverOut";
    public string OnClickAnim = "OnButtonPress";
    public float m_speed = 1.0f;

    void OnEnable()
    {
        if (m_onType != OnType.OnEnable)
            return;

        for (int i = 0; i < m_animations.Count; i++)
        {
            if (m_animations[i].m_Animation != null)
            {
                m_animations[i].m_Animation[m_animations[i].StartAnimation].speed = m_animations[i].m_speed;
                m_animations[i].m_Animation.Play(m_animations[i].StartAnimation);
            }
        }
    }

    public void OnClick()
    {
        if (m_animation != null)
        {
            m_animation[OnClickAnim].speed = m_speed;
            m_animation.Play(OnClickAnim);
        }
    }

    public void OnMouseOver()
    {
        if (m_animation != null)
        {
            m_animation[OnHoverAnim].speed = m_speed;
            m_animation.Play(OnHoverAnim);
        }
    }

    public void OnMouseOut()
    {
        if (m_animation != null)
        {
            m_animation[OnHoverOutAnim].speed = m_speed;
            m_animation.Play(OnHoverOutAnim);
        }
    }
    public void PlayReverse(string m_anima)
    {
        if (m_animation != null)
        {
            m_animation[m_anima].speed = -1;
            m_animation[m_anima].time = m_animation[m_anima].length;
            m_animation.Play(m_anima);
        }
    }

    
}