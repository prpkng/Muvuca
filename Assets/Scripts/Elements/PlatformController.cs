using Muvuca.Systems;
using UnityEngine;
namespace Muvuca.Elements
{
    public class PlatformController : MonoBehaviour, IEnablable
    {
        public bool hasPlayer;
        public bool reverse;
        public float rotationSpeed;

        private Quaternion startRot;
        private Vector3 startPos;

        private void Awake()
        {
            startRot = transform.rotation;
            startPos = transform.position;
        }

        public void Disable()
        {
            if (gameObject.TryGetComponent(out DistanceChecker checker)) checker.Disable();
            hasPlayer = false;
            gameObject.SetActive(false);
            LevelManager.Instance.disabledElements.Add(this);
        }

        public void Enable()
        {
            gameObject.SetActive(true);
            gameObject.AddComponent<PlatformReacher>();

            transform.rotation = startRot;
            transform.position = startPos;
        }

        public void Update()
        {
            if (!hasPlayer) return;
            transform.Rotate((reverse ? -1 : 1) * rotationSpeed * Time.deltaTime * Vector3.forward);
        }
    }
}