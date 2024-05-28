using System;
using Cysharp.Threading.Tasks;
using FMOD.Studio;
using FMODUnity;
using Muvuca.Effects;
using Muvuca.Game.Player;
using Muvuca.Systems;
using Muvuca.Tools;
using UnityEngine;

namespace Muvuca.Core
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance;

        public bool isInvulnerable = false;
        
        public float minimumReturnDistance = 5f;
        public float movingSpeed;
        public float returnSpeed;
        [SerializeField] public PlayerSpriteFlipping flipping;
        
        public LineRenderer lineRenderer;
        public AnimationCurve distanceSpeedCurve;
        
        [SerializeField] private float playerInvulnerabilityTime = 2f; 
        
        public readonly StateMachine machine = new();

        [ReadOnly] public bool hasPlatform;
        [ReadOnly] public Transform platform;
        [ReadOnly] public HealthSystem health;

        [Header("Sounds")] 
        public StudioEventEmitter jumpSound;
        public StudioEventEmitter hitSound;
        
        public Animator animator;
        public Flashing flashing;
        
        public static Action PlayerHealthChanged;
        
        
        
        public async void DamagePlayer(int amount = 0)
        {
            if (machine.currentStateName != "idle") 
                PlayAnimation("damage");
            CameraShaker.TriggerShake();
            health.DoDamage(amount);
            PlayerHealthChanged?.Invoke();
            flashing.Play();
            hitSound.Play();
            isInvulnerable = true;
            await UniTask.WaitForSeconds(playerInvulnerabilityTime);
            isInvulnerable = false;

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
            machine.AddState("return", new ReturnState());
            machine.owner = this;
            machine.ChangeState("moving", new string[] { });

            
            PlayAnimation("idle");
            
            if (SaveSystem.TryGetString(SaveSystemKeyNames.PlayerSpawnPos, out var s))
                transform.position = Util.DeserializeVector2(s);
        }

        void Update()
        {
            if (Time.timeScale == 0) return;
            machine.Update();
        }

        private void FixedUpdate()
        {
            machine.FixedUpdate();
        }

        public Action<Transform> enteredPlatform;

        public void PlayAnimation(string id)
        {
            animator.Play(id);
        }
    }
}