using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class SkipButton : MonoBehaviourPunCallbacks
{
    [SerializeField] private PlayableDirector _director;

    private void Start()
    {
        _director.stopped += InvokeChangeScene;
    }
    public void InvokeChangeScene(PlayableDirector director)
    {
        Invoke("ChangeScene", 0.5f);
    }

    public void ChangeScene()
    {
        _director.Stop();
        
        PhotonNetwork.LoadLevel("GameScene");
    }
    public void InvokeChangeScene()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        Invoke("ChangeScene", 0.5f);
    }
}
