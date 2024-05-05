using Muvuca.Systems;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
namespace Muvuca.Input
{
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

        private static Vector2 TouchPos = Vector2.zero;

        private void OnActionTriggered(InputAction.CallbackContext obj)
        {
            switch (obj.action.name)
            {
                case "Jump":
                    if (!obj.started) return;
                    JumpPressed?.Invoke();
                    break;
                case "Attack":
                    if (!obj.started) return;
                    AttackPressed?.Invoke();
                    break;
                case "Reset":
                    if (!obj.started) return;
                    LevelManager.Reset();
                    break;
                case "TouchPos":
                    if (!GameManager.isMobilePlatform) return;
                    TouchPos = obj.ReadValue<Vector2>();
                    var size = Screen.width;
                    var left = TouchPos.x < size / 2;
                    if (left) // LEFT CLICK
                        JumpPressed?.Invoke();
                    else                       // RIGHT CLICK
                        AttackPressed?.Invoke();
                    break;

            }
        }
    }
}