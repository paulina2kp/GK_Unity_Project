using System.Collections.Generic;
using UnityEngine;

public class UIChestSlots : MonoBehaviour
{
    public Transform chestPanel; 
    private List<InventorySlots> chestSlots = new List<InventorySlots>();

    public ChestInventory currentChest; 

    private void Start()
    {
        foreach (Transform currSlot in chestPanel)
        {
            InventorySlots slotComponent = currSlot.GetComponent<InventorySlots>();
            if (slotComponent != null)
            {
                chestSlots.Add(slotComponent);
                slotComponent.isChestSlot = true;              
                slotComponent.chestUI = this;                   
            }
        }
    }
    public void OpenChestUI(ChestInventory chest)
    {
        currentChest = chest;
        UpdateChestSlots();
    }

    public void CloseChestUI()
    {
        currentChest = null;
        foreach (var slot in chestSlots)
        {
            slot.ClearSlot();
            slot.currentItem = null;
        }
    }

    public void UpdateChestSlots()
    {
        Debug.Log("UpdateChestSlots dla: " + (currentChest != null ? currentChest.name : "brak currentChest"));
        Debug.Log("Items count: " + (currentChest != null ? currentChest.Items.Count : 0));

        for (int i = 0; i < chestSlots.Count; i++)
        {
            if (currentChest != null && i < currentChest.Items.Count)
            {
                Debug.Log($"Slot {i}: {currentChest.Items[i].loot.loot_name}, Stack: {currentChest.Items[i].stackSize}");
                chestSlots[i].SetSlot(currentChest.Items[i]);
                chestSlots[i].index = i;
            }
            else
            {
                chestSlots[i].ClearSlot();
                chestSlots[i].currentItem = null;
            }
        }
    }

    public void SetActiveChest(ChestInventory chest)
    {
        if (currentChest != chest)
        {
            CloseChestUI(); 
            OpenChestUI(chest);
        }
    }

    public void TryRemoveItemFromSlot(int index)
    {
        if (currentChest != null && index < currentChest.Items.Count)
        {
            currentChest.Items.RemoveAt(index);
            UpdateChestSlots();
        }
    }
}
