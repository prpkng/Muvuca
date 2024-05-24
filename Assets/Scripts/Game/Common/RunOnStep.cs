using UnityEngine;
using UnityEngine.Events;

namespace Muvuca.Game.Common
{
    public class RunOnStep : MonoBehaviour
    {
        
        [System.Serializable]
        public enum UnityStep
        {
            Start,
            Awake,
            OnEnable,
            OnDisable,
            OnDestroy,
            // Update,
            // FixedUpdate,
            // LateUpdate,
        }

        [SerializeField] private UnityStep step;
        
        [SerializeField] private UnityEvent @event;
        
        public void Start() { if (step == UnityStep.Start) @event.Invoke(); }
        public void Awake() { if (step == UnityStep.Awake) @event.Invoke(); }
        public void OnEnable() { if (step == UnityStep.OnEnable) @event.Invoke(); }
        public void OnDisable() { if (step == UnityStep.OnDisable) @event.Invoke(); }
        public void OnDestroy() { if (step == UnityStep.OnDestroy) @event.Invoke(); }
    }
}