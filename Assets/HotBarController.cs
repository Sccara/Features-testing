using UnityEngine;
using UnityEngine.UI;

public class HotBarController : MonoBehaviour
{
    public int slotCount = 5;
    private int selectedIndex = 0;
    public Image[] slotImages;
    public ItemSO[] hotbarItems;
    public Color selectedColor;
    public Color defaultColor;

    private void Update()
    {
        HandleScrollInput();
        HandleNumberInput();
        HandleItemUse();
        HandleItemDrop();
    }

    private void HandleNumberInput()
    {
        for (int i = 0; i < slotCount; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString())) 
            {
                selectedIndex = i;
                UpdateSlotSelection();
                break;
            }
        }
    }

    private void HandleScrollInput()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f)
        {
            selectedIndex--;
            if (selectedIndex < 0) selectedIndex = slotCount - 1;
            UpdateSlotSelection();
        }
        else if (scroll < 0f)
        {
            selectedIndex++;
            if (selectedIndex >= slotCount) selectedIndex = 0;
            UpdateSlotSelection();
        }
    }



    private void UpdateSlotSelection()
    {
        for (int i = 0; i < slotImages.Length; i++)
        {
            slotImages[i].color = (i == selectedIndex) ? selectedColor : defaultColor;

            //if (i == selectedIndex)
            //{
            //    slotImages[i].color = selectedColor;
            //}
            //else
            //{
            //    slotImages[i].color = defaultColor;
            //}
        }
    }

    public int GetSelectedSlot()
    {
        return selectedIndex;
    }

    private void HandleItemUse()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ItemSO currentItem = hotbarItems[selectedIndex];
            if (currentItem != null)
            {
                currentItem.Use();

                if (currentItem.isConsumable)
                {
                    currentItem.amount--;
                    if (currentItem.amount <= 0)
                    {
                        hotbarItems[selectedIndex] = null;
                    }
                    //UpdateHotbarUI();
                }
            }
        }
    }

    private void HandleItemDrop()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            hotbarItems[selectedIndex] = null;
        }
    }
}
