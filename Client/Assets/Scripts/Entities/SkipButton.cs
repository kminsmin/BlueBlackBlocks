using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class SkipButton : MonoBehaviour
{
    [SerializeField] PlayableDirector _director;

    private void Start()
    {
        _director.stopped += ChangeScene;
        if(!PhotonNetwork.IsMasterClient)
        {
            gameObject.SetActive(false);
        }
    }
    public void ChangeScene(PlayableDirector director)
    {
        PhotonNetwork.LoadLevel("GameScene");
    }

    public void ChangeScene()
    {
        PhotonNetwork.LoadLevel("GameScene");
    }
}
