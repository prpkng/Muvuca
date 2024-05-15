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
        private CinemachineFramingTransposer transposer;


        public static void TriggerShake() => shakeEvent?.Invoke();
        private static event Action shakeEvent;

        private void Awake()
        {
            transposer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();
        }

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
            DOTween.Shake(
                () => new Vector3(transposer.m_ScreenX, transposer.m_ScreenY),
                v =>
                {
                    transposer.m_ScreenX = v.x;
                    transposer.m_ScreenY = v.y;
                },
                shakeDuration,
                shakeForce,
                vibrato,
                randomness
            );

            transform.DOShakeRotation(angleShakeDuration, angleShakeForce, angleVibrato, angleRandomness);
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