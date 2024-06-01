using System;
using System.Linq;
using DG.Tweening;
using FMODUnity;
using Muvuca.Core;
using Muvuca.Game.Common;
using Muvuca.Game.Elements.Platform;
using Muvuca.Systems;
using Muvuca.UI.HUD;
using UnityEngine;
using UnityEngine.Events;

namespace Muvuca.Game.Elements.Enemies
{
    public class BossController : MonoBehaviour
    {

        [SerializeField] private Material shockwaveMaterial;
        public void PlayShockwave()
        {
            RuntimeManager.StudioSystem.setParameterByName("EQEffect", 1f);
            shockwaveMaterial.SetFloat("_Size", -.25f);
            shockwaveMaterial.DOFloat(2.25f, "_Size", 1f).SetEase(Ease.OutQuad);
        }
        public void Die()
        {
            Destroy(gameObject);
        }
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

        [SerializeField] private FollowObjectRigidbody follow;
        [SerializeField] private float hitSpeedFactor;

        [SerializeField] private string savingKey;
        public static bool IsFightingBoss;

        private bool isAwaken;
        [SerializeField] private UnityEvent<Transform> onAwake;

        public static event Action BossGotHit;

        private void Start()
        {
            health?.onDamage.AddListener(OnBossAwake);
        }

        private void OnBossAwake(int _)
        {
            isAwaken = true;
            onAwake.Invoke(PlayerController.Instance.transform);
            health.onDamage.RemoveListener(OnBossAwake);
        }

        private LaunchPlatform lastDetonator;

        public void HitBoss()
        {
            follow.moveSpeedMultiplier += hitSpeedFactor;
            health.DoDamage();
            BossGotHit?.Invoke();
            if (health.currentHp <= 0) return;
            if (possibleDetonatorPlatforms.Length == 0) return;
            var platforms = possibleDetonatorPlatforms
                .Where(p => Vector2.Distance(p.transform.position, transform.position) > nextDetonatorMinimumRange)
                .ToArray();

            var detonator = platforms.PickRandom();
            while (detonator == lastDetonator) detonator = platforms.PickRandom();
            lastDetonator = detonator;


            ArrowIndicator.Target = Instantiate(detonatorGameObject, lastDetonator.transform).transform.position;

        }

        public HealthSystem health;
        [SerializeField] private LaunchPlatform[] possibleDetonatorPlatforms;
    }
}