using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class HUD : MonoBehaviour
{
    [SerializeField] float fadeDuration = 0.5f;
    [SerializeField] float actionFadeDelay = 0.1f;

    [SerializeField] TextMeshProUGUI InteractableNameText;
    [SerializeField] TextMeshProUGUI InteractableActionText;

    public void DisplayInteractable(Interactable interactable)
    {
        if(interactable)
        {
            InteractableNameText.SetText(interactable.Name());
            InteractableActionText.SetText(interactable.Action());

            InteractableNameText.DOFade(1, fadeDuration);
            InteractableActionText.DOFade(1, fadeDuration).SetDelay(actionFadeDelay);
        }
        else
        {
            InteractableNameText.DOFade(0, fadeDuration);
            InteractableActionText.DOFade(0, fadeDuration);
        }
    }
}
