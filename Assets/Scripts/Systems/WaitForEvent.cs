using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Muvuca.Systems
{
    public class WaitForEvent : MonoBehaviour
    {
        public UnityEvent fire;
        
        public async void Run(float timeToWait)
        {
            await UniTask.WaitForSeconds(timeToWait, true);
            fire.Invoke();
        }
    }
}