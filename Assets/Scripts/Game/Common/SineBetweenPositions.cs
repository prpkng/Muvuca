using System;
using UnityEngine;

namespace Muvuca.Game.Common
{
    public class SineBetweenPositions : MonoBehaviour
    {
        [SerializeField] private Transform from;
        [SerializeField] private Transform to;

        [SerializeField] private float speed;
        [SerializeField] private AnimationCurve curve;

        [SerializeField] private float offset;

        private void Update() =>
            transform.position = Vector3.Lerp(from.position, to.position, curve.Evaluate(Mathf.PingPong((Time.time + offset) * speed, 1f)));
    }
}