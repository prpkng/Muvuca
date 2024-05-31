namespace Muvuca.Systems
{
    using UnityEngine;

    public class MovingDetonatorTrigger : MonoBehaviour
    {
        public void Trigger() => MovingDetonator.ReachEnd();
    }
}