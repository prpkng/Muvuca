using System;
using UnityEngine;
using UnityEngine.Events;

namespace Muvuca.Systems
{
    public class VisibilityNotify : MonoBehaviour
    {
        [SerializeField] private UnityEvent onBecameVisible; 
        private void OnBecameVisible()
        {
            onBecameVisible.Invoke();
        }

        [SerializeField] private UnityEvent onBecameInvisible; 
        private void OnBecameInvisible()
        {
            onBecameInvisible.Invoke();
        }
    }
}