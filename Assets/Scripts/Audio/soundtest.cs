using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundtest : MonoBehaviour
{

    [SerializeField] int playIndex;
    [SerializeField] VoiceOver[] voiceOver;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            VoiceOverManager.instance.Say(voiceOver[playIndex]);
        }

    }
}
