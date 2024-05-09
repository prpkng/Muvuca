using System.Collections;
using System.Collections.Generic;
using Muvuca.Effects;
using Muvuca.Input;
using Muvuca.Player;
using Muvuca.Systems;
using UnityEngine;

namespace Muvuca.Elements
{
    public class ElementalBarrier : MonoBehaviour, IEnablable
    {
        [SerializeField] private Element element;
        private HitboxChecker distanceChecker;

        private void Start()
        {
            distanceChecker = GetComponent<HitboxChecker>();
        }

        private void OnEnable()
        {
            InputManager.AttackPressed += AttackPressed;
        }

        private void OnDisable()
        {
            InputManager.AttackPressed -= AttackPressed;
        }

        private void AttackPressed()
        {
            if (!distanceChecker.IsInRange) return;
            if (LevelManager.Instance.activeElement != element) return;
            CameraBPM.TriggerBeat();
            LevelManager.Instance.activeElement = Element.Neutral;
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
