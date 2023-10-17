using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] private GameObject _door;
    [SerializeField] private GameObject _glow;
    private bool _isOpen;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!_isOpen)
        {
            _isOpen = true;
            _door.SetActive(false);
            _glow.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_isOpen)
        {
            _isOpen = false;
            _door.SetActive(true);
            _glow.SetActive(false);
        }
    }
}
