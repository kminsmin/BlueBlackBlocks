using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    private bool _isOpen;
    [SerializeField] private Button _openBtn;
    [SerializeField] private Button _closeBtn;
    [SerializeField] private GameObject _optionPanel;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Button _exitBtn;
    [SerializeField] public Slider _bgmSidebar;
    [SerializeField] public Slider _sfxSidebar;

    private void Start()
    {
        _isOpen = false;
        _openBtn.onClick.AddListener(OpenOption);
        _closeBtn.onClick.AddListener(OpenOption);
        _exitBtn.onClick.AddListener(ExitGame);
    }

    private void Update()
    {
        if (!_isOpen)
            return;
        Debug.Log("Option 업데이트\t"+ _bgmSidebar.value + "\t" + _sfxSidebar.value);
        //슬라이더 값을 오디오 매니저로 전달 필요
    }

    private void OpenOption()
    {
        if(_isOpen)
        {
            _optionPanel.SetActive(false);
            _isOpen = false;
        }
        else
        {
            _optionPanel.SetActive(true);
            _isOpen = true;
        }
    }

    private void ExitGame()
    {
        // 게임 종료
    }

    public void UpdateStage(string str)
    {
        // 스테이지 정보 업데이트
        _text.text = str;
    }


}
