namespace Muvuca.Camera
{
    using DG.Tweening;
    using Muvuca.Systems;
    using UnityEngine;

    public class CameraPositionTrigger : MonoBehaviour
    {
        public Vector2 destination;
        public float duration;
        public Ease ease;
        private void OnTriggerEnter2D(Collider2D other)
        {
            print("Move");
            if (!other.gameObject.CompareTag("Player"))
                return;
            CameraMover.MoveTo(destination, duration, ease);
        }
    }
}