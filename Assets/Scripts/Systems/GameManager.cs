using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static bool isMobilePlatform => Instance.isRunningOnMobile || Instance.runningOnMobileOverride;

    [ReadOnly] public bool isRunningOnMobile;
    public bool runningOnMobileOverride;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
    public static void SetupGameManager()
    {
        GameObject gameManager = new("GameManager");
        Instance = gameManager.AddComponent<GameManager>();
        DontDestroyOnLoad(gameManager);


        // Get platform info

        var platform = Application.platform;
        switch (platform)
        {
            case RuntimePlatform.Android:
            case RuntimePlatform.IPhonePlayer:
                Instance.isRunningOnMobile = true;
                Debug.Log("Mobile Player!");
                break;
            case RuntimePlatform.WebGLPlayer:
                Debug.Log("WebGL Player!");
                break;
            default:
                Debug.Log("Desktop Player!");
                break;
        }


    }


}
