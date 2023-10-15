using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DefaultDialogueData", menuName = "DialogueData/Default", order = 0)]
public class DialogueSO : ScriptableObject
{
    [TextArea]
    public List<string> dialogue;
}