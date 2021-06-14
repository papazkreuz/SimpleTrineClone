using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInstruction : MonoBehaviour
{
    [SerializeField] private CharacterInstructionDictionary _classInstructions;
    private Text _text;

    private void Start()
    {
        _text = GetComponent<Text>();

        _text.text = _classInstructions[GameData.pickedClass];
    }
}

