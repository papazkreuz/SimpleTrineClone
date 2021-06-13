using System.Collections;
using UnityEngine;

public class Warrior : Character
{
    [SerializeField] private GameObject warriorWeaponPrefab;
    private Weapon weapon;

    private Coroutine weaponStrikeAnimation;

    private readonly float weaponStrikeRange = 4.0f;
    private readonly float weaponStrikeSpeed = 20f;

    private IEnumerator WeaponStrikeAnimation(Vector3 targetPosition)
    {
        weapon.SetColliderEnabled(true);
        weapon.transform.right = targetPosition - weapon.transform.position;

        while (Vector3.Distance(weapon.transform.localPosition, Vector3.zero) < weaponStrikeRange)
        {
            weapon.transform.localPosition += weapon.transform.right * Time.deltaTime * weaponStrikeSpeed;
            yield return null;
        }

        while (weapon.transform.localPosition != Vector3.zero)
        {
            weapon.transform.localPosition = Vector3.MoveTowards(weapon.transform.localPosition, Vector3.zero, Time.deltaTime * weaponStrikeSpeed);
            yield return null;
        }

        weapon.SetColliderEnabled(false);
        weaponStrikeAnimation = null;
        yield break;
    }

    protected override void Start()
    {
        base.Start();

        characterClass = CharacterClass.Warrior;

        _spriteRenderer.color = Color.red;

        GameObject weaponGO = Instantiate(warriorWeaponPrefab, transform);
        weaponGO.name = warriorWeaponPrefab.name;
        weapon = weaponGO.GetComponent<Weapon>();
    }

    public void WeaponStrike(Vector3 targetPosition)
    {
        if (weaponStrikeAnimation == null)
            weaponStrikeAnimation = StartCoroutine(WeaponStrikeAnimation(targetPosition));
    }
}
