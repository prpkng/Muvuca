using System;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

namespace Muvuca.Effects
{
    public class CameraOffsetApplier : MonoBehaviour
    {
        public static event Action<Vector2, float, Ease> OffsetChanged;
        private CinemachineTransposer vCam;

        private void Start()
        {
            vCam = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTransposer>();
        }

        private void OnEnable()
        {
            OffsetChanged += OnOffsetChanged;
        }

        private void OnDisable()
        {
            OffsetChanged -= OnOffsetChanged;
        }

        private void OnOffsetChanged(Vector2 offset, float duration, Ease ease)
        {
            this.DOKill();
            DOTween.To(() => vCam.m_FollowOffset, v => vCam.m_FollowOffset = new Vector3(v.x, v.y, vCam.m_FollowOffset.z), (Vector3)offset, duration)
                .SetEase(ease).SetTarget(this);
        }

        public static void ChangeOffset(Vector2 arg1, float arg2, Ease arg3) => OffsetChanged?.Invoke(arg1, arg2, arg3);
    }
}