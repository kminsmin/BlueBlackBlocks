using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("UIManager");
                _instance = go.AddComponent<UIManager>();

                DontDestroyOnLoad(_instance);
            }

            return _instance;
        }
    }

    private const string _UIOption = "_UIOption";

    private void Start()
    {
        _instance = this;
        GameObject uiOption = Resources.Load<GameObject>(_UIOption);
        GameObject instantiate = Instantiate(uiOption, Vector3.zero, Quaternion.identity);
        instantiate.transform.SetParent(this.transform);
    }



}
