using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FamilyPicture : Interactable
{
    MeshRenderer mr;

    private void Awake()
    {
        mr = GetComponent<MeshRenderer>();
    }
    private void Update()
    {
        mr.material.SetFloat("censors", 3 - GameState.Instance.Story.rememberedPeople);
    }
}
