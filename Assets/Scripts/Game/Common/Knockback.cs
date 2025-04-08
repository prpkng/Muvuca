using Muvuca.Core;
using UnityEngine;

namespace Muvuca.Game.Common
{
    public class Knockback : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float force;
        
        public void TriggerKnockback()
        {
            rb.linearVelocity = (rb.position - (Vector2)PlayerController.Instance.transform.position).normalized * force;
        }
    }
}