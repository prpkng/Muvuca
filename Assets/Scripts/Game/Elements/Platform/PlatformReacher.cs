using Muvuca.Core;
using Muvuca.Systems;
using UnityEngine;

namespace Muvuca.Game.Elements.Platform
{
    public class PlatformReacher : MonoBehaviour
    {
        private HitboxChecker distanceChecker;

        [SerializeField] private float preRotateStartDistance;

        [SerializeField] private float preRotateSpeed;

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

        private void Update()
        {
            if (distanceChecker.IsInRange)
                return;
            if (Vector2.Distance(PlayerController.Instance.transform.position, transform.position) > preRotateStartDistance)
                return;
            transform.up = Vector3.Lerp(transform.up, (PlayerController.Instance.transform.position - transform.position).normalized, Time.deltaTime * preRotateSpeed);
        }
    }
}