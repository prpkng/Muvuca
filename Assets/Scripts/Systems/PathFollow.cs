using System;
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
        private void Start()
        {
            startDistance = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }

        private void Update()
        {
            transform.position = pathCreator.path.GetPointAtDistance(startDistance + speed * Time.time, endOfPathInstruction);
            if (followNormal) transform.right =
                pathCreator.path.GetNormalAtDistance(startDistance + speed * Time.time, endOfPathInstruction);
        }
    }
}