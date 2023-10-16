using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScene : MonoBehaviour
{
    private Animator _anim;
    [SerializeField] private Button _singleBtn;
    [SerializeField] private Button _multiBnt;
    [SerializeField] private GameObject _loginPanel;

    private void Awake()
    {
        Debug.Log(UIManager.Instance);
        _anim = GetComponent<Animator>();
    }

    private void Start()
    {
        _anim.SetBool("Run", true);
        _singleBtn.onClick.AddListener(() => UIManager.Instance.OpenUI<UIStagePanel>());

        if(PlayerPrefs.GetString("UserID") == "")
        {
            _multiBnt.onClick.AddListener(() => _loginPanel.SetActive(true));
        }
        else
        {
            //로그인 후

        }
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.right * 3 * Time.deltaTime);
    }

}
