using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectSAW
{
    public class Controller : MonoBehaviour
    {
        [SerializeField] InputActionAsset _actions;
        [SerializeField] PlayerMove _player;
        [SerializeField] UIFocus _focus;
        [SerializeField] UIPause _pause;


        private void Start()
        {
            InputAction motionAction = _actions.FindActionMap(Enum.GetName(typeof(InputMode), ControlManager.CurrentInputMode)).FindAction("Motion");
            motionAction.performed += OnMotion;
            motionAction.canceled += OnMotion;

            InputAction jumpAction = _actions.FindActionMap(Enum.GetName(typeof(InputMode), ControlManager.CurrentInputMode)).FindAction("Jump");
            jumpAction.performed += OnJump;
            jumpAction.canceled += OnJump;

            InputAction focusAction = _actions.FindActionMap(Enum.GetName(typeof(InputMode), ControlManager.CurrentInputMode)).FindAction("Focus");
            focusAction.started += OnFocus;
            focusAction.canceled += OnFocus;

            InputAction pauseAction = _actions.FindActionMap(Enum.GetName(typeof(InputMode), ControlManager.CurrentInputMode)).FindAction("Pause");
            pauseAction.started += OnPause;
            pauseAction.canceled += OnPause;

            InputAction dodgeAction = _actions.FindActionMap(Enum.GetName(typeof(InputMode), ControlManager.CurrentInputMode)).FindAction("Dodge");
            dodgeAction.started += OnDodge;
            dodgeAction.canceled += OnDodge;

            _actions.Enable();
        }
        private void OnDestroy()
        {
            _actions.Disable();

            InputAction motionAction = _actions.FindActionMap(Enum.GetName(typeof(InputMode), ControlManager.CurrentInputMode)).FindAction("Motion");
            motionAction.performed -= OnMotion;
            motionAction.canceled -= OnMotion;

            InputAction jumpAction = _actions.FindActionMap(Enum.GetName(typeof(InputMode), ControlManager.CurrentInputMode)).FindAction("Jump");
            jumpAction.performed -= OnJump;
            jumpAction.canceled -= OnJump;

            InputAction focusAction = _actions.FindActionMap(Enum.GetName(typeof(InputMode), ControlManager.CurrentInputMode)).FindAction("Focus");
            focusAction.started -= OnFocus;
            focusAction.canceled -= OnFocus;

            InputAction pauseAction = _actions.FindActionMap(Enum.GetName(typeof(InputMode), ControlManager.CurrentInputMode)).FindAction("Pause");
            pauseAction.started -= OnPause;
            pauseAction.canceled -= OnPause;

            InputAction dodgeAction = _actions.FindActionMap(Enum.GetName(typeof(InputMode), ControlManager.CurrentInputMode)).FindAction("Dodge");
            dodgeAction.started -= OnDodge;
            dodgeAction.canceled -= OnDodge;
        }

        private void OnMotion(InputAction.CallbackContext context)
        {
            if (DeathManager.NowDeathAnimation && !context.canceled)
                return;
            _player.Direction = context.ReadValue<float>();
        }

        private void OnJump(InputAction.CallbackContext context)
        {
            if (DeathManager.NowDeathAnimation && !context.canceled)
                return;
            if (context.canceled)
                _player.Jump = false;
            else
                _player.Jump = true;
        }

        private void OnFocus(InputAction.CallbackContext context)
        {
            if (DeathManager.NowDeathAnimation && !context.canceled)
                return;
            if (context.started)
            {
                if (UIPause.GameOnPause)
                    return;
                _focus.Switch();
            }
        }

        private void OnPause(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                if (UIFocus.GameInFocusMode)
                    _focus.Switch();
                _pause.Switch();
            }
        }

        private void OnDodge(InputAction.CallbackContext context)
        {
            if (DeathManager.NowDeathAnimation)
                return;
            _player.Dodge();
        }
    }
}