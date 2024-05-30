using System;
using Unity.Mathematics;
using UnityEngine;

namespace Muvuca.Core.Utils
{
    public class KeepGlobalIdentity : MonoBehaviour
    {
        private void Update()
        {
            transform.rotation = quaternion.identity;
        }
    }
}