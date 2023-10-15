using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [SerializeField] private DialogueSO _dialogueSO;
    [SerializeField] private TextMeshProUGUI _dialogueText;
    private List<string> _dialogueList;
    private int _dialogueIndex;


    private void Awake()
    {
        Instance = this;
        _dialogueList = _dialogueSO.dialogue;
    }

    public void LoadNextDialogue()
    {
        _dialogueText.text = _dialogueList[_dialogueIndex];
        if(_dialogueIndex < _dialogueList.Count-1)
            _dialogueIndex++;
    }
}
