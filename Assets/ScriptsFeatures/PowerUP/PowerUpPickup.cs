using UnityEngine;

public class PowerUpPickup : MonoBehaviour
{
    public PowerUpSO powerUp;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            powerUp.Apply(other.gameObject);
            Destroy(gameObject);
        }
    }
}