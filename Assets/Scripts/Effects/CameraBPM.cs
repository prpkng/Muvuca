using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Muvuca.Effects
{
    public class CameraBPM : MonoBehaviour
    {
        public float BPM;
        public float beatForce;
        public float returnSpeed;

        private UnityEngine.Camera cam;

        private float startZoom;

        public void Beat()
        {
            cam.orthographicSize = startZoom + beatForce;
        }


        public static void TriggerBeat() => _BeatEvent?.Invoke();
        private static event Action _BeatEvent;

        void Awake()
        {
            cam = GetComponent<UnityEngine.Camera>();
            startZoom = cam.orthographicSize;
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
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, startZoom, Time.deltaTime * returnSpeed);
        }
    }
}