using System;
using DG.Tweening;
using Muvuca.Input;
using Muvuca.Systems;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Muvuca.Elements
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

        private AsyncOperationHandle<Material>? shockwaveMaterial;
        
        private void Entered()
        {
            InputManager.AttackPressed += ActivateDetonator;
            shockwaveMaterial = Addressables.LoadAssetAsync<Material>("Assets/Art/Materials/Shockwave");
        }

        private void Exited()
        {
            InputManager.AttackPressed -= ActivateDetonator;
            shockwaveMaterial = null;
        }

        private async void ActivateDetonator()
        {
            shockwaveMaterial?.Result.SetFloat("_Size", shockwaveStartSize);
            shockwaveMaterial?.Result.DOFloat(shockwaveDestSize, "_Size", shockwaveDuration).SetEase(shockwaveEase);
            Exited();
            Destroy(this);
            Destroy(detonatorTransform.gameObject);
        }
    }
}