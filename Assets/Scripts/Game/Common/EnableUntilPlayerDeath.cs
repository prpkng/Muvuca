using System;
using Muvuca.Core;
using Muvuca.Systems;
using UnityEngine;

namespace Muvuca.Game.Common
{
    public class EnableUntilPlayerDeath : MonoBehaviour
    {
        [SerializeField] private GameObject objectToDisable;
        [SerializeField] private HitboxChecker hitbox;

        private void OnEnable()
        {
            hitbox.entered += Entered;
        }

        private void OnDisable()
        {
            hitbox.entered -= Entered;
        }

        private void Entered()
        {
            if (!objectToDisable.activeSelf)
                return;
            LevelManager.onLevelReset += () => objectToDisable.SetActive(true);
            objectToDisable.SetActive(false);
        }
    }
}