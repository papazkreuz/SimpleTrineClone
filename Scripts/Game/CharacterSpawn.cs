using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawn : MonoBehaviour
{
    [SerializeField] private GameObject _characterPrefab;
    private readonly Dictionary<CharacterClass, Type> _characterTypes = new Dictionary<CharacterClass, Type>
    {
        { CharacterClass.Mage, typeof(Mage) },
        { CharacterClass.Rogue, typeof(Rogue) },
        { CharacterClass.Warrior, typeof(Warrior) }
    };

    private void Start()
    {
        GameObject spawnedCharacter = Instantiate(_characterPrefab);
        spawnedCharacter.name = _characterPrefab.name;

        spawnedCharacter.AddComponent(_characterTypes[GameData.pickedClass]);
    }
}
