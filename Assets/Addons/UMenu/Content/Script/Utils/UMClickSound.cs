using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[AddComponentMenu("UMenu/Utils/UMClick Sound")]
public class UMClickSound : MonoBehaviour {

    public AudioClip audioClip;
    public OnType m_onType = OnType.OnClick;

    [Range(0f, 1f)]
    public float volume = 1f;
    [Range(0f, 2f)]
    public float pitch = 1.0f;

   public void OnClick()
    {
        if (canPlay && m_onType == OnType.OnClick)
            PlayAudioClip(audioClip, volume);
       
    }

    public void Play()
    {
        PlayAudioClip(audioClip, volume);
    }

    bool canPlay
    {
        get
        {
            if (!enabled)
                return false;

            Button btn = GetComponent<Button>();
            return (btn == null || btn.enabled);
        }
    }

    AudioSource PlayAudioClip(AudioClip clip, float volume)
    {
        GameObject go = new GameObject("One shot audio");
        if (Camera.main != null)
        {
            go.transform.position = Camera.main.transform.position;
        }
        else
        {
            go.transform.position = Camera.current.transform.position;
        }
        AudioSource source = go.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = volume;
        source.pitch = pitch;
        source.Play();
        Destroy(go, clip.length);
        return source;
    }
}