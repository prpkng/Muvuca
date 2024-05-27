using Muvuca.Systems;
using UnityEngine;

namespace Muvuca
{
    public class LineSizeCalculator : MonoBehaviour
    {
        [SerializeField] private LineRenderer lr;
        private void Update()
        {
            lr.SetPosition(1, Vector3.up * Vector2.Distance(transform.position, PlatformSelector.Instance.targetPosition));
        }
    }
}
