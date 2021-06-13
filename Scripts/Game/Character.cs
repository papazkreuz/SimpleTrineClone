using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public abstract class Character : MonoBehaviour
{
    protected enum CharacterClass
    {
        Mage,
        Rogue,
        Warrior
    }

    protected SpriteRenderer _spriteRenderer;
    private BoxCollider2D _collider;
    private Rigidbody2D _rigidbody;

    protected float moveSpeed = 2f;
    private float jumpForce = 400f;

    protected int maxJumpsCount = 1;

    private int currentJumpCount;
    private bool isGrounded;

    protected CharacterClass characterClass;

    private readonly Vector2 jumpDirection = Vector2.up;

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Ground>() != null)
        {
            isGrounded = true;
            currentJumpCount = maxJumpsCount;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Ground>() != null)
        {
            isGrounded = false;
        }
    }

    protected virtual void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector3 direction)
    {
        transform.localPosition += direction * moveSpeed * Time.deltaTime;
    }

    public void Jump()
    {
        if (currentJumpCount > 0)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0f);
            _rigidbody.AddForce(jumpDirection * jumpForce);
            currentJumpCount--;
        }
    }

    public bool IsMage => characterClass.Equals(CharacterClass.Mage);

    public bool IsRogue => characterClass.Equals(CharacterClass.Rogue);

    public bool IsWarrior => characterClass.Equals(CharacterClass.Warrior);
}
