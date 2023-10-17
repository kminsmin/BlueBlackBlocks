using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITest : MonoBehaviour
{

    [SerializeField] private Button _btn;
    [SerializeField] private Button _btn2;
    [SerializeField] private Button _btn3;
    [SerializeField] private Sprite _sprite;


    private UIItem _uIItem;

    private void Awake()
    {
        Debug.Log(UIManager.Instance);
    }

    void Start()
    {
        _btn.onClick.AddListener(OpenPopUp);
        _btn2.onClick.AddListener(ItemEquip);
        _btn3.onClick.AddListener(UnEquip);
        _uIItem = UIManager.Instance.OpenUI<UIItem>();
    }

    void OpenPopUp()
    {
        UIPopUp popup = UIManager.Instance.OpenUI<UIPopUp>();
        popup.SetPopup("테스트 UI", "테스트 UI,테스트 UI,테스트 UI,테스트 UI,테스트 UI,테스트 UI,테스트 UI,테스트 UI", null);
    }

    void ItemEquip()
    {
        _uIItem.SetImage(_sprite);
    }

    void UnEquip()
    {
        _uIItem.DelImage();
    }

}
