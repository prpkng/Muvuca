using System;
using UnityEngine;

namespace Muvuca.Game.Common
{
    public class MoveIntoDir : MonoBehaviour
    {
        public Vector2 direction;
        public float speed;
        public float duration = 4;

        private float counter;

        [SerializeField] private bool faceDir;

        private void Update()
        {
            transform.position += Time.deltaTime * speed * (Vector3)direction;
            if (duration <= 0) return;
            counter += Time.deltaTime / duration;
            if (counter > 1f)
                Destroy(gameObject);

            if (faceDir) transform.up = direction;
        }
    }
}