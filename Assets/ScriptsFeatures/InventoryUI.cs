using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private Inventory inventory;
    [SerializeField] private Transform itemsParent; 
    [SerializeField] private GameObject slotPrefab;

    private bool isOpen = false;

    private void Start()
    {
        inventory.OnInventoryChanged += UpdateUI;
        inventoryPanel.SetActive(false);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            isOpen = !isOpen;
            Cursor.lockState = isOpen ? CursorLockMode.None : CursorLockMode.Locked;
            inventoryPanel.SetActive(isOpen);

            if (isOpen)
                UpdateUI();
        }
    }

    private void UpdateUI()
    {
        foreach (Transform child in itemsParent)
        {
            Destroy(child.gameObject);
        }

        foreach (ItemSO item in inventory.items)
        {
            GameObject slotObj = Instantiate(slotPrefab, itemsParent);
            ItemSlot slot = slotObj.GetComponent<ItemSlot>();
            slot.SetItem(item);
        }
    }
}