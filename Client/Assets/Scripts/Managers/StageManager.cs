using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviourPunCallbacks
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
    [SerializeField] private GameObject _cinemachine;
    [SerializeField] private GameObject _skipButton;

    private void Awake()
    {
        Time.timeScale = 1.0f;
        CurrentCheckPointIndex = 0;
        CurrentItemsCollected = 0;
        Instance = this;
        //TODO: photonView 추가한 플레이어 오브젝트 프리팹화, Resources 폴더에 넣어서 Load 후 Instantiate
        _bluePlayer = GameObject.FindGameObjectWithTag("Blue");
        _blackPlayer = GameObject.FindGameObjectWithTag("Black");
        
        UIItem = UIManager.Instance.OpenUI<UIItem>();
    }
    void Start()
    {
        UIItem.DelImage();
        _playerRigidBody = _bluePlayer.GetComponent<Rigidbody2D>();
        _bluePlayer.SetActive(false);
        _blackPlayer.SetActive(false);
        Invoke("StartGame", 60f);
        OnBlueDeath += () => Invoke("RespawnBlue", _respawnInterval);
        OnBlackDeath += () => Invoke("RespawnBlack", _respawnInterval);
        OnGameRestart += () => SceneManager.LoadScene("GameScene");
        OnGameEnd += () => SceneManager.LoadScene("StartScene");
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
        if (!_blackPlayer.activeSelf)
        {
            Time.timeScale = 0f;
            UIPopUp = UIManager.Instance.OpenUI<UIPopUp>();
            UIPopUp.SetPopup("게임 오버", "다시 하시겠습니까?", OnGameRestart, OnGameEnd);
        }
        else OnBlueDeath?.Invoke();
    }

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

    public void CallGameClearEvent()
    {
        Time.timeScale = 0f;
        UIPopUp = UIManager.Instance.OpenUI<UIPopUp>();
        UIPopUp.SetPopup("게임 클리어!", "다시 하시겠습니까?", OnGameRestart, OnGameEnd);
        OnGameClear?.Invoke();
    }

    public void SkipCutScene()
    {
        StartGame();
        _skipButton.gameObject.SetActive(false);    
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
}
