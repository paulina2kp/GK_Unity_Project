using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlots : MonoBehaviour
{
    public Image item_sprite;
    public TextMeshProUGUI item_number;

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
        if (item == null)
        {
            ClearSlot();
            return;
        }

        PrepareSlot();

        item_sprite.sprite = item.loot.loot_sprite;
        item_number.text = item.stackSize.ToString();
    }
}
