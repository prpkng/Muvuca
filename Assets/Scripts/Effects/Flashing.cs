using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Muvuca.Effects
{
    public class Flashing : MonoBehaviour
    {
        [SerializeField] private Behaviour behaviour;
        [SerializeField] private new Renderer renderer;
        public float enabledInterval = .25f;
        public float disabledInterval = .1f;
        public float stopOnSeconds;
        public bool playOnAwake;

        private bool enabled = false;
        
        private void Start()
        {
            enabled = true;
            if (playOnAwake) Play();
        }

        public void Play()
        {
            Stop();
            StartCoroutine(Playing());
            
            if (stopOnSeconds != 0)
                Invoke(nameof(Stop), stopOnSeconds);
        }

        public void Stop()
        {
            StopAllCoroutines();
            enabled = true;
            if (renderer) renderer.enabled = true;
            if (behaviour) behaviour.enabled = true;
        }

        
        private IEnumerator Playing()
        {
            while (true)
            {
                enabled = !enabled;
                if (renderer) renderer.enabled = enabled;
                if (behaviour) behaviour.enabled = enabled;
                yield return new WaitForSeconds(enabled ? enabledInterval : disabledInterval);
            }
        }
    }
}