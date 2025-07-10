using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlots : MonoBehaviour, ISelectHandler
{
    public Image item_sprite;
    public TextMeshProUGUI item_number;
    public ItemClass currentItem;
    public GameObject optionPanel;
    public int index;

    public bool isChestSlot = false;
    public UIChestSlots chestUI;
    void Start()
    {
        if (index == 0 || index == 1 || index == 2 || index == 3)
        {
           ClearSlot();
        }
    }
    public ItemClass GetItem()
    {
        return currentItem;
    }
    public int GetStackSize()
    {
        return currentItem.stackSize;
    }
    public void ClearSlot()
    {
        item_sprite.enabled = false;
        item_number.enabled = false;
    }

    public void PrepareSlot()
    {
        item_sprite.enabled = true;
        item_number.enabled = true;
    }

    public void SetSlot(ItemClass item)
    {
        currentItem = item;

        if (item == null)
        {
            ClearSlot();
            return;
        }

        PrepareSlot();

        item_sprite.sprite = item.loot.loot_sprite;
        item_number.text = item.stackSize.ToString();
    }

    public void ItemClicked()
    {
        if (currentItem != null)
        {
            Debug.Log("KLIKLEM " + currentItem.loot.name);
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (optionPanel != null && currentItem != null)
        {
            optionPanel.SetActive(true);
            Debug.Log("ZAZNACZONO: " + currentItem.loot.loot_name);
        }
    }

    void Update()
    {
        if (optionPanel.activeSelf && EventSystem.current.currentSelectedGameObject == null)
        {
            optionPanel.SetActive(false);
        }

        if (EventSystem.current.currentSelectedGameObject == gameObject && Input.GetKeyDown(KeyCode.E) && currentItem != null)
        {
            Debug.Log("NACISNIETO E NA: " + currentItem.loot.loot_name);
            if (index >= 4 && index <= 10 && currentItem.loot.isUsable == true)
            {
                currentItem.loot.onUse?.Invoke();
                currentItem.DecStackSize();
                if (currentItem.stackSize <= 0)
                {
                    FindFirstObjectByType<Inventory>().DeleteFromInventory(currentItem);
                    currentItem = null;
                }
                FindFirstObjectByType<UISlotsHandler>().UpdateInventorySlots();
            }
            else
            {
                Debug.Log("NACISNIETO NA E, ALE ITEM IS NOT USABLE");
            }
        }

        if (EventSystem.current.currentSelectedGameObject == gameObject && Input.GetKeyDown(KeyCode.R) && currentItem!=null)
        {
            Debug.Log("NACISNIETO R NA: " + currentItem.loot.loot_name);
            if (index >= 4 && index <= 10)
            {
                currentItem.loot.onDrop?.Invoke();
                currentItem.DecStackSize();
                if (currentItem.stackSize <= 0)
                {
                    FindFirstObjectByType<Inventory>().DeleteFromInventory(currentItem);
                    currentItem = null;
                }
                FindFirstObjectByType<UISlotsHandler>().UpdateInventorySlots();
            }
            
            if(index == 0 || index == 1 || index == 2)
            {
                currentItem.loot.onDrop?.Invoke();
                currentItem = null;
                item_sprite.sprite = null;
                ClearSlot();
                FindFirstObjectByType<CraftManager>().prepareCurrentRecepie();
                FindFirstObjectByType<CraftManager>().GiveItemFromRecepie();
            }

            if (index == 3)
            {
                int realStackSize = FindFirstObjectByType<CraftManager>().GetStackSize();
                for(int i = 0; i < realStackSize; i++)
                {
                    currentItem.loot.onDrop?.Invoke();
                    currentItem.DecStackSize();
                }

                if (currentItem.stackSize <= 0)
                {
                    int realIndex = FindFirstObjectByType<CraftManager>().GetCurrIndex();
                    currentItem = null;
                    item_sprite.sprite = null;
                    ClearSlot();
                    FindFirstObjectByType<CraftManager>().SetBackStackSize(realIndex);
                }
                FindFirstObjectByType<CraftManager>().ClearAllSlots();
            }

            /*if (isChestSlot)
            {
                currentItem.loot.onDrop?.Invoke();
                currentItem.DecStackSize();

                if (currentItem.stackSize <= 0)
                {
                    chestUI.TryRemoveItemFromSlot(index);
                    currentItem = null;
                }
                chestUI.UpdateChestSlots();
            }*/
        }

        /*if (EventSystem.current.currentSelectedGameObject == gameObject && Input.GetKeyDown(KeyCode.Q) && currentItem != null)
        {
            Debug.Log("NACISNIETO Q NA: " + currentItem.loot.loot_name);
            if (index >= 4 && index <= 10)
            {
                currentItem.loot.onStore?.Invoke();
                currentItem.DecStackSize();
                if (currentItem.stackSize <= 0)
                {
                    FindFirstObjectByType<Inventory>().DeleteFromInventory(currentItem);
                    currentItem = null;
                }
                FindFirstObjectByType<UISlotsHandler>().UpdateInventorySlots();
            }

            if (index == 0 || index == 1 || index == 2)
            {
                currentItem.loot.onStore?.Invoke();
                currentItem = null;
                item_sprite.sprite = null;
                ClearSlot();
                FindFirstObjectByType<CraftManager>().prepareCurrentRecepie();
                FindFirstObjectByType<CraftManager>().GiveItemFromRecepie();
            }

            if (index == 3)
            {
                int realStackSize = FindFirstObjectByType<CraftManager>().GetStackSize();
                for (int i = 0; i < realStackSize; i++)
                {
                    currentItem.loot.onStore?.Invoke();
                    currentItem.DecStackSize();
                }

                if (currentItem.stackSize <= 0)
                {
                    int realIndex = FindFirstObjectByType<CraftManager>().GetCurrIndex();
                    currentItem = null;
                    item_sprite.sprite = null;
                    ClearSlot();
                    FindFirstObjectByType<CraftManager>().SetBackStackSize(realIndex);
                }
                FindFirstObjectByType<CraftManager>().ClearAllSlots();
            }

            if (isChestSlot)
            {
                Debug.Log("juz jest w chest");
            }
        }*/
    }
}
