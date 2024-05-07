using Muvuca.Elements;
using Muvuca.Systems;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Muvuca.Player
{
    public class PlayerController : MonoBehaviour, IEnablable
    {
        public float movingSpeed;

        public Transform platform;

        private StateMachine machine = new();

        public LineRenderer lineRenderer;

        public static PlayerController Instance;

        private void OnDisable()
        {
            machine.currentState?.Exit();
        }

        private void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            machine.AddState("idle", new IdleState());
            machine.AddState("moving", new MovingState());
            machine.owner = this;
            machine.ChangeState("idle", new string[] { });
            if (platform.TryGetComponent(out PlatformRotator plat))
                plat.hasPlayer = true;

            LevelManager.Instance.startingPlatform = platform;
        }

        void Update()
        {
            machine.Update();
        }

        private void FixedUpdate()
        {
            machine.FixedUpdate();
        }

        public void Enable()
        {
            platform = LevelManager.Instance.startingPlatform;
            gameObject.SetActive(true);

            if (platform.TryGetComponent(out PlatformRotator plat))
                plat.hasPlayer = true;

            transform.position = platform.position;
        }

        public void Disable()
        {
            if (platform.TryGetComponent(out PlatformRotator plat)) plat.Disable();
            LevelManager.Instance.disabledElements.Add(this);
            machine.ChangeState("idle");
        }

        public Action<Transform> EnteredPlatform;
    }
}