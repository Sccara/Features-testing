using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour, IInteractable
{
    public ShopUI shopUI;
    public ItemSO[] itemsForSale;
    public Inventory inventory;

    public void Interact(GameObject interactor)
    {
        shopUI.ShowPanel();
    }

    public void BuyItem(int index)
    {
        if (inventory != null && index >= 0 && index < itemsForSale.Length)
        {
            inventory.TryBuyItem(itemsForSale[index]);
        }
    }
}
