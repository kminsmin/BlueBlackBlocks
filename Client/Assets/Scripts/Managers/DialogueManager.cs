using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using static Define;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [SerializeField] private DialogueSO _dialogueSO;
    [SerializeField] private TextMeshProUGUI _blueText;
    [SerializeField] private TextMeshProUGUI _blackText;
    [SerializeField] private float _textTypeDuration = 1.0f;
    private List<string> _dialogueList;
    private int _dialogueIndex;


    private void Awake()
    {
        Instance = this;
        _dialogueList = _dialogueSO.dialogue;
    }

    public void LoadNextDialogue(CharacterColor color)
    {
        switch(color)
        {
            case CharacterColor.Blue:
                _blueText.text = _dialogueList[_dialogueIndex];
                ShowText(_blueText, _textTypeDuration);
                break;
            case CharacterColor.Black:
                _blackText.text = _dialogueList[_dialogueIndex];
                ShowText(_blackText, _textTypeDuration);
                break;
            default:
                return;
        }
        if (_dialogueIndex < _dialogueList.Count - 1)
            _dialogueIndex++;

    }

    private static void ShowText(TextMeshProUGUI text, float duration)
    {
        text.maxVisibleCharacters = 0;
        DOTween.To(x => text.maxVisibleCharacters = (int)x, 0f, text.text.Length, duration);
    }
}
