using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float distance = 3f;

    private IInteractable currentTarget;

    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    CheckForAttack();
        //}

        CheckForInteractable();

        if (currentTarget != null && Input.GetKeyDown(KeyCode.E))
        {
            currentTarget.Interact(gameObject);
        }
    }

    [System.Obsolete]
    void CheckForInteractable()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Debug.DrawRay(transform.position, ray.direction * 50, Color.red);

        if (Physics.Raycast(ray, out hit, distance))
        {
            currentTarget = hit.collider.GetComponent<IInteractable>();
        }
        else
        {
            currentTarget = null;
        }

        transform.rotation = Quaternion.Euler(0, 90f, 0);
    }

    void CheckForAttack()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Debug.DrawRay(transform.position, ray.direction * 50, Color.red);

        if (Physics.Raycast(ray, out hit, distance))
        {
            IDamageable objectToDamage = hit.collider.gameObject.GetComponent<IDamageable>();
            objectToDamage?.TakeDamage(25);
        }
    }

}

