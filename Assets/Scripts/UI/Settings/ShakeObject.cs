using DG.Tweening;
using UnityEngine;

namespace Muvuca.UI.Settings
{
    public class ShakeObject : MonoBehaviour
    {
        public Vector2 force = Vector2.up * 10f;
        public float duration = .1f;
        public int vibrato = 10;
        public float randomness = 90f;
        public void Shake()
        {
            transform.DOShakePosition(duration, force, vibrato, randomness);
        }
    }
}