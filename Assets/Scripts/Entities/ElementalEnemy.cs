using Muvuca.Core;
using Muvuca.Systems;
using UnityEngine;

namespace Muvuca.Entities
{

    public class ElementalEnemy : MonoBehaviour
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
            LevelManager.Instance.activeElement = element;
        }

        private void Update()
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * (distanceChecker.IsInRange ? 1.5f : 1f), Time.deltaTime * 8f);
        }
    }
}
