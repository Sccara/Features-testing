using UnityEngine;
using UnityEngine.UI;

public class HealthSystemUI : MonoBehaviour
{
    [SerializeField] private HealthSystem health;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image armorBar;

    void Update()
    {
        if (health == null)
        { 
            return;
        }
        if (healthBar)
        {
            healthBar.fillAmount = health.HealthPercent;
        }
        if (armorBar)
        {
            armorBar.fillAmount = health.ArmorPercent;
        }
    }
}
