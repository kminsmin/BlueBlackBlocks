using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviourPun 
{
    [SerializeField] private int _index;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            if(collision.gameObject.CompareTag("Blue")||collision.gameObject.CompareTag("Black"))
            {
                if(_index > StageManager.Instance.CurrentCheckPointIndex)
                {
                    StageManager.Instance.photonView.RPC("SetCheckPoint", RpcTarget.All, _index);
                    gameObject.GetComponent<Animator>().SetBool("isOn", true);
                }

                if(_index ==  StageManager.Instance.CheckPoints.Length - 1&& StageManager.Instance.CurrentItemsCollected == 3)
                {
                    StageManager.Instance.photonView.RPC("CallGameClearEvent", RpcTarget.All);
                }
            }
        }
    }
}
