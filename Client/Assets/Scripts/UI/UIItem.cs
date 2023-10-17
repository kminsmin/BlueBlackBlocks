using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItem : MonoBehaviour
{
    [SerializeField] private Image _image;

    public void SetImage(Sprite sprite)
    {
        _image.sprite = sprite;
        _image.gameObject.SetActive(true);
    }

    public void DelImage()
    {
        _image.sprite = null;
        _image.gameObject.SetActive(false);
    }
}
