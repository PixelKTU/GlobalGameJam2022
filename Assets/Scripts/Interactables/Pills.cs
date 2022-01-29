using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pills : Interactable
{
    [SerializeField] float mentalStateRestorationAmount = 0.3f;

    protected override void OnInteracted()
    {
        base.OnInteracted();
        GameState.Instance.MentalState.Medicate(mentalStateRestorationAmount);
    }
}
