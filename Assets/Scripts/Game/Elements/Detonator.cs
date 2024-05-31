using Cysharp.Threading.Tasks;
using DG.Tweening;
using FMODUnity;
using Muvuca.Core;
using Muvuca.Game.Elements.Enemies;
using Muvuca.Systems;
using Muvuca.UI.HUD;
using UnityEngine;

namespace Muvuca.Game.Elements
{
    public class Detonator : HitboxListener
    {
        protected override void OnEnable()
        {
            if (hitbox == null) hitbox = GetComponentInParent<HitboxChecker>();
            base.OnEnable();
        }
        public bool active = true;
        public void SetActive(bool b) => active = b;
        [SerializeField] private float shockwaveTravelSpeed;
        [SerializeField] private Transform detonatorTransform;

        [SerializeField] private bool destroySelf = true;
        [SerializeField] private GameObject shockwavePrefab;

        [Space]

        [SerializeField] private float shockwaveStartSize;
        [SerializeField] private float shockwaveDestSize;
        [SerializeField] private float shockwaveDuration;
        [SerializeField] private Ease shockwaveEase;


        [SerializeField] private Material shockwaveMaterial;

        protected override void Entered()
        {
            InputManager.AttackPressed += ActivateDetonator;
        }

        protected override void Exited()
        {
            InputManager.AttackPressed -= ActivateDetonator;
        }

        public StudioEventEmitter detonatorSfx;

        private void ActivateDetonator()
        {
            if (!active) return;
            detonatorSfx.Play();
            detonatorSfx.SetParameter("EQEffect", 1f);
            shockwaveMaterial.SetFloat("_Size", shockwaveStartSize);
            shockwaveMaterial.DOFloat(shockwaveDestSize, "_Size", shockwaveDuration).SetEase(shockwaveEase);
            Exited();
            ArrowIndicator.Target = null;
            if (destroySelf) Destroy(gameObject);
            else
            {
                LevelManager.onLevelReset += () => gameObject.SetActive(true);
                gameObject.SetActive(false);
            }

            var shockwave = Instantiate(shockwavePrefab);
            shockwave.transform.position = transform.position;
        }
    }
}