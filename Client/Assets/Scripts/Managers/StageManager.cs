using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviourPun
{
    public static StageManager Instance;
    public event Action OnBlueDeath;
    public event Action OnBlackDeath;
    public event Action OnGameRestart;
    public event Action OnGameEnd;
    public event Action OnGameClear;
    public int CurrentCheckPointIndex { get; private set; } = 0;
    public int CurrentItemsCollected { get; private set; } = 0;
    public UIItem UIItem { get; private set; }
    public UIPopUp UIPopUp { get; private set; }
    private GameObject _bluePlayer;
    private GameObject _blackPlayer;
    private Rigidbody2D _playerRigidBody;
    private PhotonView _photonView;
    [SerializeField] private TilemapCollider2D[] _terrainColliders;
    [SerializeField] public Transform[] CheckPoints;
    [SerializeField] private Sprite[] _itemSprites;
    [SerializeField] private float _respawnInterval = 3f; 

    private void Awake()
    {
        Time.timeScale = 1.0f;
        CurrentCheckPointIndex = 0;
        CurrentItemsCollected = 0;
        Instance = this;
        UIItem = UIManager.Instance.OpenUI<UIItem>();
        StartGame();
    }
    void Start()
    {
        UIItem.DelImage();
        OnBlueDeath += () => Invoke("RespawnBlue", _respawnInterval);
        OnBlackDeath += () => Invoke("RespawnBlack", _respawnInterval);
        OnGameRestart += () => PhotonNetwork.LoadLevel("GameScene");
        OnGameEnd += () => PhotonNetwork.LoadLevel("StartScene");
    }
    
    
    private IEnumerator CheckJump(WaitForSeconds checkInterval)
    {
        while(true)
        {
            if (_playerRigidBody != null)
            {
                if (_playerRigidBody.velocity.y > 0)
                {
                    foreach(var terrain in _terrainColliders)
                    {
                        terrain.enabled = false;
                    }
                }
                else
                {
                    foreach(var terrain in _terrainColliders)
                    { terrain.enabled = true; }
                }
            }
            yield return checkInterval;

        } 
    }

    private void StartGame()
    {
        int idx = PhotonNetwork.LocalPlayer.ActorNumber;
        GameObject prefab = Resources.Load<GameObject>("Player");
        if (idx == 1)
        {
            prefab.tag = "Blue";
            _bluePlayer = PhotonNetwork.Instantiate(prefab.name, new Vector3(-3.63f, 0.46f, 0), Quaternion.identity);
            photonView.RPC("SetBluePlayer", RpcTarget.All, _bluePlayer);
            _playerRigidBody = _bluePlayer.GetComponent<Rigidbody2D>();
        }
        else if (idx == 2)
        {
            prefab.tag = "Black";
            _blackPlayer = PhotonNetwork.Instantiate(prefab.name, new Vector3(-7.63f, 0.46f, 0), Quaternion.identity);
            _blackPlayer.GetComponent<SpriteRenderer>().color = new Color(77f/255f, 41f/255f, 46f / 255f, 1f);
            photonView.RPC("SetBlackPlayer", RpcTarget.All, _blackPlayer);
            _playerRigidBody = _blackPlayer.GetComponent<Rigidbody2D>();
        }
        photonView.RPC("InvokeJumpCheck", RpcTarget.All);
    }

    [PunRPC]
    private void InvokeJumpCheck()
    {
        if (_playerRigidBody != null)
        {
            int idx = PhotonNetwork.LocalPlayer.ActorNumber;
            if (idx == 1)
            {
                _bluePlayer = GameObject.FindGameObjectWithTag("Blue");
                _playerRigidBody = _bluePlayer.GetComponent<Rigidbody2D>();
            }
            else if (idx == 2)
            {
                _blackPlayer = GameObject.FindGameObjectWithTag("Black");
                _blackPlayer.GetComponent<SpriteRenderer>().color = new Color(77f / 255f, 41f / 255f, 46f / 255f, 1f);
                _playerRigidBody = _blackPlayer.GetComponent<Rigidbody2D>();
            }
        }
        StartCoroutine(CheckJump(new WaitForSeconds(.05f)));
    }
    [PunRPC]
    private void RespawnBlue()
    {
        _bluePlayer.transform.position = CheckPoints[CurrentCheckPointIndex].position;
        _bluePlayer.gameObject.SetActive(true);
    }

    [PunRPC]
    private void RespawnBlack()
    {
        _blackPlayer.transform.position = CheckPoints[CurrentCheckPointIndex].position;
        _blackPlayer.GetComponent<SpriteRenderer>().color = new Color(77f / 255f, 41f / 255f, 46f / 255f, 1f);
        _blackPlayer.gameObject.SetActive(true);
    }
    //---------------------------------- 아래는 전부 서버용

    [PunRPC]
    private void SetBluePlayer(GameObject blue)
    {
        this._bluePlayer = blue;
        RespawnBlue();
    }

    [PunRPC]
    private void SetBlackPlayer(GameObject black)
    {
        this._blackPlayer = black;
        RespawnBlack();
    }

    [PunRPC]
    public void CallBlueDeathEvent()
    {
        photonView.RPC("RespawnBlue", RpcTarget.All);
        //_bluePlayer.SetActive(false);
        if (!_blackPlayer.activeSelf)
        {
            Time.timeScale = 0f;
            UIPopUp = UIManager.Instance.OpenUI<UIPopUp>();
            UIPopUp.SetPopup("게임 오버", "다시 하시겠습니까?", OnGameRestart, OnGameEnd);
        }
        //else OnBlueDeath?.Invoke();
    }

    [PunRPC]
    public void CallBlackDeathEvent()
    {
        photonView.RPC("RespawnBlack", RpcTarget.All);
        //_blackPlayer.SetActive(false);
        if (!_bluePlayer.activeSelf)
        {
            Time.timeScale = 0f;
            UIPopUp = UIManager.Instance.OpenUI<UIPopUp>();
            UIPopUp.SetPopup("게임 오버", "다시 하시겠습니까?", OnGameRestart, OnGameEnd);
        }
        //else OnBlackDeath?.Invoke();
    }

    [PunRPC]
    public void CallGameClearEvent()
    {
        Time.timeScale = 0f;
        UIPopUp = UIManager.Instance.OpenUI<UIPopUp>();
        UIPopUp.SetPopup("게임 클리어!", "다시 하시겠습니까?", OnGameRestart, OnGameEnd);
        OnGameClear?.Invoke();
    }

    [PunRPC]
    public void SetCheckPoint(int index)
    {
        CurrentCheckPointIndex = index;
    }

    [PunRPC]
    public void CollectItem()
    {
        CurrentItemsCollected++;
        UIItem.SetImage(_itemSprites[CurrentItemsCollected - 1]);
    }
}
