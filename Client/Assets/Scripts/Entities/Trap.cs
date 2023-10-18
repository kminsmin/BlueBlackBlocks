using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

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
                    StageManager.Instance.CallBlueDeathEvent();
                }
                else if(collision.gameObject.CompareTag("Black"))
                {
                    StageManager.Instance.CallBlackDeathEvent(); 
                }
                break;
            case TrapType.Black:
                if (collision.gameObject.CompareTag("Blue"))
                {
                    StageManager.Instance.CallBlueDeathEvent();
                }
                break;
            case TrapType.Blue:
                if (collision.gameObject.CompareTag("Black"))
                {
                    StageManager.Instance.CallBlackDeathEvent();
                }
                break;

        }
    }
}
