using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CheckJump : MonoBehaviour
{
    [SerializeField] private TilemapCollider2D[] _terrainColliders;
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = StageManager.Instance.PlayerRigidBody;
        StartCoroutine(CheckJumps(new WaitForSeconds(.05f)));
    }

    private IEnumerator CheckJumps(WaitForSeconds checkInterval)
    {
        while (true)
        {

            if(_rb != null)
            {
                if (_rb.velocity.y > 0)
                {
                    foreach (var terrain in _terrainColliders)
                    {
                        terrain.enabled = false;
                    }
                }
                else
                {
                    foreach (var terrain in _terrainColliders)
                    { terrain.enabled = true; }
                }
            }
            else
            {
                int idx = PhotonNetwork.LocalPlayer.ActorNumber;
                if(idx == 1)
                {
                    _rb = GameObject.FindGameObjectWithTag("Blue").GetComponent<Rigidbody2D>();
                }
                else if(idx == 2)
                {
                    _rb = GameObject.FindGameObjectWithTag("Black").GetComponent<Rigidbody2D>();
                }
            }
            
            yield return checkInterval;
        }
    }
}
