using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    public int maxSlots = 2;
    public int maxSlotCapacity = 20;

    public List<ItemClass> Items = new List<ItemClass>();

    private void Awake()
    {
        Instance = this;
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

    public void DeleteFromInventory(ItemClass itemClass)
    {
        Items.Remove(itemClass);
    }
}
