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

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        _inputManager = InputManager._instance;

        cameraTransform = _mainCam.transform;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
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
    }
}
