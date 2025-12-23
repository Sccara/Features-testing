using System;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;

public class InteractableButton : MonoBehaviour, IInteractable
{
    public static Action OnButtonPressed; // Створення події

    private void Awake()
    {
        OnButtonPressed += PrintMessage; // Підписка на подію
    }

    public void Interact(GameObject interactor)
    {
        // Викликається при натисканні на кнопку

        OnButtonPressed.Invoke(); // Виклик події
    }

    public void PrintMessage()
    {
        Debug.Log("Message!");
    }

    private void OnDestroy()
    {
        OnButtonPressed -= PrintMessage; // Відписка від події
    }
}
