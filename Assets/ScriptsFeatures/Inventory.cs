using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public float coins;
    public ItemSO selectedItem;

    public List<ItemSO> items = new List<ItemSO>();
    public Action OnInventoryChanged;

    public void AddItem(ItemSO item)
    {
        items.Add(item);
        OnInventoryChanged?.Invoke();
        Debug.Log($"Added item: {item.itemName}");
    }

    public void RemoveItem(ItemSO item)
    {
        items.Remove(item);
        OnInventoryChanged?.Invoke();
    }

    public void SelectItem(ItemSO item)
    {
        selectedItem = item;
    }

    public bool TryBuyItem(ItemSO item)
    {
        if (coins >= item.price)
        {
            coins -= item.price;
            items.Add(item);
            Debug.Log($"You bought: {item.itemName}");
            return true;
        }
        else
        {
            Debug.Log("Not enough coins!");
            return false;
        }
    }

}