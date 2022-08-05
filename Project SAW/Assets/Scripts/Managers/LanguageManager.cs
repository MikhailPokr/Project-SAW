using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectSAW
{
    public enum Language
    {
        English,
        Russian,
    }
    public class LanguageManager : MonoBehaviour
    {
        private static Language _currentLanguage;
        public static Language CurrentLanguage => _currentLanguage;

        [HideInInspector] public UnityEvent<Language> LanguageSwitch;

        private void Awake()
        {
            _currentLanguage = (Language)PlayerPrefs.GetInt("Language", 0);
        }
        public void SetLanguage(int language)
        {
            _currentLanguage = (Language)language;
            PlayerPrefs.SetInt("Language", language);
            PlayerPrefs.Save();
            LanguageSwitch.Invoke((Language)language);
        }
    }
}