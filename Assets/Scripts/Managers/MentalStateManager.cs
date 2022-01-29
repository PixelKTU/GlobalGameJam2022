using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class MentalStateManager : MonoBehaviour
{
    [ShowInInspector, ProgressBar(0, 1, Height = 30)]
    public float State; // Interval between [0-1], 0 is the worst 1 is the best

    [Tooltip("The percentage of mental state that is deteriorated in one second")]
    [SerializeField] float mentalStateDeteriorationRate = 0.5f;

    private void Start()
    {
        State = 1;
    }

    private void Update()
    {
        State -= Time.deltaTime * mentalStateDeteriorationRate * 0.01f;
        State = Mathf.Max(0, State);
    }

    public void Medicate(float amount)
    {
        State += amount;
        State = Mathf.Min(State, 1);
    }
}
