using UnityEngine;

[CreateAssetMenu(fileName = "New Power Up", menuName = "PowerUps/PowerUp")]
public abstract class PowerUpSO : ScriptableObject, IPowerUp
{
    public string powerUpName;
    public Sprite icon;

    public abstract void Apply(GameObject player);
}
