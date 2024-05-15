using Muvuca.Effects;
using Muvuca.Input;
using Muvuca.Player;
using Muvuca.Systems;
using UnityEngine;

namespace Muvuca.Elements.Platform
{
    public class ReboundPlatform : MonoBehaviour
    {
        private HitboxChecker distanceChecker;
        [Range(0, 360)]
        public float newDirection;

        private void OnDrawGizmosSelected()
        {
            var angle = Mathf.Deg2Rad * newDirection;
            var dir = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle));
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + dir * 2);
        }

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
            var angle = Mathf.Deg2Rad * newDirection;
            var dir = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle));
            PlayerController.Instance.transform.position = transform.position;
            PlayerController.Instance.SetDirection(dir);

        }
        private void Update()
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * (distanceChecker.IsInRange ? 1.5f : 1f), Time.deltaTime * 8f);
        }
    }
}