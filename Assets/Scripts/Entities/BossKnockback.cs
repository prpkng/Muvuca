using DG.Tweening;
using Muvuca.Core;
using UnityEngine;

namespace Muvuca.Entities
{
    public class BossKnockback : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float force;

        [SerializeField] private float hitScaleDifference;
        [SerializeField] private float hitScaleDuration;
        [SerializeField] private Ease hitScaleEase;
        
        public void Knockback()
        {
            rb.velocity = (rb.position - (Vector2)PlayerController.Instance.transform.position).normalized * force;
            transform.DOScale(transform.localScale - Vector3.one * hitScaleDifference, hitScaleDuration)
                .SetEase(hitScaleEase);
        }
    }
}