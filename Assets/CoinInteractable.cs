using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinInteractable : MonoBehaviour, IInteractable, IDamageable
{
    public int health;

    public int Health { get { return health; } set => health = value; }

    public void Interact(GameObject interactor)
    {
        // В этом месте монетка собирается
        Destroy(gameObject);
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
