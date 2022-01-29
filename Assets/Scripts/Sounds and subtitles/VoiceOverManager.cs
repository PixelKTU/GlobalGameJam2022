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

    public void Say(VoiceOver voiceOver)
    {
        if (source.isPlaying)
            source.Stop();

        //source.PlayOneShot(voiceOver.clip);
        AudioManager.Play(voiceOver.clip);

        //SubtitleManager.instance.SetSubtitle(clip.subtitle[0], clip.clip.length + 1);
        for(int i = 0; i < voiceOver.subtitles.Count; i++)
        {
            SubtitleManager.instance.SetSubtitle(voiceOver.subtitles[i].text, voiceOver.subtitles[i].delay);
            StartCoroutine(Delay(voiceOver.subtitles[i].delay));
        }
    }

    private IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
    }
}
