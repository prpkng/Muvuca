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
    public class Detonator : MonoBehaviour
    {
        [SerializeField] private float shockwaveTravelSpeed;
        [SerializeField] private Transform detonatorTransform;

        [SerializeField] private bool destroySelf = true;
        [SerializeField] private GameObject shockwavePrefab;
        
        [Space]
        
        [SerializeField] private float shockwaveStartSize;
        [SerializeField] private float shockwaveDestSize;
        [SerializeField] private float shockwaveDuration;
        [SerializeField] private Ease shockwaveEase;
        private HitboxChecker checker;
        private void Awake() => checker = GetComponentInParent<HitboxChecker>();

        private void OnEnable()
        {
            checker.entered += Entered;
            checker.exited += Exited;
        }
        private void OnDisable()
        {
            checker.entered -= Entered;
            checker.exited -= Exited;
        }

        [SerializeField] private Material shockwaveMaterial;
        
        private void Entered()
        {
            InputManager.AttackPressed += ActivateDetonator;
        }

        private void Exited()
        {
            InputManager.AttackPressed -= ActivateDetonator;
        }

        public StudioEventEmitter detonatorSfx;
        
        private async void ActivateDetonator()
        {
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