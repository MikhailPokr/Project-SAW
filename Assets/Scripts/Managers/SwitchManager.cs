using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectSAW
{
    

    public class SwitchManager : MonoBehaviour
    {
        private enum Switches
        {
            Control,
            Visibility,
            Language,
            Screen
        }
        [SerializeField] Switches _switchType;
        [SerializeField] Text[] _switches;
        [SerializeField] Text[] _extra;
        [Space]
        [SerializeField] Color _defaultColor;
        [SerializeField] Color _selectedColor;

        private void Start()
        {
            switch (_switchType)
            {
                case Switches.Control:
                    GetComponent<ControlManager>().ControlChange.AddListener(OnControlChange);
                    OnControlChange(ControlManager.CurrentInputMode);
                    break;
                case Switches.Visibility:
                    GetComponent<RecordsManager>().VisibilityChange.AddListener(OnVisibilityChange);
                    OnVisibilityChange(RecordsManager.CurrentVisibility);
                    break;
                case Switches.Language:
                    GetComponent<LanguageManager>().LanguageSwitch.AddListener(OnLanguageChange);
                    OnLanguageChange(LanguageManager.CurrentLanguage);
                    break;
                case Switches.Screen:
                    GetComponent<ScreenManager>().ScreenModeChanged.AddListener(OnScreenModeChanged);
                    OnScreenModeChanged(ScreenManager.CurrentScreenMode);
                    break;
            }

            
        }

        private void OnDestroy()
        {
            switch (_switchType)
            {
                case Switches.Control:
                    GetComponent<ControlManager>().ControlChange.RemoveListener(OnControlChange);
                    break;
                case Switches.Visibility:
                    GetComponent<RecordsManager>().VisibilityChange.RemoveListener(OnVisibilityChange);
                    break;
                case Switches.Language:
                    GetComponent<LanguageManager>().LanguageSwitch.RemoveListener(OnLanguageChange);
                    break;
            }
        }
        private void OnControlChange(InputMode mode)
        {
            for (int i = 0; i < _switches.Length; i++)
            {
                if (i == (int)mode)
                {
                    _switches[i].color = _selectedColor;
                }
                else
                {
                    _switches[i].color = _defaultColor;
                }
            }
            switch (mode)
            {
                case InputMode.Keyboard:
                    _extra[0].text = "D";
                    _extra[1].text = "A";
                    _extra[2].text = "Space";
                    _extra[3].text = "Q";
                    _extra[4].text = "Shift";
                    _extra[5].text = "Esc";
                    break;
                case InputMode.Gamepad:
                    _extra[0].text = ">";
                    _extra[1].text = "<";
                    _extra[2].text = "RT";
                    _extra[3].text = "LS/LB";
                    _extra[4].text = "LT";
                    _extra[5].text = "Start";
                    break;
            }
            
        }

        private void OnVisibilityChange(VisibilityMode mode)
        {
            for (int i = 0; i < _switches.Length; i++)
            {
                if (i == (int)mode)
                {
                    _switches[i].color = _selectedColor;
                }
                else
                {
                    _switches[i].color = _defaultColor;
                }
                
            }
        }

        private void OnLanguageChange(Language language)
        {
            for (int i = 0; i < _switches.Length; i++)
            {
                if (i == (int)language)
                {
                    _switches[i].color = _selectedColor;
                }
                else
                {
                    _switches[i].color = _defaultColor;
                }   
            }
        }

        private void OnScreenModeChanged(ScreenMode mode)
        {
            for (int i = 0; i < _switches.Length; i++)
            {
                if (i == (int)mode)
                {
                    _switches[i].color = _selectedColor;
                }
                else
                {
                    _switches[i].color = _defaultColor;
                }
            }
        }
    }
}