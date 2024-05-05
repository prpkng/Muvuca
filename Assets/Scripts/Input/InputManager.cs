using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerInput.onActionTriggered += OnActionTriggered;
    }

    public static Action JumpPressed;
    public static Action AttackPressed;

    private void OnActionTriggered(InputAction.CallbackContext obj)
    {
        if (!obj.started) return;
        switch (obj.action.name)
        {
            case "Jump":
                JumpPressed?.Invoke();
                break;
            case "Attack":
                AttackPressed?.Invoke();
                break;
            case "Reset":
                LevelManager.Reset();
                break;
        }
    }
}
