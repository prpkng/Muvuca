using Cysharp.Threading.Tasks;
using Eflatun.SceneReference;
using Muvuca.Effects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Muvuca.Game.Elements
{
    public class TransitionToScene : MonoBehaviour
    {
        [SerializeField] private SceneReference destinationScene;
        public async void Transition()
        {
            await ScreenTransition.TransitionIn(1f);
            await SceneManager.LoadSceneAsync(destinationScene.BuildIndex);
            await UniTask.WaitForEndOfFrame(); // Wait for all objects to be properly started (prevent lags)
            await ScreenTransition.TransitionOut(1f);
        }
    }
}