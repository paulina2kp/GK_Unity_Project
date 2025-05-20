using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventorySlots : MonoBehaviour, ISelectHandler
{
    public Image item_sprite;
    public TextMeshProUGUI item_number;
    private ItemClass currentItem;
    public GameObject optionPanel;
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
            /////////
            //currentItem.loot.onUse?.Invoke();
            //if (optionPanel != null)
            //{
            //    optionPanel.SetActive(true);
            //}
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
            currentItem.loot.onUse?.Invoke();
        }

        if (EventSystem.current.currentSelectedGameObject == gameObject && Input.GetKeyDown(KeyCode.R) && currentItem!=null)
        {
            Debug.Log("NACISNIETO R NA: " + currentItem.loot.loot_name);
            currentItem.loot.onDrop?.Invoke();
        }
    }
}
