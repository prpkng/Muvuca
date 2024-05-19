using System;
using UnityEngine;

namespace Muvuca.Systems
{
    public class DisableOnAwake : MonoBehaviour
    {
        [SerializeField] private bool enable;
        private void Awake() => gameObject.SetActive(enable);
    }
}