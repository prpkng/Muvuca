using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using Muvuca.Core;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Muvuca.Game.Elements.Platform
{
    public class BreakingPlatform : HitboxListener
    {
        [SerializeField] private LaunchPlatform platform;
        [SerializeField] private float duration;

        [SerializeField] private SpriteRenderer[] lights;
        [SerializeField] private Sprite activeSprite;
        [SerializeField] private Sprite inactiveSprite;

        [SerializeField] private UnityEvent onBreak;
        [SerializeField] private UnityEvent onReset;

        private bool broken;

        private void Awake()
        {
            CheckpointPlatform.PlayerEnteredPlatform += Fix;
        }


        private void OnDestroy()
        {
            CheckpointPlatform.PlayerEnteredPlatform -= Fix;
        }

        private void Fix()
        {
            if (breakCoroutine != null) StopCoroutine(breakCoroutine);
            breakCoroutine = null;
            if (!broken) return;
            broken = false;
            onReset.Invoke();
            for (int i = 0; i < 3; i++)
                lights[i].sprite = activeSprite;
        }

        private IEnumerator breakCoroutine;

        protected override void Entered()
        {
            breakCoroutine = Break();
            StartCoroutine(breakCoroutine);
        }

        private IEnumerator Break()
        {
            for (int i = 0; i < 3; i++)
            {
                lights[i].sprite = inactiveSprite;
                yield return new WaitForSeconds(duration / 3f);
            }
            onBreak.Invoke();
            broken = true;
            if (!platform.hasPlayer) yield break;

            platform.hasPlayer = false;
            PlayerController.Instance.DamagePlayer();
            PlayerController.Instance.machine.ChangeState("return");
        }

        protected override void Exited()
        {
            for (int i = 0; i < 3; i++)
                lights[i].sprite = inactiveSprite;
            if (breakCoroutine != null) StopCoroutine(breakCoroutine);
            breakCoroutine = null;
            if (broken) return;
            onBreak.Invoke();
            broken = true;
        }
    }
}