using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


namespace ProjectSAW
{
    public class DeathManager : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _playerSprite;
        [SerializeField] private Rigidbody2D _playerRigidbody;
        [SerializeField] private PlayerMove _playerMove;

        [SerializeField] private GameObject[] _respawns;
        private static int _respawnNow = 0;
        public static int RespawnNow => _respawnNow;
        private static int _respawnMax = 0;
        public static int RespawnMax => _respawnMax;
        [SerializeField] private Image _deathFon;
        [SerializeField] private Text _deathText;
        [SerializeField] private UIFocus _focus;
        [SerializeField] private UIPause _pause;

        private static bool _nowDeathAnimation = true;
        public static bool NowDeathAnimation => _nowDeathAnimation;

        private Color _playerColor;

        public UnityEvent MaxRespawnChange;

        private void Awake()
        {
            _respawnNow = PlayerPrefs.GetInt("RespawnNow" + SceneManager.GetSceneName());
            _respawnMax = PlayerPrefs.GetInt("RespawnMax" + SceneManager.GetSceneName());
            _playerColor = _playerSprite.color;
        }
        private void FixedUpdate()
        {
            Color colorDie = new Color(191, 0, 0, _deathFon.color.a - 0.02F);
            _deathFon.color = colorDie;
            _deathText.color = colorDie;
            if (colorDie.a < 0.02F)
            {
                _deathFon.gameObject.SetActive(false);
                _deathText.gameObject.SetActive(false);
            }
            if (_playerSprite.color.maxColorComponent < _playerColor.maxColorComponent)
            {
                float color = _playerSprite.color.maxColorComponent;
                _playerSprite.color = new Color(color + 0.02f, color + 0.02f, color + 0.02f);
            }
            else
            {
                _playerSprite.color = _playerColor;
                _nowDeathAnimation = false;
                _playerRigidbody.isKinematic = false;
            }
        }
        public void Resp(int resp = -1)
        {
            if (UIPause.GameOnPause)
                _pause.Switch();
            _playerMove.SetFullStamina();
            _nowDeathAnimation = true;
            _playerSprite.color = new Color(0, 0, 0, 1);
            _playerRigidbody.isKinematic = true;
            _playerRigidbody.position = _respawns[resp == -1 ? _respawnNow : resp].transform.position;
            _playerRigidbody.rotation = 0;
            _playerRigidbody.angularVelocity = 0;
            _playerRigidbody.velocity = Vector2.zero;

        }
        public void DisplayDeathScreen()
        {
            if (Time.timeScale != 1 && !UIPause.GameOnPause)
                _focus.Switch();
            _deathFon.gameObject.SetActive(true);
            _deathText.gameObject.SetActive(true);
            _deathFon.color = new Color(191, 0, 0, 0.60F);
            _deathText.color = new Color(191, 0, 0, 0.60F);
        }
        public void SetRespawn(int num)
        {
            PlayerPrefs.SetInt("RespawnNow" + SceneManager.GetSceneName(), num);
            _respawnNow = num;
        }
        public void SetRespawnMax(int num)
        {
            PlayerPrefs.SetInt("RespawnMax" + SceneManager.GetSceneName(), num);
            _respawnMax = num;
            MaxRespawnChange.Invoke();
        }
    }
}
