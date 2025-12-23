using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public float price;
    public Sprite icon;
    public bool isStackable;
    public bool isConsumable;
    public float amount;

    public virtual void Use()
    {
        Debug.Log($"Using item: {itemName}");
    }
}