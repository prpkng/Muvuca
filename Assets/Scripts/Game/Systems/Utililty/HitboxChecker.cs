using System;
using System.Collections;
using Muvuca.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace Muvuca.Systems
{
    public class HitboxChecker : MonoBehaviour
    {
        public string targetTag = "Player";
        public Action entered;
        public Action exited;

        public bool IsInRange { get; protected set; }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag(targetTag))
                return;
            entered?.Invoke();
            IsInRange = true;
        }
        protected virtual void OnTriggerExit2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag(targetTag))
                return;
            exited?.Invoke();
            IsInRange = false;
        }

    }
}