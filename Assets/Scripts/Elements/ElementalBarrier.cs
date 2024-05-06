using System.Collections;
using System.Collections.Generic;
using Muvuca.Input;
using Muvuca.Player;
using Muvuca.Systems;
using UnityEngine;

namespace Muvuca.Elements
{
    public class ElementalBarrier : MonoBehaviour, IEnablable
    {
        [SerializeField] private Element element;
        private DistanceChecker distanceChecker;

        private void Start()
        {
            distanceChecker = GetComponent<DistanceChecker>();
            distanceChecker.target = PlayerController.Instance.transform;
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
    }
}
