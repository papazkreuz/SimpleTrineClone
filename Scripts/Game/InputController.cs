using UnityEngine;

public class InputController : MonoBehaviour
{
    private Character character;

    private void Start()
    {
        character = FindObjectOfType<Character>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            character.Move(Vector3.left);
        }

        if (Input.GetKey(KeyCode.D))
        {
            character.Move(Vector3.right);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            character.Jump();
        }

        if (character.IsMage)
        {
            Mage mage = character.GetComponent<Mage>();

            if (Input.GetMouseButtonDown(0))
            {
                Vector3 clickScreenPosition = Input.mousePosition;
                Vector3 clickWorldPosition = Camera.main.ScreenToWorldPoint(clickScreenPosition);
                Vector2 rayPosition = new Vector2(clickWorldPosition.x, clickWorldPosition.y);

                RaycastHit2D hit = Physics2D.Raycast(rayPosition, Vector2.zero);

                if (hit)
                {
                    if (hit.transform.parent.GetComponent<ILevitatable>() != null)
                    {
                        ILevitatable levitatable = hit.transform.parent.GetComponent<ILevitatable>();
                        mage.ToggleObjectLevitation(levitatable);
                    }
                }
            }
        }

        if (character.IsWarrior)
        {
            Warrior warrior = character.GetComponent<Warrior>();

            if (Input.GetMouseButtonDown(0))
            {
                Vector3 clickScreenPosition = Input.mousePosition;
                Vector3 clickWorldPosition = Camera.main.ScreenToWorldPoint(clickScreenPosition);
                clickWorldPosition = new Vector3(clickWorldPosition.x, clickWorldPosition.y); // set Z axis to 0

                warrior.WeaponStrike(clickWorldPosition);
            }
        }
    }
}
