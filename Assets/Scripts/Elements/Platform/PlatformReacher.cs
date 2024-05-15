using Muvuca.Input;
using Muvuca.Player;
using Muvuca.Systems;
using UnityEngine;

namespace Muvuca.Elements.Platform
{
    public class PlatformReacher : MonoBehaviour
    {
        private HitboxChecker distanceChecker;

        private void Entered()
        {
            PlayerController.Instance.enteredPlatform?.Invoke(transform);
        }

        private void OnEnable()
        {
            distanceChecker = GetComponent<HitboxChecker>();
            distanceChecker.entered += Entered;
        }

        private void OnDisable()
        {
            distanceChecker.entered -= Entered;
        }
    }
}