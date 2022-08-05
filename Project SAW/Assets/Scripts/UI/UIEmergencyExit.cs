using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSAW
{
    public class UIEmergencyExit : MonoBehaviour
    {
        [SerializeField] private RectTransform _canvas;
        [SerializeField] private UIWindowPopUp _windowPrefab;
        [SerializeField] private GameObject _extraIcons;

        [SerializeField] private float _windowWidth;
        [SerializeField] private float _windowHeight;

        [SerializeField] private float _minX;
        [SerializeField] private float _maxX;

        [SerializeField] private float _minY;
        [SerializeField] private float _maxY;

        private UIWindowPopUp _currentWindow;

        public void WindowCreate()
        {
            _currentWindow = _windowPrefab.Create(_canvas, Random.Range(_minX, _maxX), Random.Range(_minY, _maxY), _windowWidth, _windowHeight);
            _currentWindow.WindowClosed.AddListener(OnWindowClosed);
            Instantiate(_extraIcons, _currentWindow.transform);
        }
        private void OnWindowClosed(GameObject window)
        {
            WindowCreate();
        }
    }
}