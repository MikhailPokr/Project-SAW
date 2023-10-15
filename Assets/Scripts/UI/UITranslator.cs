using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectSAW
{
    public class UITranslator : MonoBehaviour
    {
        [SerializeField] private LanguageManager _languageManager;
        [SerializeField] private Text _text;

        [SerializeField, Multiline] private string _russianTranslation;
        //при необходимости можно вписать другие языки.

        private string _englishTranslation;

        private void Awake()
        {
            _englishTranslation = _text.text;
            _languageManager.LanguageSwitch.AddListener(OnLanguageSwitch);
        }

        //думаю тут нужны оба, чтобы менеджер успел прогрузиться и при включении язык менялся.
        private void OnEnable()
        {
            OnLanguageSwitch(LanguageManager.CurrentLanguage);
        }
        private void Start()
        {
            OnLanguageSwitch(LanguageManager.CurrentLanguage);
        }

        private void OnDestroy()
        {
            _languageManager.LanguageSwitch.RemoveListener(OnLanguageSwitch);
        }
        private void OnLanguageSwitch(Language language)
        {
            switch(language)
            {
                case Language.English:
                    _text.text = _englishTranslation;
                    break;
                case Language.Russian:
                    _text.text = _russianTranslation;
                    break;
            }
        }
    }
}