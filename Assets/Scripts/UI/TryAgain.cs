using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Muvuca.Effects;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TryAgain : MonoBehaviour
{
    public static int LastPlayerLevel;

    public async void Replay()
    {
        await ScreenTransition.TransitionIn(1f);
        await SceneManager.LoadSceneAsync(LastPlayerLevel);
        await UniTask.WaitForEndOfFrame(); // Wait for all objects to be properly started (prevent lags)
        await ScreenTransition.TransitionOut(1f);
    }
}