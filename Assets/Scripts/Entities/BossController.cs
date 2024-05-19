using System;
using System.Linq;
using Muvuca.Core;
using Muvuca.Entities.Platform;
using Muvuca.Systems;
using Muvuca.UI.HUD;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Muvuca.Entities
{
    public class BossController : MonoBehaviour
    {
        public static BossController CurrentInstance;

        public GameObject detonatorGameObject;

        public float nextDetonatorMinimumRange;
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, nextDetonatorMinimumRange);
        }
        private void Awake()
        {
            CurrentInstance = this;
        }

        [SerializeField] private string savingKey;
        public static bool IsFightingBoss;
        
        private bool isAwaken;
        [SerializeField] private UnityEvent<Transform> onAwake;

        private void Start()
        {
            bossHealth.onDamage.AddListener(OnBossAwake);
        }

        private void OnBossAwake(int _)
        {
            isAwaken = true;
            onAwake.Invoke(PlayerController.Instance.transform);
            bossHealth.onDamage.RemoveListener(OnBossAwake);
        }

        public void HitBoss()
        {
            bossHealth.DoDamage();
            if (possibleDetonatorPlatforms.Length == 0) return;
            var platforms = possibleDetonatorPlatforms
                .Where(p => Vector2.Distance(p.transform.position, transform.position) > nextDetonatorMinimumRange);
            ArrowIndicator.Target = Instantiate(detonatorGameObject, platforms.PickRandom().transform).transform.position;
            
        }
        
        [SerializeField] private HealthSystem bossHealth;
        [SerializeField] private LaunchPlatform[] possibleDetonatorPlatforms;
    }
}