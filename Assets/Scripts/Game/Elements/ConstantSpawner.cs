using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Muvuca.Game.Elements
{
    public class ConstantSpawner : MonoBehaviour
    {
        public float spawnsPerMinute = 60;

        public GameObject spawnObj;
        public Transform spawnTransform;

        public float randomizationForce;


        private void OnEnable()
        {
            counter = 0;
        }

        private float counter;
        public void Update()
        {
            if (counter <= 0)
            {
                counter = 60f / spawnsPerMinute;
                var angle = Random.Range(0, 360);
                var offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * randomizationForce;
                Instantiate(spawnObj).transform.position = spawnTransform.position + offset;
                return;
            }

            counter -= Time.deltaTime;
        }
    }
}