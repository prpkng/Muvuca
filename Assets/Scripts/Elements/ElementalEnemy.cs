using System.Collections;
using System.Collections.Generic;
using Muvuca.Input;
using Muvuca.Player;
using Muvuca.Systems;
using Unity.VisualScripting;
using UnityEngine;

namespace Muvuca
{

    public class ElementalEnemy : MonoBehaviour, IEnablable
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
            LevelManager.Instance.activeElement = element;
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
