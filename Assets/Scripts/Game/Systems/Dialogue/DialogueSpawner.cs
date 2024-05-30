using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;

namespace Muvuca.Systems.DialogueSystem
{
    public class DialogueSpawner : MonoBehaviour
    {
        private const string DialogueRunnerPrefabPath = "Assets/Game/Prefabs/UI/Dialogue System.prefab";
        [SerializeField] private DialogueData dialogue;

        public UnityEvent onFinished;
        public float delay;
        
        public async void Spawn()
        {
            await UniTask.WaitForSeconds(delay);
            var prefab = Addressables.LoadAssetAsync<GameObject>(DialogueRunnerPrefabPath);
            await prefab.Task;
            var obj = Instantiate(prefab.Result);
            var dr = obj.GetComponentInChildren<DialogueRunner>();
            await dr.RunDialogue(dialogue);
            onFinished.Invoke();
        }
    }
}