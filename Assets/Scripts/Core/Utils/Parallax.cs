using System;
using UnityEngine;

namespace Muvuca.Effects
{
    public class Parallax : MonoBehaviour
    {
        private void Start()
        {
            target = Camera.main?.transform;
            startPos = transform.position * parallaxSpeed;
        }

        private Transform target;

        private Vector3 startPos;
        
        [SerializeField] private float parallaxSpeed;

        private void LateUpdate()
        {
            transform.position = startPos + target.position * parallaxSpeed;
        }
    }
}
