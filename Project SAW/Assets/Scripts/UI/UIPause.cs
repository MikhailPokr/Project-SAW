using UnityEngine;

namespace ProjectSAW
{
    public class UIPause : MonoBehaviour
    {
        [SerializeField] private GameObject _background;
        [SerializeField] private GameObject _pauseMenu;
        [SerializeField] private GameObject _textWindow;
        [SerializeField] private GameObject _respawnWindow;

        private static bool _gameOnPause = false;
        public static bool GameOnPause => _gameOnPause;

        public void Start()
        {
            _background.SetActive(false);
            _pauseMenu.SetActive(false);
            _gameOnPause = false;
            Time.timeScale = 1;
        }

        public void Switch()
        {
            bool active = !_gameOnPause;
            Time.timeScale = active ? 0 : 1;
            _background.SetActive(active);
            _pauseMenu.SetActive(active);
            _textWindow.SetActive(false);
            _respawnWindow.SetActive(active);
            _gameOnPause = active;
            UIPointer.HidePoinder();
        }
    }
}
