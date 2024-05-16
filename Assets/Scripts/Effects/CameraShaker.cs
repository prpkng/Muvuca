using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
using UnityEditor;

namespace Muvuca.Effects
{
    public class CameraShaker : MonoBehaviour
    {
        public float shakeForce;
        public float shakeDuration;
        public int vibrato = 10;
        public float randomness =90;
        [Space]
        public float angleShakeForce;
        public float angleShakeDuration;
        public int angleVibrato = 10;
        public float angleRandomness =90;
        [SerializeField] private CinemachineCameraOffset offset;


        public static void TriggerShake() => shakeEvent?.Invoke();
        private static event Action shakeEvent;

        private void OnEnable()
        {
            shakeEvent += OnShake;
        }

        private void OnDisable()
        {
            shakeEvent -= OnShake;
        }

        private void OnShake()
        {
            this.DOKill(true);
            DOTween.Shake(
                () => offset.m_Offset,
                v => offset.m_Offset = v,
                shakeDuration,
                shakeForce,
                vibrato,
                randomness
            ).SetTarget(this);

            transform.DOShakeRotation(angleShakeDuration, angleShakeForce * Vector3.forward, angleVibrato, angleRandomness);
        }
        
    }
    
    
    #if UNITY_EDITOR
    
    [CustomEditor(typeof(CameraShaker))]
    public class CameraShakerEditor : Editor {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            if (GUILayout.Button("Shake")) CameraShaker.TriggerShake();
            
        }
    }
    
    #endif
    
}