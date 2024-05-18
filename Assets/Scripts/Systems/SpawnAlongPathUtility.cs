using System;
using System.Collections.Generic;
using PathCreation;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Muvuca.Systems
{
    public class SpawnAlongPathUtility : MonoBehaviour
    {
        public PathCreator path;

        public Vector3 randomOffsetValue;
        public bool randomlyRotated;
        public int objectCount = 100;
        [SerializeField] private GameObject objectPrefab;
        [SerializeField] private EdgeCollider2D edgeCollider;
        [SerializeField] private LineRenderer lineRenderer;
        public int edgeColliderPointCount = 100;
        
        public void Spawn()
        {
            foreach (Transform child in transform) DestroyImmediate(child.gameObject);
            for (var i = 0; i < objectCount; i++)
            {
                var pos = path.path.GetPointAtTime(1f / objectCount * i);
                var obj = (GameObject)PrefabUtility.InstantiatePrefab(objectPrefab);
                obj.transform.parent = transform;
                obj.transform.position = pos + new Vector3(
                    Random.Range(-randomOffsetValue.x, randomOffsetValue.x),
                    Random.Range(-randomOffsetValue.y, randomOffsetValue.y),
                    Random.Range(-randomOffsetValue.z, randomOffsetValue.z));
                if (randomlyRotated)
                    obj.transform.eulerAngles = Vector3.forward * Random.Range(0, 360);
                if (obj.TryGetComponent(out PathFollow f))
                    f.pathCreator = path;
            }
        }

        public void GenerateCollisions()
        {
            var points = new List<Vector2>();
            for (var i = 0; i < edgeColliderPointCount+1; i++)
            {
                var pos = path.path.GetPointAtTime(1f / edgeColliderPointCount * i);
                points.Add(pos - transform.position);
            }

            edgeCollider.points = points.ToArray();
        }
        public void GenerateLines()
        {
            var points = new List<Vector3>();
            for (var i = 0; i < edgeColliderPointCount+1; i++)
            {
                var pos = path.path.GetPointAtTime(1f / edgeColliderPointCount * i);
                points.Add(pos - transform.position);
            }

            lineRenderer.SetPositions(points.ToArray());
        }
    }
}