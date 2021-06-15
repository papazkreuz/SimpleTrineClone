using System.Collections;
using UnityEngine;

public class Warrior : Character
{
    private const float WEAPON_STRIKE_RANGE = 4f;
    private const float WEAPON_STRIKE_SPEED = 20f;

    private GameObject _warriorWeaponPrefab;
    private Weapon _weapon;

    private Coroutine weaponStrikeAnimationCoroutine;

    private IEnumerator WeaponStrikeAnimation(Vector3 targetPosition)
    {
        _weapon.SetColliderEnabled(true);
        _weapon.transform.right = targetPosition - _weapon.transform.position;

        while (Vector3.Distance(_weapon.transform.localPosition, Vector3.zero) < WEAPON_STRIKE_RANGE)
        {
            _weapon.transform.localPosition += _weapon.transform.right * Time.deltaTime * WEAPON_STRIKE_SPEED;
            yield return null;
        }

        while (_weapon.transform.localPosition != Vector3.zero)
        {
            _weapon.transform.localPosition = Vector3.MoveTowards(_weapon.transform.localPosition, Vector3.zero, Time.deltaTime * WEAPON_STRIKE_SPEED);
            yield return null;
        }

        _weapon.SetColliderEnabled(false);
        weaponStrikeAnimationCoroutine = null;
        yield break;
    }

    protected override void Start()
    {
        base.Start();

        characterClass = CharacterClass.Warrior;

        SetSpriteColor(Color.red);

        _warriorWeaponPrefab = Resources.Load<GameObject>("Prefabs/Weapon");

        GameObject weaponGO = Instantiate(_warriorWeaponPrefab, transform);
        weaponGO.name = _warriorWeaponPrefab.name;
        _weapon = weaponGO.GetComponent<Weapon>();
    }

    public void WeaponStrike(Vector3 targetPosition)
    {
        if (weaponStrikeAnimationCoroutine == null)
        {
            weaponStrikeAnimationCoroutine = StartCoroutine(WeaponStrikeAnimation(targetPosition));
        }
    }
}
