using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]

public class GiantCube : MonoBehaviour, ILevitatable, IDamageable
{
    private const float LEVITATION_HEIGHT = 3f;
    private const float RAISING_SPEED = 3f;
    private const float FLY_AMPLITUDE = 1f;
    private const float FLY_FREQUENCY = 0.2f;
    private const float FALLING_SPEED = 4f;

    [SerializeField] private float _health;
    private Vector3 _scaleLossPerHP;

    private Vector3 _startPosition;
    private Vector3 _startScale;

    private Coroutine levitationCoroutine;
    private Coroutine fallingCoroutine;

    private IEnumerator Levitation()
    {
        Vector3 levitatePosition = _startPosition + Vector3.up * LEVITATION_HEIGHT;

        //Going up
        while (transform.position.y < levitatePosition.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, levitatePosition, RAISING_SPEED * Time.deltaTime);

            yield return null;
        }

        transform.position = levitatePosition;

        float startFlyingTime = Time.time;

        //Flying on top
        while (true)
        {
            float newY = levitatePosition.y + Mathf.Sin((Time.time - startFlyingTime) * Mathf.PI * FLY_FREQUENCY) * FLY_AMPLITUDE;
            Vector3 newPosition = new Vector3(transform.position.x, newY);
            transform.position = newPosition;

            yield return null;
        }

        yield break;
    }

    private IEnumerator Falling()
    {
        while (transform.position.y > _startPosition.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, _startPosition, FALLING_SPEED * Time.deltaTime);

            yield return null;
        }

        yield break;
    }

    private void Start()
    {
        _startPosition = transform.position;
        _startScale = transform.localScale;

        if (_health <= 0)
        {
            Break();
        }
        else
        {
            _scaleLossPerHP = _startScale / _health;
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
    public void ReceiveDamage(int damage)
    {
        if (damage <= 0)
        {
            throw new System.Exception("Damage can't be zero or negative");
        }

        _health -= damage;
        transform.localScale -= damage * _scaleLossPerHP;

        if (_health <= 0) Break();
    }

    public void Break()
    {
        Destroy(gameObject);
    }

    public float Health => _health;
}
