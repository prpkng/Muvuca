using System;
using UnityEngine;
using UnityEngine.Events;

namespace Muvuca.Systems
{
    public class HealthTrigger : MonoBehaviour
    {
        [SerializeField] private HealthSystem health;
        [SerializeField] private int healthPoint;
        [SerializeField] private UnityEvent fire;

        private void Start()
        {
            health.onDamage.AddListener(OnDamage);
        }

        private void OnDamage(int amount) {
            if (health.currentHp > healthPoint)
                return;
            fire.Invoke();
            Destroy(this);
        }
    }
}