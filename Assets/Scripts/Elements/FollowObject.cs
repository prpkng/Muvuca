using UnityEngine;

namespace Muvuca.Elements
{
    public class FollowObject : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float moveSpeed;
        public float moveSpeedMultiplier;
        [SerializeField] private float distanceSpeedAffect;

        private void Update()
        {
            var from = transform.position;
            var dest = target.position;
            var velocity = moveSpeed * moveSpeedMultiplier;
            if (Mathf.Abs(distanceSpeedAffect) > Mathf.Epsilon)
                velocity *= Vector2.Distance(from, dest) * distanceSpeedAffect;
            transform.position = Vector3.Lerp(from, dest, Time.deltaTime * velocity);
        }
    }
}
