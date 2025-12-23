using UnityEngine;

public class Stone : MonoBehaviour
{
    public int quality = 3;
    public float force = 0.7f;
    private Rigidbody rb;

    private void Start()
    {
        quality = Random.Range(1, 5);
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Water") && quality > 0)
        {
            rb.AddForce(Vector3.up * force, ForceMode.Impulse);

            quality--;

            if (quality <= 0)
            {
                GetComponent<SphereCollider>().isTrigger = true;
            }
        }
    }

    
}
