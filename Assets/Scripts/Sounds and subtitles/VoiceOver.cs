using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Audio Object", menuName = "Assets/New Audio Object")]
public class VoiceOver : ScriptableObject
{
    //public class Subtitle
    //{
    //    public string text;
    //    public float timeStamp;
    //}

    public AudioClip clip;

    public List<Subtitle> subtitles;
}
