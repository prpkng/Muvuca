using System;
using UnityEngine;

namespace Muvuca.Effects
{
    public class Parallax : MonoBehaviour
    {
        private void Start()
        {
            target = Camera.main?.transform;
            startPos = transform.position * (1-parallaxSpeed);
        }

        private Transform target;

        private Vector2 startPos;
        
        [SerializeField] private float parallaxSpeed;

        private void LateUpdate()
        {
            transform.position = startPos + (Vector2)target.position * parallaxSpeed;
        }
    }
}
