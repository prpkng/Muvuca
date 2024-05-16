using System;
using DG.Tweening;
using UnityEngine;

namespace Muvuca.Effects
{
    public class CameraOffsetChanger : MonoBehaviour
    {
        [SerializeField] private Vector2 offset;
        [SerializeField] private float duration;
        [SerializeField] private Ease ease;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player"))
                return;
            
            CameraOffsetApplier.ChangeOffset(offset, duration, ease);
        }
    }
}