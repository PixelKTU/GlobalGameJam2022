using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableStates : MonoBehaviour
{
    [SerializeField] List<UnityEvent> StateEvents;
    [SerializeField] bool cyclical = true;

    private int currentState = 0;

    public void Interact()
    {
        StateEvents[currentState].Invoke();
        if(cyclical)
        {
            currentState = (currentState + 1) % StateEvents.Count;
        }
        else
        {
            currentState++;
            currentState = Mathf.Min(StateEvents.Count, currentState);
        }
    }
}
