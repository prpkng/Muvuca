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

        private Camera cam;

        private float startZoom;

        private float beatCounter;
        private bool runningBeat;

        public static void StartBPM() { BPMStarted(); }
        private static Action BPMStarted;
        public static void StopBPM() { BPMStopped(); }
        private static Action BPMStopped;

        private void OnEnable()
        {
            BPMStarted += _StartBeat;
            BPMStopped += _StopBeat;
        }
        private void OnDisable()
        {
            BPMStarted -= _StartBeat;
            BPMStopped -= _StopBeat;
        }

        private void _StartBeat()
        {
            runningBeat = true;
            beatCounter = BPM; // Just for the first beat to happen
        }

        private void _StopBeat() => runningBeat = false;

        void Start()
        {
            cam = GetComponent<Camera>();
            startZoom = cam.orthographicSize;
        }

        void Update()
        {
            if (runningBeat)
                beatCounter += Time.deltaTime;

            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, startZoom, Time.deltaTime * returnSpeed);

            if (beatCounter < 60f / BPM)
                return;

            beatCounter = 0;
            cam.orthographicSize = startZoom + beatForce;
        }
    }
}