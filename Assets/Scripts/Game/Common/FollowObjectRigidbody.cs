using UnityEngine;

namespace Muvuca.Game.Common
{
    public class FollowObjectRigidbody : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        public void SetTarget(Transform trans) => target = trans;
        [SerializeField] private Transform target;
        [SerializeField] private float moveSpeed;
        public float moveSpeedMultiplier;
        [SerializeField] private float distanceSpeedAffect;

        private void FixedUpdate()
        {
            var from = rb.position;
            Vector2 dest = target.position;
            var velocity = moveSpeed * moveSpeedMultiplier;
            if (Mathf.Abs(distanceSpeedAffect) > Mathf.Epsilon)
                velocity *= Vector2.Distance(from, dest) * distanceSpeedAffect;
            Vector2 targetSpd = (dest - from).normalized * velocity;
            rb.velocity += (targetSpd - rb.velocity) * (1f - Mathf.Exp(-moveSpeed * Time.deltaTime));
        }
    }
}
