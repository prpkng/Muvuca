using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Muvuca.Player
{
    public class PlayerSpriteFlipping : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spr;
        private Camera cam;

        private void Awake() => cam = Camera.main;

        private void Update()
        {
            var mousePos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            spr.flipX = mousePos.x < transform.position.x;
        }
    }
}