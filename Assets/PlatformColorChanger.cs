using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlatformColorChanger : MonoBehaviour
{
    private void Awake()
    {
        InteractableButton.OnButtonPressed += ChangeColor;
        InteractableButton.OnButtonPressed += MoveObject;
    }

    public void ChangeColor()
    {
        GetComponent<MeshRenderer>().material.color = UnityEngine.Random.ColorHSV();
    }

    public void MoveObject()
    {
        transform.position = transform.position + new Vector3(0, 0, 1);
    }

    private void OnDestroy()
    {
        InteractableButton.OnButtonPressed -= ChangeColor;
        InteractableButton.OnButtonPressed -= MoveObject;
    }
}
