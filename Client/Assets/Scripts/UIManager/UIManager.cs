using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : CustomSingleton<UIManager>
{
    private const string _UIOption = "_UIOption";

    private void Start()
    {
        GameObject uiOption = Resources.Load<GameObject>(_UIOption);
        GameObject instantiate = Instantiate(uiOption, Vector3.zero, Quaternion.identity);
        instantiate.transform.SetParent(this.transform);
    }

}
