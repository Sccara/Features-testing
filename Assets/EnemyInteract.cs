using UnityEngine;

public class EnemyInteract : MonoBehaviour, IInteractable, IDamageable
{
    public int health;

    public int Health { get { return health; } set => health = value; }

    private void Start()
    {
        health = 100;
    }

    public void Interact(GameObject interactor)
    {
        // В этом месте умирает враг
        TakeDamage(25);
    }

    public void Die()
    {
        Debug.Log("Enemy dead!");
        Destroy(gameObject);
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }
}
