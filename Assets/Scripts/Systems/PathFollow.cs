using System;
using PathCreation;
using UnityEngine;
using UnityEngine.Serialization;

namespace Muvuca.Systems
{
    public class PathFollow : MonoBehaviour
    {
        public PathCreator pathCreator;

        public EndOfPathInstruction endOfPathInstruction;
        public float speed = 5;
        private float distanceTravelled;

        private void Start()
        {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }

        private void Update()
        {
            distanceTravelled += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
        }
    }
}