using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SubtitleManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI subtitles = default;

    public static SubtitleManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        SetSubtitle("");
    }

    public void SetSubtitle(VoiceOver voiceOver)
    {

        for (int i = 0; i < voiceOver.subtitles.Count; i++)
        {
            StartCoroutine(ShowSubtitle(voiceOver.subtitles[i].text, voiceOver.subtitles[i].timestamp, voiceOver.subtitles[i].delay));
        }
    }

    public void SetSubtitle(string subtitle)
    {
        subtitles.text = subtitle;
    }

    private IEnumerator ShowSubtitle(string subtitle, float timestamp, float delay)
    {
        yield return new WaitForSeconds(timestamp);
        SetSubtitle(subtitle);
        StartCoroutine(ClearAfterSeconds(delay));
    }

    private IEnumerator ClearAfterSeconds(float delay)
    {
        yield return new WaitForSeconds(delay);
        SetSubtitle("");
    }

}
