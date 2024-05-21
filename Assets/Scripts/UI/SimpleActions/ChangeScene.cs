namespace Muvuca.UI.SimpleActions
{
    using UnityEngine;
    using Eflatun.SceneReference;
    using UnityEngine.SceneManagement;

    public class ChangeScene : MonoBehaviour
    {

        [SerializeField] private SceneReference sceneReference;
        public void Change()
        {
            SceneManager.LoadScene(sceneReference.BuildIndex);
            Time.timeScale = 1f;
        }
    }
}