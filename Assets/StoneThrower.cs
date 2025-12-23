using UnityEngine;

public class StoneThrower : MonoBehaviour
{
    public Transform throwPoint;
    public float minThrowForce = 5f;
    public float maxThrowForce = 25f;
    public float chargeSpeed = 10f;
    public float interactDistance = 5f;
    public GameObject heldStone;
    public float currentForce;
    public bool isCharging;
    public Camera cameraMain;

    private RaycastHit hit;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = cameraMain.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, interactDistance))
            {
                if (hit.collider.gameObject.tag == "Stone")
                {
                    heldStone = hit.collider.gameObject;
                    heldStone.transform.SetParent(transform);
                    heldStone.transform.position = throwPoint.position;
                    heldStone.SetActive(false);
                }
            }
        }

        if (heldStone != null && Input.GetMouseButtonDown(0))
        {
            isCharging = true;
            currentForce = minThrowForce;
        }

        if (isCharging && Input.GetMouseButton(0))
        {
            currentForce += chargeSpeed * Time.deltaTime;
            currentForce = Mathf.Clamp(currentForce, minThrowForce, maxThrowForce);
        }

        if (isCharging && Input.GetMouseButtonUp(0))
        {
            ThrowStone();
        }
    }

    void ThrowStone()
    {
        heldStone.SetActive(true);
        heldStone.transform.SetParent(null);

        Rigidbody rb = heldStone.GetComponent<Rigidbody>();
        rb.velocity = cameraMain.transform.forward * currentForce;

        heldStone = null;
        isCharging = false;
    }

}
