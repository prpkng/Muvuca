using Muvuca.Systems;
using UnityEngine;
namespace Muvuca.Elements
{
    public class PlatformRotator : MonoBehaviour, IEnablable
    {
        public bool hasPlayer;
        public bool reverse;

        public float minAngle;
        public float maxAngle;
        private float counter;

        public float rotationSpeed;

        private Quaternion startRot;
        private Vector3 startPos;

        private void Awake()
        {
            startRot = transform.rotation;
            startPos = transform.position;
        }

        public void Disable()
        {
            if (gameObject.TryGetComponent(out DistanceChecker checker)) checker.Disable();
            hasPlayer = false;
            gameObject.SetActive(false);
            LevelManager.Instance.disabledElements.Add(this);
        }

        public void Enable()
        {
            gameObject.SetActive(true);
            gameObject.AddComponent<PlatformReacher>();

            counter = 0;
            transform.rotation = startRot;
            transform.position = startPos;
        }

        public void Update()
        {
            if (!hasPlayer) return;
            counter += (reverse ? -1 : 1) * rotationSpeed * Time.deltaTime;
            var wrappedAngle = Mathf.PingPong(counter, maxAngle - minAngle) + minAngle;
            transform.up = DirectionFromAngle(wrappedAngle);
        }

        private static Vector3 DirectionFromAngle(float degrees) {
            var radians = degrees * Mathf.Deg2Rad;
            return new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
        }

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + DirectionFromAngle(minAngle) * 10);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + DirectionFromAngle(maxAngle) * 10);
        }
    }
}