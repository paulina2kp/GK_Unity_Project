using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISlotsHandler : MonoBehaviour
{
    public Transform inventoryPanel;
    public Transform craftPanel;
    private List<InventorySlots> slotsList = new List<InventorySlots> ();

    private void Start()
    {
        foreach (Transform currSlot in inventoryPanel)
        {
            InventorySlots slotComponent = currSlot.GetComponent<InventorySlots> ();
            if(slotComponent != null)
            {
                slotsList.Add (slotComponent);
            }
        }
        UpdateInventorySlots();
    }

    public void UpdateInventorySlots()
    {
        for(int i = 0; i < slotsList.Count; i++)
        {
            if(i < Inventory.Instance.Items.Count)
            {
                slotsList[i].SetSlot(Inventory.Instance.Items[i]);
            }
            else
            {
                slotsList[i].ClearSlot();
                slotsList[i].currentItem = null;
            }
        }

    }

}
