using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundObject : MonoBehaviour
{


    public void DestroyAfter(float time)
    {
        StartCoroutine(WaitForDestroy(time));
    }

    IEnumerator WaitForDestroy(float time)
    {
        yield return new WaitForSeconds(time);

        Destroy(gameObject);
    }
}
