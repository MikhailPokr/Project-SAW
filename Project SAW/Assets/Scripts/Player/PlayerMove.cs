using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSAW
{
    public class PlayerMove : MonoBehaviour
    {
        [Header("Stats")]
        [SerializeField] private float _maxStamina;
        public float MaxStamina => _maxStamina;
        [SerializeField] private float _stamina;
        public float Stamina => _stamina;
        [SerializeField] private int _staminaMove;
        [SerializeField] private int _staminaJump;
        [SerializeField] private int _staminaBack;
        [SerializeField] private int _staminaBackRemainder;
        [SerializeField] private int _staminaRecovery;
        [Space]
        [SerializeField] private float _torque;
        [SerializeField] private float _horisontalPower;
        [Space]
        [SerializeField] private float _jumpTorque;
        [SerializeField] private float _jumpPower;
        [Space]
        [SerializeField] private float _backLeft;
        [SerializeField] private float _backUp;
        [Space]
        [SerializeField] private float _angularVelocityLimit;
        [Space]
        [SerializeField] private int _modifierInFocus;
        [Space(2)]
        [Header("Other")]
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite[] _sprites;
        [SerializeField] private Rigidbody2D _rigidbody;

        [HideInInspector] public float Direction;
        [HideInInspector] public bool Jump;


        private void Start()
        {
            _stamina = _maxStamina;
        }
        private void FixedUpdate()
        {
            if (_stamina < _maxStamina - _staminaRecovery)
                _stamina += _staminaRecovery * Time.timeScale;

            _spriteRenderer.sprite = _sprites[Mathf.RoundToInt(_stamina / (_maxStamina / (_sprites.Length - 1)))];

            if (Mathf.Abs(_rigidbody.angularVelocity) > Mathf.Abs(_angularVelocityLimit))
            {
                _rigidbody.angularVelocity = _angularVelocityLimit * (_rigidbody.angularVelocity < 0 ? -1 : 1);
            }

            if (!UIPause.GameOnPause)
            {
                int modifier = UIFocus.GameInFocusMode ? _modifierInFocus : 1;
                float powerT = _torque * modifier;
                float powerF = _horisontalPower * modifier;
                if (Direction != 0 && _stamina >= _staminaMove * modifier)
                {
                    _stamina -= _staminaMove * modifier;
                    _rigidbody.AddTorque(-Direction * powerT, ForceMode2D.Impulse);
                    _rigidbody.AddForce(Direction * powerF * new Vector2(1, 0), ForceMode2D.Impulse);
                }

                powerT = -_jumpTorque * modifier;
                powerF = -_jumpPower * _rigidbody.angularVelocity * modifier;
                if (Jump && _stamina >= _staminaJump * modifier)
                {
                    _stamina -= _staminaJump * modifier;
                    _rigidbody.AddTorque(powerT, ForceMode2D.Impulse);
                    _rigidbody.AddForce(Vector2.up * powerF, ForceMode2D.Impulse);
                }
            }

            if (_stamina < 0)
                _stamina = 0;
            if (_stamina > _maxStamina)
                _stamina = _maxStamina;
        }
        public void Dodge()
        {
            if (_stamina < _staminaBack)
                return;
            _stamina -= _staminaBack;
            _rigidbody.AddForce(Vector2.left * _backLeft, ForceMode2D.Impulse);
            _rigidbody.AddForce(Vector2.up * _backUp, ForceMode2D.Impulse);
            _stamina = _staminaBackRemainder;
        }
        public void SetFullStamina()
        {
            _stamina = _maxStamina;
        }
    }
}