using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviourPun, IPunObservable
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
    }
    void Start()
    {
        UIItem.DelImage();
        photonView.RPC("StartGame", RpcTarget.All);
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

    [PunRPC]
    private void StartGame()
    {
        int idx = PhotonNetwork.LocalPlayer.ActorNumber;
        GameObject prefab = Resources.Load<GameObject>("Player");
        if (idx == 1)
        {
            prefab.tag = "Blue";
            prefab.GetComponent<SpriteRenderer>().color = new Color(77, 41, 46, 255);
            PhotonNetwork.Instantiate(prefab.name, new Vector3(-3.63f, 0.46f, 0), Quaternion.identity);
            _bluePlayer = GameObject.FindGameObjectWithTag("Blue");
            _playerRigidBody = _bluePlayer.GetComponent<Rigidbody2D>();
        }
        else
        {
            prefab.tag = "Black";
            prefab.GetComponent<SpriteRenderer>().color = new Color(77, 41, 46, 255);
            PhotonNetwork.Instantiate(prefab.name, new Vector3(-3.63f, 0.46f, 0), Quaternion.identity);
            _blackPlayer = GameObject.FindGameObjectWithTag("Black");
            _playerRigidBody = _blackPlayer.GetComponent<Rigidbody2D>();
        }

        StartCoroutine(CheckJump(new WaitForSeconds(.05f)));
    }

    private void RespawnBlue()
    {
        _bluePlayer.transform.position = CheckPoints[CurrentCheckPointIndex].position;
        _bluePlayer.gameObject.SetActive(true);
    }


    private void RespawnBlack()
    {
        _blackPlayer.transform.position = CheckPoints[CurrentCheckPointIndex].position;
        _blackPlayer.gameObject.SetActive(true);
    }
    //---------------------------------- 아래는 전부 서버용

    [PunRPC]
    public void CallBlueDeathEvent()
    {
        _bluePlayer.SetActive(false);
        if (!_blackPlayer.activeSelf)
        {
            Time.timeScale = 0f;
            UIPopUp = UIManager.Instance.OpenUI<UIPopUp>();
            UIPopUp.SetPopup("게임 오버", "다시 하시겠습니까?", OnGameRestart, OnGameEnd);
        }
        else OnBlueDeath?.Invoke();
    }

    [PunRPC]
    public void CallBlackDeathEvent()
    {
        _blackPlayer.SetActive(false);
        if (!_bluePlayer.activeSelf)
        {
            Time.timeScale = 0f;
            UIPopUp = UIManager.Instance.OpenUI<UIPopUp>();
            UIPopUp.SetPopup("게임 오버", "다시 하시겠습니까?", OnGameRestart, OnGameEnd);
        }
        else OnBlackDeath?.Invoke();
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
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            if(_bluePlayer != null)
                stream.SendNext(_bluePlayer);
            if(_blackPlayer != null)
                stream.SendNext(_blackPlayer);
        }
        else
        {
            _bluePlayer = (GameObject)stream.ReceiveNext();
            _blackPlayer = (GameObject)stream.ReceiveNext();
        }
    }
}
