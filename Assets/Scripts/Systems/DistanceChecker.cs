using System;
using System.Collections;
using UnityEngine;

namespace Muvuca.Systems
{
    public class DistanceChecker : MonoBehaviour, IEnablable
    {
        public float checkFrequency = 0.1f;
        public float distance = 1f;

        public Transform target;

        public Action entered;
        public Action exited;

        public bool IsInRange { get; private set; }

        [ReadOnly] public bool isRunning;

        public void Disable()
        {
            isRunning = false;
            StopAllCoroutines();
            IsInRange = false;
            LevelManager.Instance.disabledElements.Add(this);
        }

        public void Enable()
        {
            isRunning = true;
            gameObject.SetActive(true);
            StartCoroutine(Start());
        }

        public IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(checkFrequency);

                if (!target)
                    continue;

                var shouldBeInRange = Vector2.Distance(transform.position, target.position) < distance;
                if (shouldBeInRange && !IsInRange)
                {
                    IsInRange = true;
                    entered?.Invoke();
                }
                else if (!shouldBeInRange && IsInRange)
                {
                    IsInRange = false;
                    exited?.Invoke();
                }

            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = IsInRange ? Color.green : Color.red;
            Gizmos.DrawWireSphere(transform.position, distance / 2);
            Gizmos.color -= Color.black * .5f;
            Gizmos.DrawSphere(transform.position, distance / 2);
        }

    }
}