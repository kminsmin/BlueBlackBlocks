using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Dialogue : MonoBehaviour
{
    private TextMeshProUGUI _dialogueText;
    [SerializeField] private float _textTypeDuration = 1.0f;

    private void Awake()
    {
        _dialogueText = GetComponent<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        DialogueManager.Instance?.LoadNextDialogue();
        ShowText(_dialogueText, _textTypeDuration);

    }

    private static void ShowText(TextMeshProUGUI text, float duration)
    {
        text.maxVisibleCharacters = 0;
        DOTween.To(x => text.maxVisibleCharacters = (int)x, 0f, text.text.Length, duration);
    }
}
