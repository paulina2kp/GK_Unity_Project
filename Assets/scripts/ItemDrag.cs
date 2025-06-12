using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDrag : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public Canvas canvas;
    public Transform originalParent;
    public CanvasGroup canvasGroup;
    //public CanvasGroup craftCanvasGroup;
    public Transform craftSlot1;
    public Transform craftSlot2;
    public Transform craftSlot3;
    public ItemClass draggedItem;

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(canvas.transform);
        canvasGroup.blocksRaycasts = false;
        Debug.Log("ZACZYNAM DRAG");
        draggedItem = originalParent.GetComponent<InventorySlots>().GetItem();
        Debug.Log("drag item to: " + draggedItem.loot.loot_name);
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
        
        //Debug.Log("DRAGUJE");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
        if (IsPointerOverSlot(craftSlot1, eventData))
        {
            Debug.Log("TAK 1");
            MoveToSlot(craftSlot1);
        }
        else if (IsPointerOverSlot(craftSlot2, eventData))
        {
            Debug.Log("TAK 2");
            MoveToSlot(craftSlot2);
        }
        else if (IsPointerOverSlot(craftSlot3, eventData))
        {
            Debug.Log("TAK 3");
            MoveToSlot(craftSlot3);
        }
        else
        {
            Debug.Log("TAK NIC");
            // Nie trafi³ w ¿aden slot — wróæ na miejsce
            transform.SetParent(originalParent);
            transform.localPosition = Vector3.zero;
        }

        //transform.SetParent(originalParent);

        //transform.localPosition = Vector3.zero;
        canvasGroup.blocksRaycasts = true;
           
        Debug.Log("KONIEC DRAG");
    }

    private void MoveToSlot(Transform slot)
    {
        //transform.SetParent(slot);
        //transform.localPosition = Vector3.zero;
        //Debug.Log("PRZENIESIONO DO: " + slot.name);

        slot.GetComponent<InventorySlots>().currentItem = draggedItem;
        slot.GetComponent<InventorySlots>().item_sprite.sprite = draggedItem.loot.loot_sprite;
        //transform.localPosition = Vector3.zero;
        //transform.SetParent(originalParent);
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
