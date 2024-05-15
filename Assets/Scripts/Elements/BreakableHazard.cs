using System;
using System.Collections;
using System.Collections.Generic;
using Muvuca.Effects;
using Muvuca.Input;
using Muvuca.Player;
using Muvuca.Systems;
using UnityEngine;

namespace Muvuca.Elements
{
    public class BreakableHazard : MonoBehaviour
    {
        [SerializeField] private Element element;
        private HitboxChecker distanceChecker;
        [SerializeField] private Collider2D col;
        [SerializeField] private float offsetForce;
        private void OnEnable()
        {
            distanceChecker = GetComponent<HitboxChecker>();
            distanceChecker.entered += Entered;
            distanceChecker.exited += Exited;
        }

        private void OnDisable()
        {
            distanceChecker.entered -= Entered;
            distanceChecker.exited -= Exited;
        }

        private void Entered()
        {
            InputManager.AttackPressed += AttackPressed;
        }

        private void Exited()
        {
            InputManager.AttackPressed -= AttackPressed;
            PlayerController.Instance.DamagePlayer(1);
        }

        private void AttackPressed()
        {
            Destroy(gameObject);
            InputManager.AttackPressed -= AttackPressed;
        }

        private void Update()
        {
            if (distanceChecker.IsInRange) return;
            col.offset = (transform.position - PlayerController.Instance.transform.position).normalized * offsetForce;
        }
    }
}
