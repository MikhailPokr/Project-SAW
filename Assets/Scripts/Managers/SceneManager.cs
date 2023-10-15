using UnityEngine;
using Scene = UnityEngine.SceneManagement.SceneManager;

namespace ProjectSAW
{
    public class SceneManager : MonoBehaviour
    {
        public void LoadLevel(int levelIndex)
        {
            Scene.LoadScene(levelIndex);
        }
        public void RestartLevel()
        {
            Scene.LoadScene(Scene.GetActiveScene().buildIndex);
        }
        public static string GetSceneName()
        {
            return Scene.GetActiveScene().name;
        }
        public void LoadNextLevel()
        {
            Scene.LoadScene(Scene.GetActiveScene().buildIndex+1);
        }

        public void Exit()
        {
            Application.Quit();
        }

        public void ClearPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
            RestartLevel();
        }
    }
}