namespace Muvuca.UI
{
    using Cysharp.Threading.Tasks;
    using Muvuca.Core;
    using Muvuca.Effects;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    public class RestartLevel : MonoBehaviour
    {
        public async void Restart()
        {
            await ScreenTransition.TransitionIn(1f);
            await SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
            await UniTask.WaitForEndOfFrame(); // Wait for all objects to be properly started (prevent lags)
            await ScreenTransition.TransitionOut(1f);
        }
    }
}