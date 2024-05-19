using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using PathCreation;
using UnityEngine;
using UnityEngine.Serialization;

namespace Muvuca.Systems
{
    public class PathFollow : MonoBehaviour
    {
        public PathCreator pathCreator;

        public bool followNormal;
        public EndOfPathInstruction endOfPathInstruction;
        public float speed = 5;
        
        private float startDistance;
        private Vector2 startOffset;
        private void Awake()
        {
            var path = pathCreator.path;
            var position = transform.position;
            startDistance = path.GetClosestDistanceAlongPath(position);
            startOffset = path.GetPointAtDistance(startDistance) - position;
        }


        private void Update()
        {
            var normal = pathCreator.path.GetNormalAtDistance(startDistance + speed * Time.time, endOfPathInstruction);
            transform.position = (Vector3)Vector2.Reflect(startOffset, normal) 
                                 + pathCreator.path.GetPointAtDistance(startDistance + speed * Time.time, endOfPathInstruction);
            if (followNormal) transform.right = normal;

        }
    }
}