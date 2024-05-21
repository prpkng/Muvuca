using System;
using Muvuca.Core;
using UnityEngine;

namespace Muvuca.Effects
{
    public class ParticleByVelocity : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private ParticleSystem particle;

        [SerializeField] private float maxVel;
        [SerializeField] private float maxOpacity;
        [SerializeField] private float minOpacity;
        
        private Color startColor;
        
        private void Awake()
        {
            startColor = particle.main.startColor.color;
        }

        private void Update()
        {
            var color = startColor;
            color.a *= rb.velocity.magnitude.Map(0f, maxVel, minOpacity, maxOpacity);
            var main = particle.main;
            main.startColor = color;
        }
    }
}