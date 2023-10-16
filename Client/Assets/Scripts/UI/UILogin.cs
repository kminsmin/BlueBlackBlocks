using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILogin : MonoBehaviour
{
    [SerializeField] private Button _signUp;
    [SerializeField] private Button _signIn;
    [SerializeField] private TMP_InputField _id;
    [SerializeField] private TMP_InputField _pw;

    private void Start()
    {
        _signUp.onClick.AddListener(SignUP);
        _signIn.onClick.AddListener(SingIn);
    }

    private void SignUP()
    {
        Application.OpenURL("http://naver.com");
    }

    private void SingIn()
    {
        if(_id.text == "")
        {
            UIPopUp popup = UIManager.Instance.OpenUI<UIPopUp>();
            popup.SetPopup("�α��� ����", "���̵� �Է��ϼ���.", null);
        }
        else if(_pw.text == "")
        {
            UIPopUp popup = UIManager.Instance.OpenUI<UIPopUp>();
            popup.SetPopup("�α��� ����", "��й�ȣ�� �Է��ϼ���.", null);
        }
        else
        {
            // �α��� �õ�
        }
    }
}
