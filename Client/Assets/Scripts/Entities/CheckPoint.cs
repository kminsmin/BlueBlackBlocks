using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
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
                    StageManager.Instance.SetCheckPoint(_index);
                    gameObject.GetComponent<Animator>().SetBool("isOn", true);
                }
                if(_index ==  StageManager.Instance._checkPoints.Length - 1&& StageManager.Instance.CurrentItemsCollected == 3)
                {
                    StageManager.Instance.CallGameClearEvent();
                }
            }
        }
    }
}
