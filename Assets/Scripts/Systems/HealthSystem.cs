using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Muvuca.Systems
{
    public class HealthSystem : MonoBehaviour
    {
        public UnityEvent onDie;
        public UnityEvent<int> onDamage;

        [SerializeField] private Collider2D col;

        public int hitCooldown = 500;
        
        public int currentHealthPoints;
        
        public async void DoDamage(int amount = 1)
        {
            onDamage.Invoke(amount);
            currentHealthPoints -= amount;
            if (currentHealthPoints <= 0)
                onDie.Invoke();
            
            col.enabled = false;
            await Task.Delay(hitCooldown);
            col.enabled = true;
        }
    }
}