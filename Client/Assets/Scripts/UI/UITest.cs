using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITest : MonoBehaviour
{

    [SerializeField] private Button _btn;

    void Start()
    {
        Debug.Log(UIManager.Instance);
        _btn.onClick.AddListener(OpenPopUp);
    }

    void OpenPopUp()
    {
        UIPopUp popup = UIManager.Instance.OpenUI<UIPopUp>();
        popup.SetPopup("테스트 UI", "테스트 UI,테스트 UI,테스트 UI,테스트 UI,테스트 UI,테스트 UI,테스트 UI,테스트 UI", null);
    }

}
