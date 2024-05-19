using Muvuca.Core;
using UnityEngine;

namespace Muvuca.Entities
{
    public class BossKnockback : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float force;
        
        
        public void Knockback()
        {
            rb.velocity = (rb.position - (Vector2)PlayerController.Instance.transform.position).normalized * force;
        }
    }
}