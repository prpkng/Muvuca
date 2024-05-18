using Muvuca.Core;
using Muvuca.Systems;
using UnityEngine;

namespace Muvuca.Entities
{
    public class Hazard : MonoBehaviour
    {
        
        private HitboxChecker distanceChecker;
        [SerializeField] private Collider2D col;
        [SerializeField] private float offsetForce;
        
        
        private void OnEnable()
        {
            distanceChecker = GetComponent<HitboxChecker>();
            distanceChecker.entered += Entered;
            distanceChecker.exited += Exited;
        }

        private void OnDisable()
        {
            distanceChecker.entered -= Entered;
            distanceChecker.exited -= Exited;
        }

        protected virtual void Entered()
        {
        }

        protected virtual void Exited()
        {
            PlayerController.Instance.DamagePlayer(1);
        }
        

        private void Update()
        {
            if (distanceChecker.IsInRange) return;
            col.offset = (transform.position - PlayerController.Instance.transform.position).normalized * offsetForce;
        }

    }
}