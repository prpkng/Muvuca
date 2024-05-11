using DG.Tweening;
using UnityEngine;

namespace Muvuca.UI.Settings
{
    public class ShakeText : MonoBehaviour
    {
        public Vector2 force = Vector2.up * 10f;
        public float duration = .1f;
        public void Shake()
        {
            transform.DOShakePosition(duration, force);
        }
    }
}