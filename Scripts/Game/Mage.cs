using UnityEngine;

public class Mage : Character
{
    protected override void Start()
    {
        base.Start();

        characterClass = CharacterClass.Mage;

        SetSpriteColor(Color.blue);
    }

    public void ToggleObjectLevitation(ILevitatable levitatable)
    {
        if (levitatable.IsLevitating() == false)
        {
            levitatable.Levitate();
        }
        else
        {
            levitatable.Fall();
        }
    }
}
