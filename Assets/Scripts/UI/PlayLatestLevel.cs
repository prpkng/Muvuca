using Cysharp.Threading.Tasks;
using Muvuca.Core;
using Muvuca.Effects;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayLatestLevel : MonoBehaviour
{
    public async void Play()
    {
        await ScreenTransition.TransitionIn(1f);
        await SceneManager.LoadSceneAsync(GameManager.PlayerProgress);
        await UniTask.WaitForEndOfFrame(); // Wait for all objects to be properly started (prevent lags)
        await ScreenTransition.TransitionOut(1f);
    }
}