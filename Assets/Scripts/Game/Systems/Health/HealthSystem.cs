using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Muvuca.Systems
{
    public class HealthSystem : MonoBehaviour
    {
        public UnityEvent onDie;
        public UnityEvent<int> onDamage;

        [SerializeField] private Collider2D col;

        [FormerlySerializedAs("currentHealthPoints")] public int currentHp;
        
        public async void DoDamage(int amount = 1)
        {
            currentHp -= amount;
            onDamage.Invoke(amount);
            if (currentHp <= 0)
                onDie.Invoke();
        }
    }
}