using System;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Muvuca.Effects
{
    public static class ScreenTransition
    {
        private static readonly int Size = Shader.PropertyToID("_Size");

        private const string TransitionMaterialPath = "FullScreenCircleTransition";
        
        public static async void TransitionIn(float duration, Action then)
        {
            var transitionMat = Resources.Load<Material>(TransitionMaterialPath);
            transitionMat.SetFloat(Size, 1f);
            transitionMat.DOFloat(0f, Size, duration).onComplete += () => then?.Invoke();
        }
        public static async void TransitionOut(float duration, Action then)
        {
            var transitionMat = Resources.Load<Material>(TransitionMaterialPath);

            transitionMat.SetFloat(Size, 0f);
            transitionMat.DOFloat(1f, Size, duration).onComplete += () => then?.Invoke();
        }
        public static async Task TransitionIn(float duration) 
        {
            var transitionMat = Resources.Load<Material>(TransitionMaterialPath);
            transitionMat.SetFloat(Size, 1f);
            await transitionMat.DOFloat(0f, Size, duration).AsyncWaitForCompletion();
        }
        public static async Task TransitionOut(float duration) 
        {
            var transitionMat = Resources.Load<Material>(TransitionMaterialPath);
            transitionMat.SetFloat(Size, 0f);
            await transitionMat.DOFloat(1f, Size, duration).AsyncWaitForCompletion();
        }
    }
}