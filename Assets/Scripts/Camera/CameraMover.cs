using System;
using DG.Tweening;
using UnityEngine;

namespace Muvuca.Camera
{
    public class CameraMover : MonoBehaviour
    {
        public static void MoveTo(Vector2 dest, float duration, Ease ease) => OnMoveTo?.Invoke(dest, duration, ease);
        private static event Action<Vector2, float, Ease> OnMoveTo;

        private void OnEnable() => OnMoveTo += Move;

        private void OnDisable() => OnMoveTo -= Move;

        private void Move(Vector2 dest, float duration, Ease ease)
        {
            transform.DOKill();
            transform.DOMove(new Vector3(dest.x, dest.y, transform.position.z), duration).SetEase(ease);
        }
    }
}