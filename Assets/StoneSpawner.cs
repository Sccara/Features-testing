using UnityEngine;

public class StoneSpawner : MonoBehaviour
{
    public GameObject stonePrefab;
    public int count = 20;
    public Vector3 areaSize = new Vector3(20, 1, 20);

    void Start()
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 pos = transform.position + new Vector3(
                Random.Range(-areaSize.x / 2, areaSize.x / 2),
                1,
                Random.Range(-areaSize.z / 2, areaSize.z / 2)
            );
            Instantiate(stonePrefab, pos, Quaternion.identity);
        }
    }

}
