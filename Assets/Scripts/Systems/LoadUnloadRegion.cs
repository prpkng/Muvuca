using System;
using Eflatun.SceneReference;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Muvuca.Systems
{
    public class LoadUnloadRegion : MonoBehaviour
    {
        [SerializeField] private SceneReference regionScene;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player"))
                return;
            SceneManager.LoadSceneAsync(regionScene.BuildIndex, LoadSceneMode.Additive);
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag("Player"))
                return;
            SceneManager.UnloadSceneAsync(regionScene.BuildIndex, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
        }
    }
}