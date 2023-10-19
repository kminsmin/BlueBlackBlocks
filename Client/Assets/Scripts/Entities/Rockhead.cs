using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Photon.Pun;

public class Rockhead : MonoBehaviourPun, IPunObservable
{
    [SerializeField] private float _targetY;
    private float _currentY;
    private Sequence mySequence;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            if(collision.CompareTag("Blue")||collision.CompareTag("Black"))
            {
                _currentY = transform.position.y;
                mySequence = DOTween.Sequence();
                mySequence.Append(transform.DOMoveY(_targetY, 0.1f).SetEase(Ease.InCubic));
                mySequence.Append(transform.DOShakePosition(1f));
                mySequence.Append(transform.DOMoveY(_currentY, 1f));
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
