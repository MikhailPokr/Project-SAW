using System.IO;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

namespace ProjectSAW
{
    public enum VisibilityMode
    {
        NoDisplay,
        NoTriggerDisplay,
        TriggerDisplay
    }

    public class RecordsManager : MonoBehaviour
    {
        private static VisibilityMode _currentVisibility;
        public static VisibilityMode CurrentVisibility => _currentVisibility;

        private Dictionary<int, bool> _allRecords;

        [SerializeField] private List<Record> _records;

        [HideInInspector] public UnityEvent<VisibilityMode> VisibilityChange;

        private void Awake()
        {
            _allRecords = new Dictionary<int, bool>();
            _currentVisibility = (VisibilityMode)PlayerPrefs.GetInt("RecordsVisibility", 2);

            string records = PlayerPrefs.GetString("Records", "");
            if (records.Length > 0)
            {
                string[] recordData = records.Split(",");
                for (int i = 0; i < recordData.Length; i++)
                {
                    string[] record = recordData[i].Split(":");
                    _allRecords.Add(int.Parse(record[0]), record[1] == "1");
                }
            }

            foreach (Record record in _records)
            {
                if (!_allRecords.TryGetValue(record.Number, out bool active))
                {
                    _allRecords.Add(record.Number, record.Active);
                }
                else
                {
                    record.Active = active;
                }
            }
        }
        public void SetVisibility(int visibility)
        {
            if (_currentVisibility == (VisibilityMode)visibility)
                return;
            _currentVisibility = (VisibilityMode)visibility;
            PlayerPrefs.SetInt("RecordsVisibility", visibility);
            PlayerPrefs.Save();

            VisibilityChange.Invoke((VisibilityMode)visibility);
        }

        public void SwitchActive(Record record)
        {
            _allRecords[record.Number] = record.Active;
        }

        public void SaveAndClear()
        {
            string data = "";
            for (int i = 0; i < _allRecords.Count; i++)
            {
                data += $"{i}:{(_allRecords[i] ? 1 : 0)}";
                if (i + 1 != _allRecords.Count)
                    data += ",";
            }
            PlayerPrefs.SetString("Records", data);
            PlayerPrefs.Save();
            _allRecords.Clear();
        }
    }
}