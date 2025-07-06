using System.Collections.Generic;
using UnityEngine;

public class ChestInventory : MonoBehaviour
{
    public int maxSlots = 10;
    public int maxSlotCapacity = 20;
    public List<ItemClass> Items = new List<ItemClass>();

    public bool playerInRange = false;
    public GameObject slotPanel;
    public GameObject my_Player;

    void Update()
    {

        if (playerInRange)
        {
            slotPanel.SetActive(true);
            var uiHandler = slotPanel.GetComponent<UIChestSlots>();
            if (uiHandler != null)
            {
                //uiHandler.OpenChestUI(this);
                uiHandler.SetActiveChest(this);
            }

            Debug.Log("slotPanel: " + slotPanel.name);
            Debug.Log("UI handler: " + slotPanel.GetComponent<UIChestSlots>());
        }
        else
        {
            slotPanel.SetActive(false);
            var uiHandler = slotPanel.GetComponent<UIChestSlots>();
            if (uiHandler != null)
            {
                uiHandler.CloseChestUI();
            }
        }
    }

    private void OnTriggerEnter(Collider my_collider)
    {
        if (my_collider.gameObject.Equals(my_Player))
        {
            playerInRange = true;
        }
    }
    private void OnTriggerExit(Collider my_collider)
    {
        if (my_collider.gameObject.Equals(my_Player))
        {
            playerInRange = false;
        }
    }

    public bool Add(Loot item)
    {
        foreach (ItemClass itemClass in Items)
        {
            if (itemClass.loot == item && itemClass.stackSize < maxSlotCapacity)
            {
                itemClass.IncStackSize();
                return true;
            }
        }

        if (Items.Count < maxSlots)
        {
            Items.Add(new ItemClass(item));
            return true;
        }

        return false;
    }

    public void DeleteFromChestInventory(ItemClass itemClass)
    {
        Items.Remove(itemClass);
    }
}
