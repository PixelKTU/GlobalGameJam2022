using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pills : Interactable
{
    [SerializeField] float mentalStateRestorationAmount = 0.3f;


    ParticleSystem ps;
    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    protected override void OnInteracted()
    {
        base.OnInteracted();
        GameState.Instance.MentalState.Medicate(mentalStateRestorationAmount);
    }


    private void Update()
    {
        ps.enableEmission = GameState.Instance.MentalState.State <= 0.3f;
    }
}
