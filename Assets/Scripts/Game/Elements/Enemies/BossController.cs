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
            follow.moveSpeedMultiplier += hitSpeedFactor;
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