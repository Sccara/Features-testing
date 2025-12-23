using UnityEngine;

public class Character : MonoBehaviour, IDamageable
{
    public int health;

    public int Health { get { return health; } set => health = value; }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Debug.Log("Health <= 0");
        }
    }
}
