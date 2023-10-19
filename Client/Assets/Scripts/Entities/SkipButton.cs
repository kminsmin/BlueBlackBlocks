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
    }
    public void ChangeScene(PlayableDirector director)
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
