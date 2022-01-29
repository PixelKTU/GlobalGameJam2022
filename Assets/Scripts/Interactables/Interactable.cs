using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField] string defaultAction = "";

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
        onInteracted.Invoke();
        OnInteracted();
    }

    public virtual string Name()
    {
        return gameObject.name;
    }
    public virtual string Action()
    {
        return defaultAction;
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
