using System;
using System.Collections;
using UnityEngine;

namespace Muvuca.Systems
{
    public class HitboxChecker : MonoBehaviour
    {
        public Action entered;
        public Action exited;

        public bool IsInRange { get; private set; }

        [HideInInspector] public bool isRunning = true;


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!isRunning || !other.gameObject.CompareTag("Player"))
                return;
            print("Entered");
            IsInRange = true;
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (!isRunning || !other.gameObject.CompareTag("Player"))
                return;
            IsInRange = false;
        }

    }
}