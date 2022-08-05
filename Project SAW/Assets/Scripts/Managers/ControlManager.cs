using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace ProjectSAW
{
    public enum InputMode
    {
        Keyboard,
        Gamepad
    }
    public enum Button
    {
        MoveRight,
        MoveLeft,
        Jump,
        Focus,
        Dodge,
        Pause
    }
    public class ControlManager : MonoBehaviour
    {
        [SerializeField] private InputActionAsset _actions;
        [SerializeField] static private InputMode _currentInputMode;
        public static InputMode CurrentInputMode => _currentInputMode;

        public UnityEvent<InputMode> ControlChange;

        private void Start()
        {
            _currentInputMode = (InputMode)PlayerPrefs.GetInt("InputMode", 0);
        }
        public void ChangeInputMode(int mode)
        {
            if ((InputMode)mode == _currentInputMode)
                return;
            _currentInputMode = (InputMode)mode;
            PlayerPrefs.SetInt("InputMode", mode);
            PlayerPrefs.Save();

            ControlChange.Invoke((InputMode)mode);
        }
    }
}