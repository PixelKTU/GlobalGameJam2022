using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentSounds : MonoBehaviour
{
    [SerializeField] private AudioClip ambient;

    void Start()
    {
        AudioManager.Play(ambient, true);
    }

}
