using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Photon.Pun;

public class FallingPlatform : MonoBehaviourPun, IPunObservable
{
    private Transform _transform;
    private Vector3 _fall = Vector3.zero;
    private Vector3 _initialPos = Vector3.zero;
    private Rigidbody2D _rigidbody;
    private bool _isColliding;
    [SerializeField] private float _fallSpeed = 1.5f;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        _fall = new Vector3(0, -1, 0) * _fallSpeed * Time.deltaTime;
        _initialPos = _transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(_fall == Vector3.zero)
        {
            _fall = new Vector3(0, -1, 0) * _fallSpeed * Time.deltaTime;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision != null)
        {
            if(collision.gameObject.CompareTag("Blue")||collision.gameObject.CompareTag("Black"))
            {
                _isColliding = true;
                _transform.Translate(_fall);
            }
         }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (_transform.position.y < _initialPos.y)
            {
                _isColliding = false;
            }
            if (!_isColliding)
            {
                _transform.DOLocalMoveY(_initialPos.y, 1f);
            }

        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(_rigidbody.position);
            stream.SendNext(_rigidbody.velocity);
        }
        else
        {
            _rigidbody.position = (Vector2)stream.ReceiveNext();
            _rigidbody.velocity = (Vector2)stream.ReceiveNext();
        }
    }
}
