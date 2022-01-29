using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent onHovered;
    public UnityEvent onUnhovered;
    public UnityEvent onInteracted;

    public bool Hovered { get; private set; }

    public void SetHover(bool hover)
    {
        if(hover)
        {
            OnHovered();
            onHovered.Invoke();
        }
        else
        {
            OnUnhovered();
            onUnhovered.Invoke();
        }
    }
    public void Interact()
    {
        Debug.Log($"Interacted with {gameObject}");
        onInteracted.Invoke();
        OnInteracted();
    }

    protected virtual void OnHovered()
    {

    }
    protected virtual void OnUnhovered()
    {

    }
    protected virtual void OnInteracted()
    {

    }
}
