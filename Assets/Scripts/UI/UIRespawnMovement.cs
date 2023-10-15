using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSAW
{
    public class UIRespawnMovement : MonoBehaviour
    {
        [SerializeField] GameObject[] _respawns;

        [SerializeField] DeathManager _deathManager;

        private void Start()
        {
            _deathManager.MaxRespawnChange.AddListener(OnMaxRespawnChange);
            OnMaxRespawnChange();
        }
        private void OnDestroy()
        {
            _deathManager.MaxRespawnChange.RemoveListener(OnMaxRespawnChange);
        }

        private void OnMaxRespawnChange()
        {
            int maxRespawn = DeathManager.RespawnMax;
            for (int i = 0; i < _respawns.Length; i++)
            {
                if (i <= maxRespawn)
                {
                    _respawns[i].SetActive(true);
                }
                else
                {
                    _respawns[i].SetActive(false);
                }
            }
        }
    }
}