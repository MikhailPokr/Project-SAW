using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectSAW
{
    public class UIStaminaBattery : MonoBehaviour
    {
        [SerializeField] private PlayerMove _player;
        [SerializeField] private Text _text;
        private void FixedUpdate()
        {
            int fullS = Mathf.RoundToInt(_player.Stamina * 10 / _player.MaxStamina);
            if (fullS <= 0 || fullS > 10)
                _text.text = "~" + new string((fullS <= 0 ? '▯' : '▮'), 10) + "~";
            else
                _text.text = "~" + new string('▮', fullS) + new string('▯', 10 - fullS) + "~";
        }
    }
}