using System;
using System.Collections;
using System.Collections.Generic;
using Muvuca.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Muvuca.Effects
{
    public class PlayerLowHealthIndicator : MonoBehaviour
    {
        [SerializeField] private Image img;
        [SerializeField] private float speed;
        [SerializeField] private float alphaMutliplier;
        private void Update()
        {
            var clr = img.color;
            clr.a = PlayerController.Instance.health.currentHp <= 1
                ? Mathf.Abs(Mathf.Sin(Time.time * speed)) * alphaMutliplier
                : 0f;
            img.color = clr;

        }
    }
}