using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    [SerializeField] Transform RespawnPosition;

    public int rememberedPeople = 0;

    private void Start()
    {
        WakeUp();
    }

    [ContextMenu("Wake up")]
    public void WakeUp()
    {
        GameState.Instance.player.ResetPose();
        GameState.Instance.player.active = false;
        GameState.Instance.MentalState.WakeUp();
        GameState.Instance.player.transform.position = RespawnPosition.position;
        GameState.Instance.player.transform.forward = RespawnPosition.right;
        RespawnPosition.gameObject.SetActive(true);
        DOVirtual.DelayedCall(RespawnPosition.GetComponentInChildren<Animation>().clip.length, () =>
        {
            RespawnPosition.gameObject.SetActive(false);
            GameState.Instance.player.active = true;
        });
    }

    public void RememberPerson()
    {
        rememberedPeople++;

        if(rememberedPeople >= 3)
        {
            // Game win condition, or you can now fight yourself
        }
    }
}
