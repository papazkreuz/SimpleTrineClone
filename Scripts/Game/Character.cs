using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public abstract class Character : MonoBehaviour
{
    private const float JUMP_FORCE = 400f;
    private readonly Vector2 jumpDirection = Vector2.up;

    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _collider;
    private Rigidbody2D _rigidbody;

    protected CharacterClass characterClass;
    protected float moveSpeed = 2f;
    protected int maxJumpsCount = 1;
    
    private int _currentJumpCount;
    private bool _isGrounded;

    public event Action OnCharacterFinishedEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Finish>() != null)
        {
            OnCharacterFinishedEvent?.Invoke();
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Ground>() != null)
        {
            _isGrounded = true;
            _currentJumpCount = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Ground>() != null)
        {
            _isGrounded = false;
        }
    }

    protected virtual void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    protected void SetSpriteColor(Color color)
    {
        _spriteRenderer.color = color;
    }

    public void Move(Vector3 direction)
    {
        transform.localPosition += direction * moveSpeed * Time.deltaTime;
    }

    public void Jump()
    {
        if (_currentJumpCount < maxJumpsCount)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0f);
            _rigidbody.AddForce(jumpDirection * JUMP_FORCE);
            _currentJumpCount++;
        }
    }

    public bool IsMage => characterClass.Equals(CharacterClass.Mage);

    public bool IsRogue => characterClass.Equals(CharacterClass.Rogue);

    public bool IsWarrior => characterClass.Equals(CharacterClass.Warrior);
}
