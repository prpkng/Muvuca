using System;
using PathCreation;
using UnityEngine;

namespace Muvuca.Game.Elements
{
    public class FollowPathWrap : MonoBehaviour
    {
        public PathCreator path;

        public float followSpeed;

        private void Start()
        {
            timer = 0f;
        }

        private float timer;

        private void Update()
        {
            timer += followSpeed / path.path.length;
            
            transform.position += path.path.GetPointAtTime(timer, EndOfPathInstruction.Stop);
            if (timer > 1f)
                Destroy(gameObject);
        }
    }
}