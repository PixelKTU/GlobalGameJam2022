using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ObjectState : MonoBehaviour
{
    [MinMaxSlider(0, 1)]
    public Vector2 existanceBounds;

    public void SetState(bool state)
    {
        if (!Application.isPlaying) gameObject.SetActive(state);
    }
}
