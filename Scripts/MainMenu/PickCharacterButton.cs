using UnityEngine;
using UnityEngine.SceneManagement;

public class PickCharacterButton : MonoBehaviour
{
    [SerializeField] private CharacterClass _characterClass;

    public void PickCharacter()
    {
        GameData.pickedClass = _characterClass;
        SceneManager.LoadScene("Game");
    }
}
