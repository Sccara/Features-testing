using UnityEngine;

[CreateAssetMenu(fileName = "Health Power Up", menuName = "PowerUps/Health Power Up")]
public class HealthPowerUp : PowerUpSO
{
    public int healAmount;

    public override void Apply(GameObject player)
    {
        player.GetComponent<HealthSystem>().Heal(healAmount);
    }
}
