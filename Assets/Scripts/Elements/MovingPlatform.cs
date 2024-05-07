using Muvuca.Systems;
using UnityEngine;
namespace Muvuca.Elements
{
    public class MovingPlatform : FixedPlatform
    {
        [SerializeField] private AnimationCurve curve;
        [SerializeField] private Vector2 moveOffset;
        [SerializeField] private float moveSpeed;

        private float counter;
        private Vector3 startPos;
        private Vector3 destPos;

        private void Awake()
        {
            startPos = transform.position;
            destPos = startPos + (Vector3) moveOffset;
        }

        public override void Enable()
        {
            base.Enable();

            counter = 0;
            transform.position = startPos;
        }


        private void Update() {
            if (!hasPlayer) return;
            counter += (reverse ? -1 : 1) * moveSpeed/2 * Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, destPos, curve.Evaluate(counter % 1));
        }

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + (Vector3) moveOffset);
        }

    }
}