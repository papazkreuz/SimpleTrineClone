using UnityEngine;

public class InputController : MonoBehaviour
{
    private Character _character;

    private void Start()
    {
        _character = FindObjectOfType<Character>();
        _character.OnCharacterFinishedEvent += Disable;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            _character.Move(Vector3.left);
        }

        if (Input.GetKey(KeyCode.D))
        {
            _character.Move(Vector3.right);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _character.Jump();
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (_character.IsMage)
            {
                Mage mage = _character.GetComponent<Mage>();

                RaycastHit2D hit = Physics2D.Raycast(GetMouseWorldPosition(), Vector2.zero);
                if (hit)
                {
                    if (hit.transform.parent.GetComponent<ILevitatable>() != null)
                    {
                        ILevitatable levitatable = hit.transform.parent.GetComponent<ILevitatable>();
                        mage.ToggleObjectLevitation(levitatable);
                    }
                }
            }

            if (_character.IsWarrior)
            {
                Warrior warrior = _character.GetComponent<Warrior>();

                warrior.WeaponStrike(GetMouseWorldPosition());
            }
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 clickScreenPosition = Input.mousePosition;
        Vector3 clickWorldPosition = Camera.main.ScreenToWorldPoint(clickScreenPosition);
        clickWorldPosition = new Vector3(clickWorldPosition.x, clickWorldPosition.y); // set Z axis to 0
        return clickWorldPosition;
    }

    private void Disable()
    {
        this.enabled = false;
    }
}
