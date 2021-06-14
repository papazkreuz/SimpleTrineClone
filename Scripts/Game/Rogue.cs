using UnityEngine;

public class Rogue : Character
{
    protected override void Start()
    {
        base.Start();

        characterClass = CharacterClass.Rogue;

        moveSpeed = 3.5f;
        maxJumpsCount = 2;

        SetSpriteColor(Color.green);
    }
}
