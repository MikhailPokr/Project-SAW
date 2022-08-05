using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSAW
{
    enum TriggerType
    {
        SetRespawn,
        LevelEnd
    }

    public class Trigger : MonoBehaviour
    {
        [SerializeField] private int _num = -1;

        [SerializeField] private TriggerType _type;

        [SerializeField] SceneManager _sceneManager;

        [SerializeField] RecordsManager _recordsManager;

        [SerializeField] DeathManager _deathManager;
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerMove move))
            {
                switch (_type)
                {
                    case TriggerType.SetRespawn:
                        int _preNum = PlayerPrefs.GetInt("RespawnNow" + SceneManager.GetSceneName());
                        _deathManager.SetRespawn(_num);

                        int maxRespawn = PlayerPrefs.GetInt("RespawnMax" + SceneManager.GetSceneName());
                        if (_num > maxRespawn)
                        {
                            _deathManager.SetRespawnMax(_num);
                        }
                        break;
                    case TriggerType.LevelEnd:
                        _recordsManager.SaveAndClear();
                        _sceneManager.LoadNextLevel();
                        break;
                }
            }
        }
    }
}