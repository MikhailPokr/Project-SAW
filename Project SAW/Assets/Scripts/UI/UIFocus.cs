using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSAW
{
    public class UIFocus : MonoBehaviour
    {
        [SerializeField] private GameObject _background;
        [SerializeField] private GameObject _focusUI;
        [SerializeField] private float _timeScale;

        private static bool _gameInFocusMode = false;
        public static bool GameInFocusMode => _gameInFocusMode;
        public void Switch()
        {
            bool active = !_gameInFocusMode;
            Time.timeScale = active ? _timeScale : 1;
            _background.SetActive(active);
            _focusUI.SetActive(active);
            _gameInFocusMode = active;
        }
    }
}