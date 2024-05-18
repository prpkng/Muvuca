using System;
using System.Linq;
using Muvuca.Core;
using Muvuca.Entities.Platform;
using Muvuca.Systems;
using UnityEngine;
using UnityEngine.Events;

namespace Muvuca.Entities
{
    public class BossController : MonoBehaviour
    {
        public static BossController CurrentInstance;

        public GameObject detonatorGameObject;
        
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
                .OrderBy(p => Vector2.Distance(p.transform.position, PlayerController.Instance.transform.position));
            Instantiate(detonatorGameObject, platforms.ElementAt(0).transform);
        }
        
        [SerializeField] private HealthSystem bossHealth;
        [SerializeField] private LaunchPlatform[] possibleDetonatorPlatforms;
    }
}