using System;
using PathCreation;
using UnityEngine;

namespace Muvuca.Systems
{
    public class AsteroidsSpawner : MonoBehaviour
    {
        public PathCreator path;

        public int asteroidCount = 100;

        [SerializeField] private GameObject asteroidGameObject;
        
        private void Start()
        {
            var objs = InstantiateAsync(asteroidGameObject, asteroidCount, transform);
            objs.completed += _ =>
            {
                for (var i = 0; i < asteroidCount; i++)
                {
                    var pos = path.path.GetPointAtTime(1f / asteroidCount * i);
                    objs.Result[i].transform.position = pos;
                    if (objs.Result[i].TryGetComponent(out PathFollow f))
                        f.pathCreator = path;
                }
            };
        }
    }
}