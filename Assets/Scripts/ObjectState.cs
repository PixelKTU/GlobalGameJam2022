using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ObjectState : MonoBehaviour
{
    [MinMaxSlider(0, 1)]
    public Vector2 existanceBounds;

    bool dirty;
    bool change;

    public void SetState(bool state)
    {
        if (!Application.isPlaying) gameObject.SetActive(state);
        else
        {
            if (state) Appear();
            else Dissapear();
        }
    }

    public void Appear()
    {

    }
    public void Dissapear()
    {

    }
}
