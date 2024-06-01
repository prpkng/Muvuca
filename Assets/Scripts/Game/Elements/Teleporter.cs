using Eflatun.SceneReference;
using Muvuca.Core;
using Muvuca.Systems;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private bool last = false;
    public void Save()
    {
        if (last)
        {
            GameManager.PlayerProgress = 1;
            SaveSystem.Set(SaveSystemKeyNames.PlayerProgress, 1);
            return;
        }

        var current = SceneManager.GetActiveScene().buildIndex + 1;
        if (GameManager.PlayerProgress < current)
        {
            GameManager.PlayerProgress = current;
            SaveSystem.Set(SaveSystemKeyNames.PlayerProgress, current);
        }
    }
}