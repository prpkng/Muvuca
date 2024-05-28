using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Muvuca.Core
{
    public class InputManager : MonoBehaviour
    {

        private PlayerInput playerInput;

        public void ForceJump()
        {
            JumpPressed?.Invoke();
        }
        
        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Confined;
            JumpPressed = null;
            AttackPressed = null;
            ResetPressed = null;
            playerInput = GetComponent<PlayerInput>();
            playerInput.onActionTriggered += OnActionTriggered;
            IgnoringMouse = false;
        }

        public static event Action JumpPressed;
        public static event Action AttackPressed;
        public static event Action ResetPressed;

        private static Vector2 TouchPos = Vector2.zero;

        public static bool IgnoringMouse;

        private void OnActionTriggered(InputAction.CallbackContext obj)
        {
            if (IgnoringMouse) 
                return;
            
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
                    ResetPressed?.Invoke();
                    break;
                case "TouchPos":
                    if (!GameManager.isMobilePlatform) return;
                    TouchPos = obj.ReadValue<Vector2>();
                    var size = Screen.width;
                    var left = TouchPos.x < size / 2;
                    if (left) // LEFT CLICK
                        AttackPressed?.Invoke();
                    else                       // RIGHT CLICK
                        JumpPressed?.Invoke();
                    break;

            }
        }
    }
}