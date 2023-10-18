using Photon.Pun;
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

    [SerializeField] private Button _multiBnt;

    private void Awake()
    {
        _signUp.onClick.AddListener(SignUP);
        _signIn.onClick.AddListener(SingIn);
        _multiBnt.onClick.AddListener(() => gameObject.SetActive(true));
        gameObject.SetActive(false);
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
            popup.SetPopup("로그인 실패", "아이디를 입력하세요.", null);
        }
        else if(_pw.text == "")
        {
            UIPopUp popup = UIManager.Instance.OpenUI<UIPopUp>();
            popup.SetPopup("로그인 실패", "비밀번호를 입력하세요.", null);
        }
        else
        {
            // 로그인 테스트
            PlayerPrefs.SetString("UserID", _id.text);

            PhotonNetwork.LocalPlayer.NickName = PlayerPrefs.GetString("UserID");
            PhotonNetwork.ConnectUsingSettings();

            UIManager.Instance.OpenUI<UILobby>();
            _multiBnt.onClick.RemoveAllListeners();
            _multiBnt.onClick.AddListener(() => UIManager.Instance.OpenUI<UILobby>());
            gameObject.SetActive(false);
        }
    }
}
