using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class CraftManager : MonoBehaviour
{
    //private ItemClass currItem;
    //public List<Loot> currCraftItems = new List<Loot>();
    public List<string> recepieString = new List<string>();
    private List<List<string>> finalRecepiesList = new List<List<string>>();
    private List<string> currRecepie = new List<string>();
    private int actualStackSize;
    private int actualIndex;
    public ItemClass[] recepieResult;

    public Transform craftSlot1;
    public Transform craftSlot2;
    public Transform craftSlot3;
    public Transform resultSlot;

    private ItemClass currItemSlot1;
    private ItemClass currItemSlot2;
    private ItemClass currItemSlot3;

    void Start()
    {
        PrepareFinalRecepies();
    }

    public void GiveItemFromRecepie()
    {
        for (int i = 0; i < finalRecepiesList.Count; i++)
        {
            if (CheckIfGoodrecepie(finalRecepiesList[i]))
            {           
                SetResultSlot(i);
                actualIndex = i;
                break;
            }
            else
            {
                ResetResultSlot();
            }
        }
    }
    void SetResultSlot(int i)
    {
        resultSlot.GetComponent<InventorySlots>().PrepareSlot();
        resultSlot.GetComponent<InventorySlots>().SetSlot(recepieResult[i]);
        actualStackSize = resultSlot.GetComponent<InventorySlots>().GetStackSize();
    }

    void ResetResultSlot()
    {
        resultSlot.GetComponent<InventorySlots>().currentItem = null;
        resultSlot.GetComponent<InventorySlots>().item_sprite.sprite = null;
        resultSlot.GetComponent<InventorySlots>().item_number.text = null;
        resultSlot.GetComponent<InventorySlots>().ClearSlot();
    }

    public void SetBackStackSize(int i)
    {
        recepieResult[i].stackSize = actualStackSize;
    }
    
    public int GetStackSize()
    {
        return actualStackSize;
    }
    public void ClearAllSlots()
    {
        craftSlot1.GetComponent<InventorySlots>().currentItem = null;
        craftSlot1.GetComponent<InventorySlots>().item_sprite.sprite = null;
        craftSlot2.GetComponent<InventorySlots>().currentItem = null;
        craftSlot2.GetComponent<InventorySlots>().item_sprite.sprite = null;
        craftSlot3.GetComponent<InventorySlots>().currentItem = null;
        craftSlot3.GetComponent<InventorySlots>().item_sprite.sprite = null;
    }

    public void prepareCurrentRecepie()
    {
        currRecepie.Clear();

        currItemSlot1 = craftSlot1.GetComponent<InventorySlots>().GetItem();
        currRecepie.Add(currItemSlot1?.loot?.loot_name ?? "null");

        currItemSlot2 = craftSlot2.GetComponent<InventorySlots>().GetItem();
        currRecepie.Add(currItemSlot2?.loot?.loot_name ?? "null");

        currItemSlot3 = craftSlot3.GetComponent<InventorySlots>().GetItem();
        currRecepie.Add(currItemSlot3?.loot?.loot_name ?? "null");
    }

    bool CheckIfGoodrecepie(List<string> currTestedList)
    {
        return Enumerable.SequenceEqual(currRecepie.OrderBy(t => t), currTestedList.OrderBy(t => t));
    }

    void PrepareFinalRecepies()
    {
        foreach (string line in recepieString)
        {
            List<string> oneRecepie = line.Split(' ').ToList();
            finalRecepiesList.Add(oneRecepie);
        }
    }

    public int GetCurrIndex()
    {
        return actualIndex;
    }

}
