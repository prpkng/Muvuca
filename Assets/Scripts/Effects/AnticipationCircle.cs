using System.Collections;
using System.Collections.Generic;
using Muvuca.Core;
using Muvuca.Player;
using UnityEngine;

namespace Muvuca.Effects
{
    public class AnticipationCircle : MonoBehaviour
    {

        [SerializeField] private SpriteRenderer spriteRenderer;

        [SerializeField] private AnimationCurve distanceAttenuationCurve;


        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Update()
        {

            float distance = Vector2.Distance(PlayerController.Instance.transform.position, transform.position);
            spriteRenderer.material.SetFloat("_Size", distanceAttenuationCurve.Evaluate(distance));
        }
    }
}
