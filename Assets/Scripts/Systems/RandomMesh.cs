using UnityEngine;

namespace Muvuca.Systems
{
    public class RandomMesh : MonoBehaviour
    {
        [SerializeField] private Mesh[] meshes;

        [SerializeField] private MeshFilter meshFilter;

        private void Awake()
        {
            meshFilter.mesh = meshes[Random.Range(0, meshes.Length)];
            Destroy(this);
        }
    }
}