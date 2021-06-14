using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawn : MonoBehaviour
{
    [SerializeField] private GameObject _characterPrefab;
    private Dictionary<CharacterClass, Type> _characterTypes;

    private void Start()
    {
        _characterTypes = new Dictionary<CharacterClass, Type>();
        _characterTypes.Add(CharacterClass.Mage, typeof(Mage));
        _characterTypes.Add(CharacterClass.Rogue, typeof(Rogue));
        _characterTypes.Add(CharacterClass.Warrior, typeof(Warrior));

        GameObject spawnedCharacter = Instantiate(_characterPrefab);
        spawnedCharacter.name = _characterPrefab.name;

        spawnedCharacter.AddComponent(_characterTypes[GameData.pickedClass]);
    }
}
