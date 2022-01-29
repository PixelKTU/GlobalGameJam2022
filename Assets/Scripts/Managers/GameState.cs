using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteAlways]
public class GameState : MonoBehaviour
{
    public StoryManager Story;
    public MentalStateManager MentalState;
    public HUD HUD;

    public PlayerController player;

    public static GameState Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        Objects = FindObjectsOfType<ObjectState>(true).ToList();
    }
    private void Update()
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            Instance = this;
            Objects = FindObjectsOfType<ObjectState>(true).ToList();
        }
#endif 


        foreach (var item in Objects)
        {
            item.gameObject.SetActive(item.existanceBounds.IsWithinBounds(GameState.Instance.MentalState.State));
        }
    }
    List<ObjectState> Objects = new List<ObjectState>();
}
