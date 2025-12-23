using UnityEngine;

public class ObjectForInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemSO itemData;

    public void Interact(GameObject interactor)
    {
        interactor.GetComponent<Inventory>().AddItem(itemData);
        Destroy(gameObject);
    }
}
