using DG.Tweening;
using Muvuca.Core;
using Muvuca.Systems;
using UnityEngine;

namespace Muvuca.Entities
{
    public class Detonator : MonoBehaviour
    {
        [SerializeField] private Transform detonatorTransform;
        [SerializeField] private float shockwaveStartSize;
        [SerializeField] private float shockwaveDestSize;
        [SerializeField] private float shockwaveDuration;
        [SerializeField] private Ease shockwaveEase;
        private HitboxChecker checker;
        private void Awake() => checker = GetComponent<HitboxChecker>();

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

        private async void ActivateDetonator()
        {
            shockwaveMaterial.SetFloat("_Size", shockwaveStartSize);
            shockwaveMaterial.DOFloat(shockwaveDestSize, "_Size", shockwaveDuration).SetEase(shockwaveEase);
            Exited();
            Destroy(this);
            Destroy(detonatorTransform.gameObject);
        }
    }
}