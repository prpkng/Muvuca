using UnityEngine;

namespace Muvuca.Tools
{
    public class RythmPositioner : MonoBehaviour
    {
        [Range(0, 360)]
        public float directionDegrees = 0;
        public float BPM = 100;
        [Range(1, 4)]
        public float beatCount;
        public float movementSpeed = 10; // In seconds
        private static readonly Color dimWhite = Color.white - Color.black / 2;

        private void Awake()
        {
            Destroy(this);
        }

        [ExecuteInEditMode]
        public void Move()
        {
            var radians = Mathf.Deg2Rad * directionDegrees;
            var dir = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));

            float beatsPerSecond = BPM / 60f;
            float movementPerBeat = movementSpeed / beatsPerSecond;
            transform.position += movementPerBeat * beatCount * (Vector3)dir;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = dimWhite;
            var radians = Mathf.Deg2Rad * directionDegrees;
            var dir = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));

            float beatsPerSecond = BPM / 60f;
            float movementPerBeat = movementSpeed / beatsPerSecond;
            Gizmos.DrawLine(transform.position, transform.position + 4 * movementPerBeat * (Vector3)dir);
            for (int i = 1; i < 5; i++)
            {
                Gizmos.color = dimWhite;
                var pos = transform.position + i * movementPerBeat * (Vector3)dir;
                Vector3 perpDir = Vector2.Perpendicular(dir) / 2;

                Gizmos.DrawLine(pos - perpDir, pos + perpDir);
            }

            var _pos = transform.position + beatCount * movementPerBeat * (Vector3)dir;
            Gizmos.color = Color.green;
            Vector3 _perpDir = Vector2.Perpendicular(dir) / 2;

            Gizmos.DrawLine(_pos - _perpDir, _pos + _perpDir);
        }
    }
}