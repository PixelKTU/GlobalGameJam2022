using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundtest : MonoBehaviour
{
    [SerializeField] AudioClip onCamera;
    [SerializeField] AudioClip onTransform;
    [SerializeField] AudioClip atPosition;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            AudioManager.Play(onCamera);
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            AudioManager.Play(transform, onTransform);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            AudioManager.Play(transform.position + new Vector3(0, 0, 10), atPosition);
        }
    }
}
