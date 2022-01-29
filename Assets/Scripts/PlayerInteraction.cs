using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] new Camera camera;
    [SerializeField] LayerMask interactionMask;
    [SerializeField] float interactionDistance = 5;

    private Interactable current;

    private void FixedUpdate()
    {
        RaycastHit hit;
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, interactionMask))
        {
            if(hit.collider.TryGetComponentInParent(out Interactable interactable))
            {
                if(hit.distance <= interactionDistance)
                {
                    // Interable within interaction range
                    Debug.DrawRay(camera.transform.position, camera.transform.forward * hit.distance, Color.green);
                    Select(interactable);
                }
                else
                {
                    // Interactable within look range
                    Debug.DrawRay(camera.transform.position, camera.transform.forward * hit.distance, Color.yellow);
                    Deselect();
                }
            }
            else
            {
                // Hit but interactable not found
                Debug.DrawRay(camera.transform.position, camera.transform.forward * hit.distance, Color.white);
                Deselect();
            }
        }
        else
        {
            // Raycast did not hit anything
            Debug.DrawRay(camera.transform.position, camera.transform.forward * 1000, Color.white);
            Deselect();
        }
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(current)
            {
                current.Interact();
            }
        }
    }

    private void Deselect()
    {
        GameState.Instance.HUD.DisplayInteractable(null);

        if(current)
        {
            current.SetHover(false);
            current = null;
        }
    }
    private void Select(Interactable next)
    {
        if(current != next)
        {
            if (current) current.SetHover(false);
            current = next;
            current.SetHover(true);
            GameState.Instance.HUD.DisplayInteractable(current);
        }
    }
}
