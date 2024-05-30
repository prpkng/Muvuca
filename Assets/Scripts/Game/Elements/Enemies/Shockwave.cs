using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Muvuca.Game.Elements.Enemies
{
    public class Shockwave : MonoBehaviour
    {
        public CircleCollider2D col;
        public float growSpeedInSecs;
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, col.radius);
        }

        private void FixedUpdate()
        {
            col.radius += growSpeedInSecs * Time.fixedDeltaTime;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out ShockwaveTriggerable shockwaveTriggerable))
                shockwaveTriggerable.trigger.Invoke();
        }
    }
}