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

        private bool isInRange;

        public void Disable()
        {
            StopCoroutine(Start());
            isInRange = false;
            LevelManager.Instance.disabledElements.Add(this);
        }

        public void Enable()
        {
            print("checker enabled");
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
                if (shouldBeInRange && !isInRange)
                {
                    isInRange = true;
                    entered?.Invoke();
                }
                else if (!shouldBeInRange && isInRange)
                {
                    isInRange = false;
                    exited?.Invoke();
                }

            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = isInRange ? Color.green : Color.red;
            Gizmos.DrawWireSphere(transform.position, distance);
            Gizmos.color -= Color.black * .5f;
            Gizmos.DrawSphere(transform.position, distance);
        }

    }
}