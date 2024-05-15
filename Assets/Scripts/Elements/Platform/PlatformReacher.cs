using Muvuca.Input;
using Muvuca.Player;
using Muvuca.Systems;
using UnityEngine;

namespace Muvuca.Elements.Platform
{
    public class PlatformReacher : MonoBehaviour
    {
        [SerializeField] private bool startEnabled = true;
        private HitboxChecker distanceChecker;

        private void Start()
        {
            distanceChecker = GetComponent<HitboxChecker>();
        }

        private void OnEnable()
        {
            InputManager.JumpPressed += JumpPressed;
        }

        private void OnDisable()
        {
            InputManager.JumpPressed -= JumpPressed;
        }

        private void JumpPressed()
        {
            if (!distanceChecker.IsInRange) return;
            PlayerController.Instance.enteredPlatform?.Invoke(transform);
        }
    }
}