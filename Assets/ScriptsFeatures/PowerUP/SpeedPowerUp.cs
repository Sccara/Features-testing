using UnityEngine;

[CreateAssetMenu(fileName = "Speed Power Up", menuName = "PowerUps/Speed Power Up")]
public class SpeedPowerUp : PowerUpSO
{
    public int speedMultiplier;
    public float powerUpDuration;

    public override void Apply(GameObject player)
    {
        player.GetComponent<PlayerController>().moveSpeed *= speedMultiplier;
    }
}