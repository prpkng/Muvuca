using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Muvuca
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
