using System;
using System.Linq;
using Muvuca.Core;
using Muvuca.Entities.Platform;
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
        [SerializeField] private bool returnToNearest;
        [SerializeField] private float playerReturnMinimumRange = 1;
        
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

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, playerReturnMinimumRange);
        }

        protected virtual void Damage()
        {
            PlayerController.Instance.DamagePlayer(1);
            if (!returnPlayer) return;
            PlayerController.Instance.machine.ChangeState("return");
            if (!returnToNearest || LaunchPlatform.availablePlatforms.Count == 0) return;
                //Find nearest platform in range
            var pos = transform.position;
            var playerPos = PlayerController.Instance.transform.position;
            var platform = LaunchPlatform.availablePlatforms
                .Where(p => Vector2.Distance(p.transform.position, transform.position) > playerReturnMinimumRange)
                .OrderBy(p => Vector2.Distance(p.transform.position, playerPos))
                .ElementAt(0);
            PlayerController.Instance.platform = platform.transform;

        }
        

        private void Update()
        {
            if (distanceChecker.IsInRange) return;
            col.offset = (transform.position - PlayerController.Instance.transform.position).normalized * offsetForce;
        }

    }
}