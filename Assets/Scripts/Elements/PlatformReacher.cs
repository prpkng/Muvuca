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
        private DistanceChecker distanceChecker;

        private void Start()
        {
            distanceChecker = GetComponent<DistanceChecker>();
            if (!startEnabled)
                distanceChecker.Disable();
            distanceChecker.target = PlayerController.Instance.transform;
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
            distanceChecker.Disable();
            PlayerController.Instance.EnteredPlatform(transform);
        }

    }
}