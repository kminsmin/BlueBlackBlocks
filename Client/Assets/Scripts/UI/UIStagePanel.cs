using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStagePanel : MonoBehaviour
{
    [SerializeField] private Transform _content;
    [SerializeField] private GameObject _stageSlot;

    private int _maxLevel;
    private GameObject[] _stageSlotList;

    private void Start()
    {
        _maxLevel = 2;//юс╫ц
        SetStage();
    }

    private void OnEnable()
    {
        UpdateStage();
    }

    private void SetStage()
    {
        _stageSlotList = new GameObject[_maxLevel + 1];
        for (int i = 1; i <= _maxLevel; i++)
        {
            GameObject instantiate = Instantiate(_stageSlot, Vector3.zero, Quaternion.identity);
            instantiate.transform.SetParent(_content);
            instantiate.GetComponent<UIStageSlot>().SetStage(i);
            _stageSlotList[i] = instantiate;
        }
    }

    private void UpdateStage()
    {
        if (_stageSlotList == null)
            return;

        for (int i = 1; i <= _maxLevel; i++)
        {
            _stageSlotList[i].GetComponent<UIStageSlot>().SetStage(i);
        }
    }
}
