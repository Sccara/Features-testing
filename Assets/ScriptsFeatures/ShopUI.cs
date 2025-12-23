using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public Shop shop;
    public GameObject buttonPrefab;
    public GameObject panel;
    public Transform buttonContainer;

    // Start is called before the first frame update
    void Start()
    {
        UpdateShop();
    }

    public void ShowPanel()
    {
        panel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void ClosePanel()
    {
        panel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void UpdateShop()
    {
        foreach (Transform child in buttonContainer)
            Destroy(child.gameObject);

        for (int i = 0; i < shop.itemsForSale.Length; i++)
        {
            var item = shop.itemsForSale[i];
            GameObject btn = Instantiate(buttonPrefab, buttonContainer);

            btn.GetComponentInChildren<TextMeshProUGUI>().text = $"{item.itemName} - {item.price}";
            int index = i;
            btn.GetComponent<Button>().onClick.AddListener(() => shop.BuyItem(index));
        }
    }
}
