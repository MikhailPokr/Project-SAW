using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace ProjectSAW
{
    public class Record : MonoBehaviour
    {
        [SerializeField] private int _number;
        [SerializeField, Multiline] private string _textEnglish;
        [SerializeField, Multiline] private string _textRussian;
        [SerializeField] private float _windowWidth;
        [SerializeField] private float _windowHeight;
        [Space]
        [SerializeField] private RecordsManager _recordManager;
        [SerializeField] private UIPause _pause;
        [SerializeField] private LanguageManager _languageManager;
        [Space]
        [SerializeField] private RectTransform _window;
        [SerializeField] private Text _text;
        public int Number => _number;

        public bool Active = true;


        private void Start()
        {
            _recordManager.VisibilityChange.AddListener(OnVisibilityChange);
            OnVisibilityChange(RecordsManager.CurrentVisibility);
        }
        private void OnDestroy()
        {
            _recordManager.VisibilityChange.RemoveListener(OnVisibilityChange);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (Active || RecordsManager.CurrentVisibility == VisibilityMode.TriggerDisplay)
            {
                _pause.Switch();
                _window.sizeDelta = new Vector2(_windowWidth, _windowHeight);
                _window.gameObject.SetActive(true);
                switch (LanguageManager.CurrentLanguage)
                {
                    case Language.English:
                        _text.text = _textEnglish;
                        break;
                    case Language.Russian:
                        _text.text = _textRussian;
                        break;
                }
                _text.gameObject.SetActive(true);
            }
            Active = false;
            OnVisibilityChange(RecordsManager.CurrentVisibility);
            _recordManager.SwitchActive(this);
        }

        private void OnVisibilityChange(VisibilityMode mode)
        {
            if (!Active)
            {
                SpriteRenderer sprite = GetComponent<SpriteRenderer>(); 
                if (mode == VisibilityMode.NoTriggerDisplay)
                {
                    sprite.color = new Color(1, 1, 1, 0.35f);
                }
                else if (mode == VisibilityMode.NoDisplay)
                {
                    sprite.color = new Color(1, 1, 1, 0f);
                }
            }
        }

    }
}