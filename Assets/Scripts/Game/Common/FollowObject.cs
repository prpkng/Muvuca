using UnityEngine;

namespace Muvuca.Game.Common
{
    public class FollowObject : MonoBehaviour
    {
        public void SetTarget(Transform trans) => target = trans;
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
