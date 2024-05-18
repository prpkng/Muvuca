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
        [SerializeField] private bool returnPlayer;
        
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
            Damage();
        }

        protected virtual void Exited()
        {
            
        }

        protected virtual void Damage()
        {
            PlayerController.Instance.DamagePlayer(1);
            if (returnPlayer) PlayerController.Instance.machine.ChangeState("return");
        }
        

        private void Update()
        {
            if (distanceChecker.IsInRange) return;
            col.offset = (transform.position - PlayerController.Instance.transform.position).normalized * offsetForce;
        }

    }
}