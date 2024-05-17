using System;
using Muvuca.Effects;
using Muvuca.Player;
using Muvuca.Systems;
using Muvuca.Tools;
using UnityEngine;

namespace Muvuca.Core
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance;
        
        public float movingSpeed;
        public LineRenderer lineRenderer;

        
        private readonly StateMachine machine = new();
        
        [ReadOnly] public Transform platform;
        [ReadOnly] public HealthSystem health;
        public Animator animator;

        public static Action PlayerGotHit;
        
        public void DamagePlayer(int amount = 0)
        {
            CameraShaker.TriggerShake();
            health.DoDamage(amount);
            PlayerGotHit?.Invoke();
            print("Damaged player!");
        }
        
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
            health = GetComponent<HealthSystem>();
        }

        private void Start()
        {
            machine.AddState("idle", new IdleState());
            machine.AddState("moving", new MovingState());
            machine.owner = this;
            machine.ChangeState("moving", new string[] { });

            if (SaveSystem.TryGetString(SaveSystemKeyNames.PlayerSpawnPos, out var s))
                transform.position = Util.DeserializeVector2(s);
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

        public void PlayAnimation(string id)
        {
            print($"Played {id}");
            animator.Play(id);
        }
    }
}