using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectSAW
{
    public enum ScreenMode
    {
        FullScreen,
        Window
    }

    public class ScreenManager : MonoBehaviour
    {
        private static ScreenMode _currentScreenMode;
        public static ScreenMode CurrentScreenMode => _currentScreenMode;

        public UnityEvent<ScreenMode> ScreenModeChanged;
        void Start()
        {
            _currentScreenMode = (ScreenMode)PlayerPrefs.GetInt("FullScreen", 0);
            SwitchScreenMode((int)_currentScreenMode);
        }

        public void SwitchScreenMode(int mode)
        {
            PlayerPrefs.SetInt("FullScreen", mode);
            _currentScreenMode = (ScreenMode)mode;
            switch (_currentScreenMode)
            { 
                case ScreenMode.FullScreen:
                    Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
                    break;
                case ScreenMode.Window:
                    Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, false);
                    break;
            }
            ScreenModeChanged.Invoke(_currentScreenMode);
        }
    }
}