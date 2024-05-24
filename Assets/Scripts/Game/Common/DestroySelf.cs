using UnityEngine;

namespace Muvuca.Game.Common
{
    public class DestroySelf : MonoBehaviour
    {
        public void Destroy_Self() =>
            Destroy(this.gameObject);
    }
}