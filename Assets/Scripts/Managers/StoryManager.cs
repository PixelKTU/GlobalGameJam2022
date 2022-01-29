using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    [SerializeField] Transform RespawnPosition;

    private void Start()
    {
        WakeUp();
    }

    [ContextMenu("Wake up")]
    public void WakeUp()
    {
        GameState.Instance.MentalState.WakeUp();
        GameState.Instance.player.transform.position = RespawnPosition.position;
        RespawnPosition.gameObject.SetActive(true);
        DOVirtual.DelayedCall(RespawnPosition.GetComponentInChildren<Animation>().clip.length, () =>
        {
            GameState.Instance.player.transform.position = RespawnPosition.position;
            RespawnPosition.gameObject.SetActive(false);
        });
    }
}
