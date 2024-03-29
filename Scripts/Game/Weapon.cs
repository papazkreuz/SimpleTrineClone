﻿using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]

public class Weapon : MonoBehaviour
{
    private const int WEAPON_DAMAGE = 1;

    private BoxCollider2D _collider;

    private void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.parent.GetComponent<IDamageable>() != null)
        {
            IDamageable damageable = collision.transform.parent.GetComponent<IDamageable>();
            damageable.ReceiveDamage(WEAPON_DAMAGE);

            SetColliderEnabled(false);
        }
    }

    public void SetColliderEnabled(bool colliderEnabled)
    {
        _collider.enabled = colliderEnabled;
    }
}
