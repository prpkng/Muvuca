using System;
using System.Collections;
using UnityEngine;

namespace Muvuca.Systems
{
    public class HitboxChecker : MonoBehaviour, IEnablable
    {
        public Action entered;
        public Action exited;

        public bool IsInRange { get; private set; }

        [HideInInspector] public bool isRunning = true;

        public void Disable()
        {
            isRunning = false;
            IsInRange = false;
            LevelManager.Instance.disabledElements.Add(this);
        }

        public void Enable()
        {
            isRunning = true;
            gameObject.SetActive(true);
        }


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