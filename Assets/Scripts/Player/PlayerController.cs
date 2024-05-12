using Muvuca.Elements;
using Muvuca.Systems;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Muvuca.Player
{
    public class PlayerController : MonoBehaviour
    {
        public float movingSpeed;

        public Transform platform;

        private StateMachine machine = new();

        public LineRenderer lineRenderer;

        public static PlayerController Instance;

        public void SetDirection(Vector2 direction)
        {
            machine.ChangeState("moving", new string[] { Util.SerializeVector3Array(new Vector3[] { direction }) });
            transform.up = direction;
        }

        private void OnDisable()
        {
            machine.currentState?.Exit();
        }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            machine.AddState("idle", new IdleState());
            machine.AddState("moving", new MovingState());
            machine.owner = this;
            machine.ChangeState("idle", new string[] { });
            if (platform.TryGetComponent(out LaunchPlatform plat))
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

        public Action<Transform> enteredPlatform;
    }
}