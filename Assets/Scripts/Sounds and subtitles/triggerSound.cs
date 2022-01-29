using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerSound : MonoBehaviour
{
    [SerializeField] VoiceOver clip;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            Vocals.instance.Say(clip);
    }
}
