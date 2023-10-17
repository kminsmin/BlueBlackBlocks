using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StageManager : MonoBehaviourPunCallbacks
{
    public static StageManager Instance;
    public event Action OnBlueDeath;
    public event Action OnBlackDeath;
    public int CurrentCheckPointIndex { get; private set; } = 0;
    public int CurrentItemsCollected { get; private set; } = 0;
    public UIItem UIItem { get; private set; }
    private GameObject _bluePlayer;
    private GameObject _blackPlayer;
    private Rigidbody2D _playerRigidBody;
    [SerializeField] private TilemapCollider2D[] _terrainColliders;
    [SerializeField] private Transform[] _checkPoints;
    [SerializeField] private Sprite[] _itemSprites;
    [SerializeField] private float _respawnInterval = 3f; 
    [SerializeField] GameObject _cinemachine;

    private void Awake()
    {
        Instance = this;
        //TODO: photonView 추가한 플레이어 오브젝트 프리팹화, Resources 폴더에 넣어서 Load 후 Instantiate
        /*
         * if(photonView.AmOwner) 아이템 생성 메서드 실행(상황봐서 Start로 옮길 예정)
         */
        _bluePlayer = GameObject.FindGameObjectWithTag("Blue");
        _blackPlayer = GameObject.FindGameObjectWithTag("Black");
        
        UIItem = UIManager.Instance.OpenUI<UIItem>();
    }
    void Start()
    {
        _playerRigidBody = _bluePlayer.GetComponent<Rigidbody2D>();
        _bluePlayer.SetActive(false);
        _blackPlayer.SetActive(false);
        Invoke("StartGame", 60f);
        OnBlueDeath += InvokeRespawnBlue;
        OnBlackDeath += InvokeRespawnBlack;
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
        _bluePlayer.SetActive(true);
        _blackPlayer.SetActive(true);
        _cinemachine.SetActive(false);
        StartCoroutine(CheckJump(new WaitForSeconds(.05f)));
    }

    public void CallBlueDeathEvent()
    {
        _bluePlayer.SetActive(false);
        OnBlueDeath?.Invoke();
    }

    public void CallBlackDeathEvent()
    {
        _blackPlayer.SetActive(false);
        OnBlackDeath?.Invoke();
    }

    public void SetCheckPoint(int index)
    {
        CurrentCheckPointIndex = index;
    }

    public void CollectItem()
    {
        CurrentItemsCollected++;
        UIItem.SetImage(_itemSprites[CurrentItemsCollected - 1]);
    }

    private void InvokeRespawnBlue()
    {
        Invoke("RespawnBlue", _respawnInterval);
    }
    private void RespawnBlue()
    {
        _bluePlayer.transform.position = _checkPoints[CurrentCheckPointIndex].position;
        _bluePlayer.gameObject.SetActive(true);
    }

    private void InvokeRespawnBlack()
    {
        Invoke("RespawnBlack", _respawnInterval);
    }
    private void RespawnBlack()
    {
        _blackPlayer.transform.position = _checkPoints[CurrentCheckPointIndex].position;
        _blackPlayer.gameObject.SetActive(true);
    }
}
