using Muvuca.Systems;
using UnityEngine;
namespace Muvuca.Elements
{
    public class RotatingPlatform : FixedPlatform
    {

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

        public override void Enable()
        {
            base.Enable();

            counter = 0;
            transform.SetPositionAndRotation(startPos, startRot);
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