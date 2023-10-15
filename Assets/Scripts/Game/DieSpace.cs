using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSAW
{
    public class DieSpace : MonoBehaviour
    {
        [SerializeField] private DeathManager _death;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerMove move))
            {
                _death.DisplayDeathScreen();
                _death.Resp();
            }
        }
    }
}
