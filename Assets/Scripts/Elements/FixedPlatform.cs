using Muvuca.Systems;
using UnityEngine;

namespace Muvuca.Elements {
    public abstract class FixedPlatform : MonoBehaviour, IEnablable {
        public bool hasPlayer;
        public bool reverse;

        public virtual void Disable() {
            if (gameObject.TryGetComponent(out DistanceChecker checker)) checker.Disable();
            hasPlayer = false;
            gameObject.SetActive(false);
            LevelManager.Instance.disabledElements.Add(this);
        }
        public virtual void Enable() {
            gameObject.SetActive(true);
            gameObject.AddComponent<PlatformReacher>();
        }
    }
}