using UnityEngine;

namespace Muvuca.Game.Systems
{
    public class DestroyWithMe : MonoBehaviour
    {
        [SerializeField] private Object[] objectsToDestroy;

        private void OnDisable()
        {
            foreach (var o in objectsToDestroy) Destroy(o);
            Destroy(this);
        }
    }
}