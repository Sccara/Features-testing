using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class CylinderA : MonoBehaviour
{
    public CylinderB cylinderB; // null

    // Start is called before the first frame update
    void Start()
    {
        cylinderB.CylinderBTestMethod(50);    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CylinderATestMethod(string message)
    {
        Debug.Log("Message in cylinderA: " + message);
    }
}
