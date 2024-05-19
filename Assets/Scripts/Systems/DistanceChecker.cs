using System;
using Muvuca.Core;
using UnityEngine;

namespace Muvuca.Systems
{
    public class DistanceChecker : HitboxChecker
    {
        [SerializeField] private float distance;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, distance);
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {}

        protected override void OnTriggerExit2D(Collider2D other)
        {}

        private bool wasInRange;
        
        private void Update()
        {
            IsInRange = Vector2.Distance(transform.position, PlayerController.Instance.transform.position) <
                            distance;
            
            if (IsInRange && !wasInRange)
                entered?.Invoke();
            if (wasInRange && !IsInRange)
                exited?.Invoke();
            
            wasInRange = IsInRange;
        }
    }
}