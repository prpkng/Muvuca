using System;
using UnityEngine;

namespace Muvuca.Core.Utils
{
    public class DoNotRotatePLEASE : MonoBehaviour
    {
        private void Update()
        {
            transform.localRotation = Quaternion.identity;
        }
    }
}