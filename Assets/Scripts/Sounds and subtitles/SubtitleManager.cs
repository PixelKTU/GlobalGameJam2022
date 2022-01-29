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
        instance = this;
        subtitles.text = "";
    }

    public void SetSubtitle(string subtitle, float delay)
    {
        subtitles.text = subtitle;

        StartCoroutine(ClearAfterSeconds(delay));
    }

    public void ClearSubtitle()
    {
        subtitles.text = "";
    }

    private IEnumerator ClearAfterSeconds(float delay)
    {
        yield return new WaitForSeconds(delay);
        ClearSubtitle();
    }
}
