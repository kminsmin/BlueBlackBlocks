using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStageSlot : MonoBehaviour
{
    [SerializeField] private Image _lock;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Button _btn;
    [SerializeField] private int _level;

    public void SetStage(int level)
    {
        _level = level;
        _text.text = _level.ToString();

        // 임시
        if (_level <= 1)
        {
            OpenStage();
        }
        else
        {
            CloseStage();
        }
    }

    private void OpenStage()
    {
        _lock.gameObject.SetActive(false);
        _btn.onClick.AddListener(StartStage);
    }

    private void CloseStage()
    {
        _lock.gameObject.SetActive(true);
        _btn.onClick.RemoveListener(StartStage);
    }

    void StartStage()
    {
        //게임 시작
    }

}
