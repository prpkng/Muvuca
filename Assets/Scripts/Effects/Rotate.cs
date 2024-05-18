using System;
using UnityEngine;

namespace Muvuca.Effects
{
    public class Rotate : MonoBehaviour
    {
        [SerializeField] private Vector3 velocity;

        private Vector3 startRotation;
        
        private void Start()
        {
            startRotation = transform.eulerAngles;
        }

        private void Update()
        {
            var euler = startRotation; 
            if (velocity.x != 0) euler.x = velocity.x * Time.time % 360f;
            if (velocity.y != 0) euler.y = velocity.y * Time.time % 360f;
            if (velocity.z != 0) euler.z = velocity.z * Time.time % 360f;
            transform.eulerAngles = euler;
        }
    }
}