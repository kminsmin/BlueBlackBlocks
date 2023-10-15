using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using static Define;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private CharacterColor _color;
    private void OnEnable()
    {
        DialogueManager.Instance?.LoadNextDialogue(_color);
    }
}
