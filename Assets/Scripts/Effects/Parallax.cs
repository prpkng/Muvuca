using UnityEngine;

namespace Muvuca.Effects
{
    public class Parallax : MonoBehaviour
    {
        [SerializeField] private Transform target;

        [SerializeField] private float parallaxSpeed;

        private void Update()
        {
            transform.position = target.position * parallaxSpeed;
        }
    }
}
