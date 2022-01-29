using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager _instance;
    
    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = new PlayerInput();

        _instance = this;

        Cursor.visible = false;
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    public Vector2 GetPlayerMovement()
    {
        return _playerInput.PlayerControls.Movement.ReadValue<Vector2>();
    }

    public Vector2 GetMouseDelta()
    {
        return _playerInput.PlayerControls.Look.ReadValue<Vector2>();
    }

    public bool IsRunning()
    {
        return _playerInput.PlayerControls.Run.IsPressed();
    }
}
