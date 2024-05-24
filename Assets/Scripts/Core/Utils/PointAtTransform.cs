using Muvuca.UI.HUD;
using UnityEngine;

namespace Muvuca.Effects
{
    public class PointAtTransform : MonoBehaviour
    {
        [SerializeField] private Transform trans;
        public void SetTarget(Transform dest) => trans = dest;
        public void Point() => ArrowIndicator.Target = trans.position;
        public void RemovePoint() => ArrowIndicator.Target = null;
    }
}