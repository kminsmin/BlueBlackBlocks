using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Rockhead : MonoBehaviour
{
    [SerializeField] private float _targetY;
    private float _currentY;
    private Sequence mySequence;


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
}
