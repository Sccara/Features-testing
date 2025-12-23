using UnityEngine;

public class Food : MonoBehaviour
{
    public GameObject foodPrefab;
    public float xRange = 8f;
    public float yRange = 4f;

    void Start()
    {
        SpawnFood();
    }

    public void SpawnFood()
    {
        Vector2 pos = new Vector2(Random.Range(-xRange, xRange), Random.Range(-yRange, yRange));
        Instantiate(foodPrefab, pos, Quaternion.identity);
        Destroy(gameObject);
    }
}