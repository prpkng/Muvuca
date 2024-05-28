using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
using UnityEditor;

namespace Muvuca.Effects
{
    [Serializable]
    public struct ShakeData
    {
        public float force;
        public float duration;
        public int vibrato;
        public int randomness;

        public ShakeData(float force = .1f, float duration = .25f, int vibrato = 25, int randomness = 90)
        {
            this.force = force;
            this.duration = duration;
            this.vibrato = vibrato;
            this.randomness = randomness;
        }
    }
    public class CameraShaker : MonoBehaviour
    {
        [SerializeField] private CinemachineCameraOffset offset;


        public static void TriggerShake(ShakeData? data = null, ShakeData? angleData = null) => shakeEvent?.Invoke(
            data ?? new ShakeData(.1f, .25f, 25, 90),
            angleData ?? new ShakeData(10f, .25f, 50, 90)
            );
        private static event Action<ShakeData, ShakeData> shakeEvent;

        private void OnEnable()
        {
            shakeEvent += OnShake;
        }

        private void OnDisable()
        {
            shakeEvent -= OnShake;
        }

        private void OnShake(ShakeData data, ShakeData angleData)
        {
            this.DOKill(true);
            transform.DOKill(true);
            DOTween.Shake(
                () => offset.m_Offset,
                v => offset.m_Offset = v,
                data.duration,
                data.force,
                data.vibrato,
                data.randomness
            ).SetTarget(this);

            transform.DOShakeRotation(angleData.duration, angleData.force * Vector3.forward, angleData.vibrato, angleData.randomness);
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