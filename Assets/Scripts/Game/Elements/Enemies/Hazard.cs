using System.Linq;
using JetBrains.Annotations;
using Muvuca.Core;
using Muvuca.Game.Elements.Platform;
using Muvuca.Systems;
using UnityEngine;

namespace Muvuca.Game.Elements.Enemies
{
    public class Hazard : MonoBehaviour
    {
        
        private HitboxChecker distanceChecker;
        [SerializeField] private Collider2D col;
        [SerializeField] private float offsetForce;
        [SerializeField] private bool enableOffset;
        [SerializeField] private bool returnPlayer;
        [SerializeField] private bool returnToNearest;
        [SerializeField] private float playerReturnMinimumRange = 1;

        public bool hitOnInvulnerable = false;
        public bool returnOnInvulnerable = false;

        [SerializeField] private bool selfDestructOnEnter;
        
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
            if (!PlayerController.Instance.isInvulnerable || hitOnInvulnerable)
            {
                // Running this on the next frame to ensure the player will trigger the return even if returnOnInvulnerable is false
                Util.InvokeNextFrame(PlayerController.Instance.DamagePlayer, 1);
                if (selfDestructOnEnter) Destroy(gameObject);
            }
            
            if (!returnPlayer || (PlayerController.Instance.isInvulnerable && !returnOnInvulnerable)) return;
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
            if (enableOffset) col.offset = (transform.position - PlayerController.Instance.transform.position).normalized * offsetForce;
        }

    }
}