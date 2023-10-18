using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScene : MonoBehaviour
{
    private Animator _anim;
    [SerializeField] private Button _singleBtn;

    private void Awake()
    {
        Debug.Log(UIManager.Instance);
        _anim = GetComponent<Animator>();
        PlayerPrefs.SetString("UserID", "");
    }

    private void Start()
    {
        _anim.SetBool("Run", true);
        _singleBtn.onClick.AddListener(() => UIManager.Instance.OpenUI<UIStagePanel>());
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.right * 3 * Time.deltaTime);
    }


}
