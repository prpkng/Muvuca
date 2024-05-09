using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Muvuca.Effects
{
    public class CameraBPM : MonoBehaviour
    {
        public float BPM;
        public float beatForce;
        public float returnSpeed;

        private CinemachineVirtualCamera vCam;

        private float startZoom;

        public void Beat()
        {
            vCam.m_Lens.OrthographicSize = startZoom + beatForce;
        }


        public static void TriggerBeat() => _BeatEvent?.Invoke();
        private static event Action _BeatEvent;

        void Awake()
        {
            vCam = GetComponent<CinemachineVirtualCamera>();
            startZoom = vCam.m_Lens.OrthographicSize;
        }

        private void OnEnable()
        {
            _BeatEvent += Beat;
        }

        private void OnDisable()
        {
            _BeatEvent -= Beat;
        }

        void Update()
        {
            vCam.m_Lens.OrthographicSize = Mathf.Lerp(vCam.m_Lens.OrthographicSize, startZoom, Time.deltaTime * returnSpeed);
        }
    }
}