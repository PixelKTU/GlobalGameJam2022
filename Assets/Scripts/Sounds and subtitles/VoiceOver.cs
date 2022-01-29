using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Audio Object", menuName = "Assets/New Audio Object")]
public class VoiceOver : ScriptableObject
{
    public AudioClip clip;
    public string subtitle;
}
