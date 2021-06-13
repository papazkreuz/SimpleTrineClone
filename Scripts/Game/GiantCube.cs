using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]

public class GiantCube : MonoBehaviour, ILevitatable, IDamageable
{
    private float health = 5;
    private Vector3 scalePerHP;

    private Vector3 startPosition;
    private Vector3 startScale;

    private Coroutine levitationCoroutine;
    private Coroutine fallingCoroutine;

    private readonly float levitationHeight = 3f;

    private IEnumerator Levitation()
    {
        Vector3 levitatePosition = startPosition + Vector3.up * levitationHeight;
        float uppingSpeed = 3f;

        //Going up
        while (transform.position.y < levitatePosition.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, levitatePosition, uppingSpeed * Time.deltaTime);

            yield return null;
        }

        transform.position = levitatePosition;

        float flyFrequency = 1f;
        float flyAmplitude = 0.2f;
        float startFlyingTime = Time.time;

        //Flying on top
        while (true)
        {
            float newY = levitatePosition.y + Mathf.Sin((Time.time - startFlyingTime) * Mathf.PI * flyFrequency) * flyAmplitude;
            Vector3 newPosition = new Vector3(transform.position.x, newY);
            transform.position = newPosition;

            yield return null;
        }

        yield break;
    }

    private IEnumerator Falling()
    {
        float fallingSpeed = 4f;

        while (transform.position.y > startPosition.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, fallingSpeed * Time.deltaTime);

            yield return null;
        }

        yield break;
    }

    private void Start()
    {
        startPosition = transform.position;
        startScale = transform.localScale;

        if (health <= 0)
        {
            Break();
        }
        else
        {
            scalePerHP = startScale / health;
        }
    }

    //ILevitatable
    public void Levitate()
    {
        if (fallingCoroutine != null)
        {
            StopCoroutine(fallingCoroutine);
            fallingCoroutine = null;
        }

        if (levitationCoroutine == null)
        {
            levitationCoroutine = StartCoroutine(Levitation());
        }
    }

    public void Fall()
    {
        StopCoroutine(levitationCoroutine);
        levitationCoroutine = null;
        fallingCoroutine = StartCoroutine(Falling());
    }

    public bool IsLevitating()
    {
        return levitationCoroutine != null;
    }

    //IDamageable
    public void GetDamage(int damage)
    {
        if (damage <= 0)
            throw new System.Exception("Damage can't be zero or negative");

        health -= damage;
        transform.localScale -= damage * scalePerHP;

        if (health <= 0) Break();
    }

    public void Break()
    {
        Destroy(gameObject);
    }

    public float Health => health;
}
