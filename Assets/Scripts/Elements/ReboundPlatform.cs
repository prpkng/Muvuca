namespace Muvuca.Elements
{
    using Muvuca.Effects;
    using Muvuca.Input;
    using Muvuca.Player;
    using Muvuca.Systems;
    using UnityEngine;

    public class ReboundPlatform : MonoBehaviour, IEnablable
    {
        private DistanceChecker distanceChecker;
        [Range(0, 360)]
        public float newDirection;

        private void OnDrawGizmosSelected()
        {
            var angle = Mathf.Deg2Rad * newDirection;
            var dir = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle));
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + dir * 2);
        }

        private void Start()
        {
            distanceChecker = GetComponent<DistanceChecker>();
            distanceChecker.target = PlayerController.Instance.transform;
        }

        private void OnEnable()
        {
            InputManager.JumpPressed += JumpPressed;
        }

        private void OnDisable()
        {
            InputManager.JumpPressed -= JumpPressed;
        }

        private void JumpPressed()
        {
            if (!distanceChecker.IsInRange) return;
            var angle = Mathf.Deg2Rad * newDirection;
            var dir = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle));
            PlayerController.Instance.transform.position = transform.position;
            PlayerController.Instance.SetDirection(dir);

            CameraBPM.TriggerBeat();
            Disable();
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
            distanceChecker.Disable();
            LevelManager.Instance.disabledElements.Add(this);
        }

        private void Update()
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * (distanceChecker.IsInRange ? 1.5f : 1f), Time.deltaTime * 8f);
        }
    }
}