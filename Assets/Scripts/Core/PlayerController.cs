using System;
using Cysharp.Threading.Tasks;
using FMODUnity;
using Muvuca.Effects;
using Muvuca.Game.Elements.Platform;
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


        internal Transform lastSafePlatform;

        public Action<Transform> enteredPlatform;
        [SerializeField] public Collider2D col;

        private void Awake()
        {
            Instance = this;
            health = GetComponent<HealthSystem>();
            enteredPlatform += plat =>
            {
                if (plat.TryGetComponent(out LaunchPlatform p) && p.isSafePlatform) lastSafePlatform = plat;
                else if (plat.GetChild(0).TryGetComponent(out p) && p.isSafePlatform) lastSafePlatform = plat;
            };
        }

        private void Start()
        {
            machine.AddState("idle", new IdleState());
            machine.AddState("moving", new MovingState());
            machine.AddState("return", new ReturnState());
            machine.owner = this;
            machine.ChangeState("moving", new string[] { });


            PlayAnimation("idle");
        }

        private void Update()
        {
            if (Time.timeScale == 0) return;
            machine.Update();
        }

        private void FixedUpdate()
        {
            machine.FixedUpdate();
        }

        public bool IsInReturnState => machine.currentStateName == "return";

        public void PlayAnimation(string id)
        {
            animator.Play(id);
        }
    }
}