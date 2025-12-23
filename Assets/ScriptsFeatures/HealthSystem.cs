using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float maxArmor;

    [SerializeField] private float health; 
    [SerializeField] private float armor; 

    public static Action<float> OnHealthChanged;
    public static Action<float> OnArmorChanged;
    public static Action OnDeath;

    public float HealthPercent => health / maxHealth;
    public float ArmorPercent => armor / maxArmor;

    private void Start()
    {
        health = maxHealth;
        armor = maxArmor;
    }

    public void TakeDamage(float damage) 
    {
        if (damage <= 0f)
        {
            return;
        }

        if (armor > 0f)
        {
            float absorbed = Mathf.Min(armor, damage);
            armor -= absorbed;
            damage -= absorbed; 
        }

        if (damage > 0f)
        {
            health = Mathf.Max(0f, health - damage);
            if (health <= 0f)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        OnDeath?.Invoke(); // Скорочення

        //if (OnDeath != null)
        //{
        //    OnDeath.Invoke();
        //}
    }

    public void Heal(float amount)
    {
        if (amount <= 0f)
        {
            return;
        }

        health = Mathf.Min(maxHealth, health + amount);
    }

    public void AddArmor(float amount)
    {
        if (amount <= 0f)
        {
            return;
        }

        armor = Mathf.Min(maxArmor, armor + amount);
    }

}
