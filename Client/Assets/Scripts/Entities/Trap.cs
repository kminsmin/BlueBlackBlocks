using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
using Photon.Pun;

public class Trap : MonoBehaviour
{
    [SerializeField] private TrapType type;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch(type)
        {
            case TrapType.Trap:
                if(collision.gameObject.CompareTag("Blue"))
                {
                    StageManager.Instance.photonView.RPC("CallBlueDeathEvent", RpcTarget.All);
                }
                else if(collision.gameObject.CompareTag("Black"))
                {
                    StageManager.Instance.photonView.RPC("CallBlackDeathEvent", RpcTarget.All);
                }
                break;
            case TrapType.Black:
                if (collision.gameObject.CompareTag("Blue"))
                {
                    StageManager.Instance.photonView.RPC("CallBlueDeathEvent", RpcTarget.All);
                }
                break;
            case TrapType.Blue:
                if (collision.gameObject.CompareTag("Black"))
                {
                    StageManager.Instance.photonView.RPC("CallBlackDeathEvent", RpcTarget.All);
                }
                break;

        }
    }
}
