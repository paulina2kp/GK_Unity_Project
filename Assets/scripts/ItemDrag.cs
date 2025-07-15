using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDrag : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public Canvas canvas;
    public Transform originalParent;
    public CanvasGroup canvasGroup;
    public Transform craftSlot1;
    public Transform craftSlot2;
    public Transform craftSlot3;
    public ItemClass draggedItem;

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(canvas.transform);
        canvasGroup.blocksRaycasts = false;
        draggedItem = originalParent.GetComponent<InventorySlots>().GetItem();
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            canvas.worldCamera,
            out pos
        );

        transform.localPosition = pos;     
    }

    public void OnEndDrag(PointerEventData eventData)
    {       
        if (IsPointerOverSlot(craftSlot1, eventData))
        {
            MoveToSlot(craftSlot1);
            FindFirstObjectByType<CraftManager>().prepareCurrentRecepie();
            FindFirstObjectByType<CraftManager>().GiveItemFromRecepie();
        }
        else if (IsPointerOverSlot(craftSlot2, eventData))
        {
            MoveToSlot(craftSlot2);
            FindFirstObjectByType<CraftManager>().prepareCurrentRecepie();
            FindFirstObjectByType<CraftManager>().GiveItemFromRecepie();
        }
        else if (IsPointerOverSlot(craftSlot3, eventData))
        {
            MoveToSlot(craftSlot3);
            FindFirstObjectByType<CraftManager>().prepareCurrentRecepie();
            FindFirstObjectByType<CraftManager>().GiveItemFromRecepie();
        }
        else
        {
            transform.SetParent(originalParent);
            transform.localPosition = Vector3.zero;
        }

        canvasGroup.blocksRaycasts = true;
    }

    private void MoveToSlot(Transform slot)
    {
        slot.GetComponent<InventorySlots>().PrepareSlot();
        slot.GetComponent<InventorySlots>().currentItem = draggedItem;
        slot.GetComponent<InventorySlots>().item_sprite.sprite = draggedItem.loot.loot_sprite;

        draggedItem.DecStackSize();

        if(originalParent.GetComponent<InventorySlots>().currentItem.stackSize > 0)
        {
            originalParent.GetComponent<InventorySlots>().item_sprite.sprite = draggedItem.loot.loot_sprite;
            transform.SetParent(originalParent);
            transform.localPosition = Vector3.zero;
        }else if(originalParent.GetComponent<InventorySlots>().currentItem.stackSize <= 0)
        {
            FindFirstObjectByType<Inventory>().DeleteFromInventory(draggedItem);
            transform.SetParent(originalParent);
            transform.localPosition = Vector3.zero;
        }
            FindFirstObjectByType<UISlotsHandler>().UpdateInventorySlots();
    }

    private bool IsPointerOverSlot(Transform slot, PointerEventData eventData)
    {
        RectTransform rect = slot as RectTransform;

        return RectTransformUtility.RectangleContainsScreenPoint(
            rect,
            eventData.position,
            eventData.pressEventCamera) && slot.GetComponent<InventorySlots>().item_sprite.sprite == null;
    }
}
