using Muvuca.Input;
using Muvuca.Player;
using Muvuca.Systems;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Scripting;

namespace Muvuca.Elements
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

        private void Update()
        {
            if (!distanceChecker.isRunning) return;
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * (distanceChecker.IsInRange ? 1.5f : 1f), Time.deltaTime * 8f);
        }
    }
}