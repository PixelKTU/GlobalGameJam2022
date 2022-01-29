using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceOverManager : MonoBehaviour
{
    private AudioSource source;
    public static VoiceOverManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
    }

    public void Say(VoiceOver clip)
    {
        if (source.isPlaying)
            source.Stop();

        source.PlayOneShot(clip.clip);

        SubtitleManager.instance.SetSubtitle(clip.subtitle, clip.clip.length + 1);
    }
}
