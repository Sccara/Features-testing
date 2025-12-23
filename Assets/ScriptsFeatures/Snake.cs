using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    Vector2 dir = Vector2.right;
    List<Transform> tail = new List<Transform>();
    public bool ate = false;

    public GameObject tailPrefab;
    public float moveRate = 0.3f;

    void Start()
    {
        InvokeRepeating("Move", moveRate, moveRate);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W)) dir = Vector2.up;
        if (Input.GetKey(KeyCode.S)) dir = Vector2.down;
        if (Input.GetKey(KeyCode.A)) dir = Vector2.left;
        if (Input.GetKey(KeyCode.D)) dir = Vector2.right;
    }

    void Move()
    {
        Vector2 pos = transform.position;
        transform.Translate(dir);

        if (ate)
        {
            GameObject g = Instantiate(tailPrefab, pos, Quaternion.identity);
            tail.Insert(0, g.transform);
            ate = false;
        }
        else if (tail.Count > 0)
        {
            tail[tail.Count - 1].position = pos;
            tail.Insert(0, tail[tail.Count - 1]);
            tail.RemoveAt(tail.Count - 1);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Food")
        {
            ate = true;
            Destroy(coll.gameObject);
        }

        if (coll.tag == "Wall")
        {
            Destroy(gameObject);
            foreach (Transform t in tail)
            {
                Destroy(t.gameObject);
            }
        }
    }
}



