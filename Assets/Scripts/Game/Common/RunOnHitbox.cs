using System;
using UnityEngine;
using UnityEngine.Events;

namespace Muvuca.Systems
{
    public class RunOnHitbox : MonoBehaviour
    {
        [SerializeField] private HitboxChecker checker;
        [SerializeField] private UnityEvent @event;
        [SerializeField] private UnityEvent exit;
        private void OnEnable()
        {
            checker.entered += Entered;
            checker.exited += Entered;
        }

        private void OnDisable()
        {
            checker.entered -= Entered;
        }

        private void Entered()
        {
            @event.Invoke();
        }
        private void Exited()
        {
            exit.Invoke();
        }
    }
}