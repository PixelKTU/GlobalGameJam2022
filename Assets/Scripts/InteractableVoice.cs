using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class InteractableVoice : MonoBehaviour
{
    [SerializeField] bool randomOrder;
    [SerializeField] bool playOnce;

    [SerializeField] List<VoiceOver> VO;

    List<VoiceOver> Unplayed;

    private void Awake()
    {
        GetComponent<Interactable>().onInteracted.AddListener(Interact);
        Unplayed = new List<VoiceOver>(VO);
    }

    private void Interact()
    {
        if(Unplayed.Count <= 0)
        {
            if(playOnce)
            {
                return;
            }
            else
            {
                Unplayed = new List<VoiceOver>(VO);
            }
        }

        VoiceOver vo = null;
        if(randomOrder)
        {
            vo = Unplayed.PickRandom();
            Unplayed.Remove(vo);
        }
        else
        {
            vo = Unplayed[0];
            Unplayed.RemoveAt(0);
        }

        VoiceOverManager.instance.Say(vo);
    }
}
