using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    
    [SerializeField]
    private float walkingSpeed = 2.0f;

    [SerializeField] private float runningSpeed = 8.0f;

    [SerializeField]
    private float gravityValue = -9.81f;

    private InputManager _inputManager;

    private Transform cameraTransform;

    [SerializeField] private Camera _mainCam;
    [SerializeField] LayerMask interactionMask;
    [SerializeField] float interactionDistance = 5;
    [SerializeField] Transform eyesTr;

    private Interactable current;

    public bool active = false;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        _inputManager = InputManager._instance;

        cameraTransform = _mainCam.transform;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (!active) return;

        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 movement = _inputManager.GetPlayerMovement();
        Vector3 move = new Vector3(movement.x, 0f, movement.y);
        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        move.y = 0f;
            
        controller.Move(move.normalized * Time.deltaTime * ((_inputManager.IsRunning()) ? runningSpeed : walkingSpeed));

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if (_inputManager.GetInteractionDown())
        {
            if (current)
            {
                current.Interact();
            }
        }
    }


    private void FixedUpdate()
    {
        if (!active) return;

        RaycastHit hit;
        Ray ray = new Ray(_mainCam.transform.position, _mainCam.transform.forward);
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, interactionMask))
        {
            if (hit.collider.TryGetComponentInParent(out Interactable interactable))
            {
                if (hit.distance <= interactionDistance)
                {
                    // Interable within interaction range
                    Debug.DrawRay(_mainCam.transform.position, _mainCam.transform.forward * hit.distance, Color.green);
                    Select(interactable);
                }
                else
                {
                    // Interactable within look range
                    Debug.DrawRay(_mainCam.transform.position, _mainCam.transform.forward * hit.distance, Color.yellow);
                    Deselect();
                }
            }
            else
            {
                // Hit but interactable not found
                Debug.DrawRay(_mainCam.transform.position, _mainCam.transform.forward * hit.distance, Color.white);
                Deselect();
            }
        }
        else
        {
            // Raycast did not hit anything
            Debug.DrawRay(_mainCam.transform.position, _mainCam.transform.forward * 1000, Color.white);
            Deselect();
        }
    }

    private void Deselect()
    {
        GameState.Instance.HUD.DisplayInteractable(null);

        if (current)
        {
            current.SetHover(false);
            current = null;
        }
    }
    private void Select(Interactable next)
    {
        if (current != next)
        {
            if (current) current.SetHover(false);
            current = next;
            current.SetHover(true);
            GameState.Instance.HUD.DisplayInteractable(current);
        }
    }

    public void ResetPose()
    {
        eyesTr.rotation = Quaternion.identity;
    }
}
