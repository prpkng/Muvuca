using System;
using Muvuca.Core;
using Muvuca.Systems;
using UnityEngine;

namespace Muvuca.Game.Elements.Platform
{
    public class PlatformReacher : HitboxListener
    {
        [SerializeField] private float preRotateStartDistance;

        [SerializeField] private float preRotateSpeed;

        protected override void Entered()
        {
            PlayerController.Instance.enteredPlatform?.Invoke(transform);
        }

        private void Awake()
        {
            hitbox = GetComponent<HitboxChecker>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
        }

        private void Update()
        {
            if (hitbox.IsInRange)
                return;
            if (Vector2.Distance(PlayerController.Instance.transform.position, transform.position) > preRotateStartDistance)
                return;
            transform.up = Vector3.Lerp(transform.up, (PlayerController.Instance.transform.position - transform.position).normalized, Time.deltaTime * preRotateSpeed);
        }
    }
}