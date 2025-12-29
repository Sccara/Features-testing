using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] public int health;

    public abstract void TakeDamage(int amount);

    public abstract void Attack();

    public void Move()
    {
        Debug.Log("Move");
    }

}
